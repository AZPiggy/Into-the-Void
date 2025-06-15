using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Character Controller 2D is based upon the 2D character controller for Unity by Sharp Blog Code
// URL: https://www.sharpcoderblog.com/blog/2d-platformer-character-controller
//
// Adapted by: Angela Zhu on 6/3/2025


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    float moveDirection = 0;
    bool isGrounded = false;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;

    Rigidbody2D r2d;

    // public Transform groundCheckPosition;
    public Vector3 groundCheckOffset;
    public float groundCheckRadius;
    public LayerMask groundLayerMask;

    // melee attack variables
    public Transform meleeHitBoxObject;
    public LayerMask meleeHitMask;

    // falling off the edge death
    public float deathYThreshold = -10f;

    // animation
    private Animator anim;
    private bool isDead = false;

    private SpriteRenderer sprite;
    private Color originalColor;

    // flip hitbox
    private Vector3 rightFacingOffset;
    private Vector3 leftFacingOffset;

    // Use this for initialization
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;


        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");

        // initialize the animator
        anim = GetComponentInChildren<Animator>();

        originalColor = sprite.color;

        // save initial positions
        rightFacingOffset = meleeHitBoxObject.localPosition;
        leftFacingOffset = new Vector3(-rightFacingOffset.x, rightFacingOffset.y, rightFacingOffset.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Freeze player when not in playing mode
        if (GameManager.game == null || GameManager.game.currentState != GameState.Playing || isDead) 
        {
            return;
        }
        // set the move direction
        moveDirection = moveAction.ReadValue<Vector2>().x;
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));

        // are we jumping?
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {

            r2d.linearVelocity = new Vector2(r2d.linearVelocity.x, jumpHeight);
            SoundManager.S.PlayJumpSound();
            anim.SetBool("IsJumping", true);
            // set grounded to false since already jumping
            isGrounded = false;
        }

        if (isGrounded)
        {
            anim.SetBool("IsJumping", false);
            // Debug.Log("is jumping false");
        }

        // ATTACK!
        if (attackAction.WasPressedThisFrame())
        {
            anim.SetTrigger("PlayerAttack");
            SoundManager.S.PlayAttackSound();
        }

        if (transform.position.y <= deathYThreshold)
        {
            SoundManager.S.PlayPlayerDestroySound();
            GameManager.game.PlayerInstantDeath(); // play animation and set death bool to true
        }

        if (moveDirection > 0f)
        {
            sprite.flipX = false; // face right
            meleeHitBoxObject.localPosition = rightFacingOffset;
        }
        else if (moveDirection < 0f)
        {
            sprite.flipX = true; // face left
            meleeHitBoxObject.localPosition = leftFacingOffset;
        }

    }

    void FixedUpdate()
    {
        if (GameManager.game == null || GameManager.game.currentState != GameState.Playing || isDead)
        {
            return;
        }
        // reset the grounded state
        isGrounded = false;

        // look for colliders that are not us
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundLayerMask);

        // was there a collider
        if (colliders.Length > 0) { isGrounded = true; }

        // Apply movement velocity
        r2d.linearVelocity = new Vector2((moveDirection) * maxSpeed, r2d.linearVelocity.y);
        
    }

    private void MeleeAttack()
    {
        // make sure we are not dead
        if (!isDead)
        {
            // check for enemy objects (any collider)
            Collider2D thisCollider = Physics2D.OverlapBox(meleeHitBoxObject.position, meleeHitBoxObject.localScale, 0f, meleeHitMask);
            if (thisCollider)
            {
                BoarEnemy boarEnemy = thisCollider.GetComponent<BoarEnemy>();
                BeeEnemy beeEnemy = thisCollider.GetComponent<BeeEnemy>();
                if (boarEnemy)
                {
                    boarEnemy.BoarEnemyDie();
                }
                else if (beeEnemy)
                {
                    beeEnemy.BeeEnemyDie();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // show the grounded radius
        if (isGrounded)
        {
            Gizmos.color = Color.yellow;
        } else
        {
            Gizmos.color = Color.red;
        }

        // draw the circle for the ground check
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(meleeHitBoxObject.position, meleeHitBoxObject.localScale);

    }

    public void PlayDeathAnimation()
    {
        // already played
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("Die");

        // freeze movement, but keep gravity
        r2d.linearVelocity = Vector2.zero;
        r2d.bodyType = RigidbodyType2D.Kinematic;
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRedCoroutine());
    }

    private IEnumerator FlashRedCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = originalColor;
    }

}