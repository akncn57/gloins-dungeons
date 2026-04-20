using UnityEngine;
using Health;
using DG.Tweening;
using Zenject;
using Zenject.Signals.Combat;

namespace Character.Combat
{
    public class CharacterCombatController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform attackPoint; // Vuruşun merkez noktası
        [SerializeField] private float attackRadius = 0.5f; // Vuruşun büyüklüğü (yarıçapı)
        [SerializeField] private LayerMask damageableLayer; // Sadece bu layer'daki objelere vur (Performans için)
        
        [Header("Auto-Targeting")]
        [SerializeField] private float targetingRadius = 5f;
        [SerializeField] private GameObject indicatorPrefab;
        [SerializeField] private Vector3 indicatorOffset = new Vector3(0, 1.5f, 0);
        
        private GameObject _activeIndicator;
        private SpriteRenderer _indicatorSpriteRenderer;
        private bool _isIndicatorFadingOut;
        
        public Transform CurrentTarget { get; private set; }
        
        // Bu referansı Hit-Stop yapmak için kullanacağız
        private CharacterController _characterController;
        private SignalBus _signalBus;
        private Transform _previousTarget; // 3. Değişimi anlamak için eski hedefi tut
        private HealthController _currentTargetHealth;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_characterController != null)
            {
                CurrentTarget = GetBestTarget(_characterController.MovementInput);
                
                HandleTargetSignal();
                
                if (CurrentTarget != null && indicatorPrefab != null)
                {
                    if (_activeIndicator == null)
                    {
                        _activeIndicator = Instantiate(indicatorPrefab);
                        _activeIndicator.SetActive(false); // Prefab varsayılan olarak açıksa diye zorla kapatıyoruz ki alttaki Fade In bloğu tetiklensin!
                        _indicatorSpriteRenderer = _activeIndicator.GetComponentInChildren<SpriteRenderer>();
                        
                        // Başlangıçta görünmez (Alpha 0) yap
                        if (_indicatorSpriteRenderer != null)
                        {
                            Color c = _indicatorSpriteRenderer.color;
                            c.a = 0f;
                            _indicatorSpriteRenderer.color = c;
                        }
                    }
                    
                    // Eğer kapalıysa veya şu an sönerek kapanmaktaysa, tekrar aydınlanarak aç (Fade In)
                    if (!_activeIndicator.activeSelf || _isIndicatorFadingOut)
                    {
                        _activeIndicator.SetActive(true);
                        _isIndicatorFadingOut = false;
                        
                        if (_indicatorSpriteRenderer != null)
                        {
                            _indicatorSpriteRenderer.DOKill();
                            _indicatorSpriteRenderer.DOFade(1f, 0.2f); // 0.2 saniyede belirginleş
                        }
                    }
                    
                    _activeIndicator.transform.position = CurrentTarget.position + indicatorOffset;
                }
                else if (_activeIndicator != null && _activeIndicator.activeSelf && !_isIndicatorFadingOut)
                {
                    _isIndicatorFadingOut = true;
                    
                    if (_indicatorSpriteRenderer != null)
                    {
                        // 0.2 saniyede soluklaş (Fade Out), bitince objeyi kapat
                        _indicatorSpriteRenderer.DOKill();
                        _indicatorSpriteRenderer.DOFade(0f, 0.2f).OnComplete(() => 
                        {
                            _activeIndicator.SetActive(false);
                            _isIndicatorFadingOut = false;
                        });
                    }
                    else
                    {
                        _activeIndicator.SetActive(false);
                        _isIndicatorFadingOut = false;
                    }
                }
            }

            if (attackPoint != null && _characterController != null && _characterController.SpriteRenderer != null)
            {
                // Kılıç vuruş noktasını (attackPoint) karakterin baktığı yöne göre dinamik olarak çevir (flipX)
                float absX = Mathf.Abs(attackPoint.localPosition.x);
                float targetX = _characterController.SpriteRenderer.flipX ? -absX : absX;
                
                attackPoint.localPosition = new Vector3(targetX, attackPoint.localPosition.y, attackPoint.localPosition.z);
            }
        }

        // Bu metot Animasyon Event'i ile kılıcın tam hedefe ulaştığı karede çağrılacak!
        public void PerformMeleeAttack(int damageAmount)
        {
            // 1. attackPoint merkezli hayali bir çember çiz ve içindeki tüm objeleri bul
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, damageableLayer);

            Debug.Log($"<b><color=orange>[Combat]</color></b> Kılıç savruldu! Çember içinde <b>{hitColliders.Length}</b> adet obje bulundu.");
            
            bool hitSuccess = false;

            // 2. Bulunan her obje için döngüye gir
            foreach (Collider2D hitCollider in hitColliders)
            {
                // 3. DEPENDENCY INVERSION (DIP): Objenin Düşman mı Kutu mu olduğunu sormuyoruz!
                // Sadece "Sen hasar alabilen bir şey misin?" diye soruyoruz.
                if (hitCollider.TryGetComponent(out IDamageable damageable))
                {
                    AttackType attackType = AttackType.Light; // Default to Light
                    if (_characterController != null && _characterController.StateMachine != null)
                    {
                        if (_characterController.StateMachine.CurrentState == _characterController.StateMachine.HeavyAttackState)
                        {
                            attackType = AttackType.Heavy;
                        }
                    }

                    damageable.TakeDamage(damageAmount, transform.position, attackType);
                    hitSuccess = true; // En az bir şeye vurduk!

                    if (hitCollider.TryGetComponent(out HealthController healthController))
                    {
                        _signalBus.Fire(new HealthChangedSignal 
                        { 
                            CurrentHealth = healthController.CurrentHealth, 
                            MaxHealth = healthController.MaxHealth 
                        });
                    }
                    
                    Debug.Log($"<b><color=green>[Combat] BAŞARILI VURUŞ!</color></b> {hitCollider.name} isimli hedefe <b>{damageAmount}</b> hasar verildi.");
                }
                else
                {
                    Debug.Log($"<b><color=yellow>[Combat] UYARI:</color></b> Kılıç {hitCollider.name} objesine çarptı ama bu objede IDamageable yok!");
                }
            }

            // 4. GAME FEEL: Eğer bir şeye başarılı şekilde vurduysak zamanı dondur!
            if (hitSuccess)
            {
                Debug.Log("<b><color=cyan>[Combat]</color></b> En az bir hedefe vuruldu, <b>Hit-Stop (Zaman Donması)</b> tetikleniyor!");
                _characterController.HitStop(0.05f); 
            }
            else
            {
                Debug.Log("<b><color=grey>[Combat]</color></b> Kılıç boşa savruldu (İsabet yok).");
            }
        }

        // Unity Editöründe vuruş alanını (çemberi) görmek için harika bir yardımcı metot
        private void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
            }
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, targetingRadius);
        }

        public Transform GetBestTarget(Vector2 movementInput)
        {
            Collider2D[] potentialTargets = Physics2D.OverlapCircleAll(transform.position, targetingRadius, damageableLayer);
            
            Transform bestTarget = null;
            float bestScore = -float.MaxValue;

            foreach (var col in potentialTargets)
            {
                // Hasar alabilen bir şey mi?
                if (!col.TryGetComponent(out IDamageable _)) continue;

                // Karakterin kendisine çarpmasını engelle
                if (col.gameObject == gameObject) continue;

                Vector2 directionToTarget = (col.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, col.transform.position);

                float score = 0;

                if (movementInput.magnitude > 0.1f)
                {
                    // Oyuncu hareket ediyorsa, gittiği yöne olan açıyı (Dot Product) baz al
                    float dot = Vector2.Dot(movementInput.normalized, directionToTarget);
                    
                    // Açıyı uzaklıktan daha çok önemsemek için yüksek katsayı ile çarpıyoruz
                    score = dot * 10f - distance; 
                }
                else
                {
                    // Duruyorsa sadece mesafeye göre puanla (Mesafe kısaldıkça skor yükselir)
                    score = -distance; 
                }

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = col.transform;
                }
            }

            return bestTarget;
        }
        
        private void HandleTargetSignal()
        {
            // Eğer hedef bir önceki kareden farklıysa (Yeni birine geçtik veya hedefi kaybettik)
            if (CurrentTarget != _previousTarget)
            {
                _previousTarget = CurrentTarget;

                if (CurrentTarget != null)
                {
                    // Yeni hedefin HealthSystem bileşenini bir kez alıyoruz (Cache)
                    _currentTargetHealth = CurrentTarget.GetComponent<HealthController>();

                    if (_currentTargetHealth != null)
                    {
                        // Sinyali Ateşle!
                        _signalBus.Fire(new TargetChangedSignal 
                        {
                            EnemyName = CurrentTarget.name,
                            CurrentHealth = _currentTargetHealth.CurrentHealth,
                            MaxHealth = _currentTargetHealth.MaxHealth,
                            HasTarget = true
                        });
                    }
                }
                else
                {
                    // Hedef kaybedildi, UI kapansın diye boş sinyal gönder
                    _currentTargetHealth = null;
                    _signalBus.Fire(new TargetChangedSignal { HasTarget = false });
                }
            }
            // Eğer hedef aynı ama canı değişmişse (Hasar anında güncelleme için)
            else if (CurrentTarget != null && _currentTargetHealth != null)
            {
                // Opsiyonel: Buraya can değişimi için ayrı bir sinyal de eklenebilir 
                // veya basitçe her karede UI güncellenmesin diye bir "lastHealth" kontrolü yapılabilir.
            }
        }
    }
}