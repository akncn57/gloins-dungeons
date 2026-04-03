using UnityEngine;
using Health; // Senin yazdığın IDamageable ve HealthController'ın olduğu namespace

namespace Character.Combat
{
    public class CharacterCombatController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Transform attackPoint; // Vuruşun merkez noktası
        [SerializeField] private float attackRadius = 0.5f; // Vuruşun büyüklüğü (yarıçapı)
        [SerializeField] private LayerMask damageableLayer; // Sadece bu layer'daki objelere vur (Performans için)
        
        // Bu referansı Hit-Stop yapmak için kullanacağız
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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
                    damageable.TakeDamage(damageAmount);
                    hitSuccess = true; // En az bir şeye vurduk!
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
            if (attackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}