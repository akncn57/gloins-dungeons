using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int heavyAttackCooldown = 3;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Button lightAttackButton;
    [SerializeField] private Button heavyAttackButton;
    [SerializeField] private Button testHurtButton;
    [SerializeField] private Button testDeathButton;
    [SerializeField] private Button resetButton;

    private Vector2 _movement;
    private bool _isAttacking;
    private bool _isHeavyAttackOnCooldown;

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        lightAttackButton.onClick.AddListener(PlayLightAttack);
        heavyAttackButton.onClick.AddListener(PlayHeavyAttack);
        testHurtButton.onClick.AddListener(TestHurt);
        testDeathButton.onClick.AddListener(TestDeath);
        resetButton.onClick.AddListener(ResetLevel);
    }

    private void Update()
    {
        if (_isAttacking)
        {
            _movement = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        _movement = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (_movement.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);

            if (_movement.x != 0)
                spriteRenderer.flipX = _movement.x < 0;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (!_isAttacking)
        {
            rb.linearVelocity = _movement.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void PlayLightAttack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        animator.SetTrigger("lightAttack");
    }

    private void PlayHeavyAttack()
    {
        if (_isAttacking || _isHeavyAttackOnCooldown) return;

        _isAttacking = true;
        _isHeavyAttackOnCooldown = true;
        animator.SetTrigger("heavyAttack");
        StartCoroutine(HeavyAttackCooldownRoutine());
    }
    
    private IEnumerator HeavyAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(heavyAttackCooldown);
        _isHeavyAttackOnCooldown = false;
    }
    
    public void OnAttackEnd()
    {
        _isAttacking = false;
    }
    
    private void TestHurt()
    {
        animator.SetTrigger("hurt");
    }

    private void TestDeath()
    {
        animator.SetTrigger("death");
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
