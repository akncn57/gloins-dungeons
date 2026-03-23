using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Components")] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public Animator Animator { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField, BoxGroup("Components")] public ParticleSystem DashEffect { get; private set; }
        
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
        }

        private void Start()
        {
            lightAttackButton.onClick.AddListener(() => _characterStateMachine.OnLightAttackPressed());
            heavyAttackButton.onClick.AddListener(() => _characterStateMachine.OnHeavyAttackPressed());
            dashButton.onClick.AddListener(() => _characterStateMachine.OnDashPressed());
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
    }
}