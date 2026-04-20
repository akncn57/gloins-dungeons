using System.Collections;
using Health;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Utils;
using Character.Combat;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Components")] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Animator Animator { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public HealthController HealthController { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public CameraShake CameraShake { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public CharacterCombatController CombatController { get; private set; }
        
        [field: SerializeField, BoxGroup("VFX")] public GameObject DashVFXLeft { get; private set; }
        [field: SerializeField, BoxGroup("VFX")] public GameObject DashVFXRight { get; private set; }
        [field: SerializeField, BoxGroup("VFX")] public GameObject BloodVFX { get; private set; }
        
        [field: SerializeField, BoxGroup("Stats"),Required] public CharacterStatsSO CharacterStats { get; private set; }
        
        [field: SerializeField, BoxGroup("Input")] public Joystick joystick;
        
        [field: SerializeField, BoxGroup("UI")] private Button lightAttackButton;
        [field: SerializeField, BoxGroup("UI")] private Button heavyAttackButton;
        [field: SerializeField, BoxGroup("UI")] private Button dashButton;
        
        public Vector2 MovementInput { get; private set; }
        public float LastDashTime { get; set; } = -100f;
        public float LastHeavyAttackTime { get; set; } = -100f;
        
        public CharacterStateMachine StateMachine => _characterStateMachine;
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
            
            HealthController.OnTakeDamage += HandleTakeDamage;
            HealthController.OnDeath += HandleDeath;
        }

        private void OnDestroy()
        {
            if (HealthController != null)
            {
                HealthController.OnTakeDamage -= HandleTakeDamage;
                HealthController.OnDeath -= HandleDeath;
            }
        }

        private void HandleTakeDamage(int currentHealth, Vector2 damageSourcePosition, AttackType attackType)
        {
            if (currentHealth > 0)
            {
                _characterStateMachine.OnHurt();
            }
            
            if (damageSourcePosition != Vector2.zero)
            {
                var knockbackDirection = ((Vector2)transform.position - damageSourcePosition).normalized;
                Rb.linearVelocity = Vector2.zero;
                Rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse);
            }
        }

        private void HandleDeath()
        {
            _characterStateMachine.ChangeState(_characterStateMachine.DeathState);
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
            Time.timeScale = 0f; 
            yield return new WaitForSecondsRealtime(duration); 
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
            SpriteRenderer.transform.DOKill();
            SpriteRenderer.transform.localScale = Vector3.one;
            SpriteRenderer.transform.DOScale(targetScale, duration / 2f)
                .SetLoops(2, DG.Tweening.LoopType.Yoyo)
                .SetEase(DG.Tweening.Ease.OutQuad);
        }

        public void StartWalkBounce()
        {
            SpriteRenderer.transform.DOKill();
            SpriteRenderer.transform.localScale = Vector3.one;
            SpriteRenderer.transform.DOScale(new Vector3(1.03f, 0.97f, 1f), 0.2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public void StopWalkBounce()
        {
            SpriteRenderer.transform.DOKill();
            SpriteRenderer.transform.DOScale(Vector3.one, 0.1f);
        }
    }
}