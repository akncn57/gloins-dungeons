using System.Collections;
using Health;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Utils;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Components")] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Animator Animator { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public HealthController HealthController { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public CameraShake CameraShake { get; private set; }
        
        [field: SerializeField, BoxGroup("VFX")] public GameObject DashVFXLeft { get; private set; }
        [field: SerializeField, BoxGroup("VFX")] public GameObject DashVFXRight { get; private set; }
        [field: SerializeField, BoxGroup("VFX")] public GameObject BloodVFX { get; private set; }
        
        [field: SerializeField, BoxGroup("Stats"),Required] public CharacterStatsSO CharacterStats { get; private set; }
        
        [field: SerializeField, BoxGroup("Input")] public Joystick joystick;
        
        [field: SerializeField, BoxGroup("UI")] private Button lightAttackButton;
        [field: SerializeField, BoxGroup("UI")] private Button heavyAttackButton;
        [field: SerializeField, BoxGroup("UI")] private Button dashButton;
        [field: SerializeField, BoxGroup("UI")] private Button testHurtButton;
        [field: SerializeField, BoxGroup("UI")] private Button testDeathButton;
        [field: SerializeField, BoxGroup("UI")] private Button resetButton;
        
        public Vector2 MovementInput { get; private set; }
        public float LastDashTime { get; set; } = -100f;
        public float LastHeavyAttackTime { get; set; } = -100f;
        
        private CharacterStateMachine _characterStateMachine;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            _characterStateMachine = new CharacterStateMachine(this);
            HealthController.Initialize(CharacterStats.MaxHealth);
        }

        private void Start()
        {
            lightAttackButton.onClick.AddListener(() => _characterStateMachine.OnLightAttackPressed());
            heavyAttackButton.onClick.AddListener(() => _characterStateMachine.OnHeavyAttackPressed());
            dashButton.onClick.AddListener(() => _characterStateMachine.OnDashPressed());
            testHurtButton.onClick.AddListener(() => _characterStateMachine.OnHurt());
        }

        private void Update()
        {
            MovementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            
            _characterStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _characterStateMachine.FixedUpdate();
        }
        
        public bool CanDash()
        {
            return Time.time >= LastDashTime + CharacterStats.DashCooldown;
        }
        
        public bool CanHeavyAttack()
        {
            return Time.time >= LastHeavyAttackTime + CharacterStats.HeavyAttackCooldown;
        }
        
        public void HitStop(float duration = 0.05f) 
        {
            StartCoroutine(HitStopRoutine(duration));
        }

        private IEnumerator HitStopRoutine(float duration)
        {
            // 1. Oyunu tamamen dondur (Zaman akışı = 0)
            Time.timeScale = 0f; 

            // 2. DİKKAT: Zaman durduğu için normal WaitForSeconds ÇALIŞMAZ!
            // Gerçek dünya zamanına göre (Realtime) beklemeliyiz:
            yield return new WaitForSecondsRealtime(duration); 

            // 3. Süre doldu, zamanı normale (1) döndür
            Time.timeScale = 1f; 
        }
        
        public void OnLightAttackAnimationEnd()
        {
            _characterStateMachine.OnLightAttackAnimationEnd();
        }
        
        public void OnHeavyAttackAnimationEnd()
        {
            _characterStateMachine.OnHeavyAttackAnimationEnd();
        }

        public void OnHurtAnimationEnd()
        {
            _characterStateMachine.OnHurtAnimationEnd();
        }

        public void SquashAndStretch(Vector2 targetScale, float duration)
        {
            // Varsa önceki DOTween animasyonunu iptal et
            SpriteRenderer.transform.DOKill();
            
            // Boyutu önce normale (1, 1, 1) al
            SpriteRenderer.transform.localScale = Vector3.one;

            // DOTween ile büyüklüğe git (sürecin yarısında git) ve sonra geri gel (Yoyo)
            SpriteRenderer.transform.DOScale(targetScale, duration / 2f)
                .SetLoops(2, DG.Tweening.LoopType.Yoyo)
                .SetEase(DG.Tweening.Ease.OutQuad);
        }

        public void StartWalkBounce()
        {
            SpriteRenderer.transform.DOKill();
            SpriteRenderer.transform.localScale = Vector3.one;
            
            // Yürüyüşte sürekli bir yaylanma (dikeyde hafifçe bastırıp uzat)
            // Süre 0.2f olursa = 0.4 saniyede bir tam adım hissi verir.
            SpriteRenderer.transform.DOScale(new Vector3(1.03f, 0.97f, 1f), 0.2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public void StopWalkBounce()
        {
            // O anki esnemeyi durdur ve yumuşakça orijinal haline dön
            SpriteRenderer.transform.DOKill();
            SpriteRenderer.transform.DOScale(Vector3.one, 0.1f);
        }
    }
}