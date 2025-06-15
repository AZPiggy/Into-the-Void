using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterControllerTutorial : MonoBehaviour
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

    // animation
    private Animator anim;

    private SpriteRenderer sprite;

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

        // save initial positions
        rightFacingOffset = meleeHitBoxObject.localPosition;
        leftFacingOffset = new Vector3(-rightFacingOffset.x, rightFacingOffset.y, rightFacingOffset.z);

    }

    // Update is called once per frame
    void Update()
    {
        // set the move direction
        moveDirection = moveAction.ReadValue<Vector2>().x;
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));

        // are we jumping?
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {

            r2d.linearVelocity = new Vector2(r2d.linearVelocity.x, jumpHeight);
            SoundManager.S.PlayJumpSound();
            // Debug.Log("Ready to set bool?");
            anim.SetBool("IsJumping", true);
            // set grounded to false since already jumping
            isGrounded = false;
        }

        if (isGrounded)
        {
            anim.SetBool("IsJumping", false);
        }

        // ATTACK!
        if (attackAction.WasPressedThisFrame())
        {
            anim.SetTrigger("PlayerAttack");
            // Debug.Log("attack sound should play");
            SoundManager.S.PlayAttackSound();
            // MeleeAttack();
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
        // reset the grounded state
        isGrounded = false;

        // look for colliders that are not us
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundLayerMask);

        // was there a collider
        if (colliders.Length > 0) { isGrounded = true; }

        // Apply movement velocity
        r2d.linearVelocity = new Vector2((moveDirection) * maxSpeed, r2d.linearVelocity.y);

    }

    private void OnDrawGizmos()
    {
        // show the grounded radius
        if (isGrounded)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        // draw the circle for the ground check
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);

    }

    private void MeleeAttack()
    {
        // check for enemy objects (any collider)
        Collider2D thisCollider = Physics2D.OverlapBox(meleeHitBoxObject.position, meleeHitBoxObject.localScale, 0f, meleeHitMask);
        if (thisCollider)
        {
            BoarTutorialScript boarEnemy = thisCollider.GetComponent<BoarTutorialScript>();
            if (boarEnemy)
            {
                boarEnemy.EnemyDie();
            }
        }
        // else { Debug.Log("Whiff"); }
    }

}
