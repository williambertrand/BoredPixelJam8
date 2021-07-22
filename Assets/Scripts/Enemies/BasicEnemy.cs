using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum EnemyState
{
    IDLE,
    MOVING,
    PATROLING,
    ATTACKING,
}

public class BasicEnemy : Enemy
{


    // Attack related variables
    [Header("Ranged Attack")]
    public bool isRanged;
    public GameObject projectile;
    public float shootVel = 2.0f;

    [Tooltip("How long between each attack")]
    public float attackTime;
    float lastAttack;

    [Tooltip("How much damage this will do if it hits player")]
    public int attackDamage;
    public float attackDistance;

    [Tooltip("Where attack/projectiles start")]
    public Transform attackPoint;

    [Tooltip("Weapon Sprite")]
    public Transform weaponTransform;


    [Header("Enemy Movement")]
    [Tooltip("How fast can the enemy move?")]
    public float moveSpeed;

    [Header("Enemy State handling")]
    float idleDone;
    public bool patrolSwitch;
    public Transform patrolLoc;

    float reachDist = 0.2f;

    [Tooltip("Player target")]
    public GameObject target;
    Vector3 lastPlayerSighing;
    Vector3 moveDest;
    Vector3 startPos;

    // For determining which way the enemy is currently facing.
    public bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 velocity = Vector3.zero;

    public LayerMask attackMask;


    public EnemyState currentState;


    // Section: State handling
    void _handlePatrolOrMoveState()
    {
        if (Mathf.Abs(transform.position.x - moveDest.x)  < reachDist)
        {
            //Transition to idle
            idleDone = Time.time + Random.Range(1.5f, 3.0f);
            currentState = EnemyState.IDLE;
            // TODO: Animation handling
            //anim.SetFloat("move", 0);
            //anim.SetTrigger("idle");
        }
    }

    void _handleIdleState()
    {
        if (Time.time > idleDone)
        {
            if(patrolLoc != null)
            {
                //Transition back to patrolling
                if (patrolSwitch)
                {
                    moveDest = patrolLoc.position;
                }
                else
                {
                    moveDest = startPos;
                }
                patrolSwitch = !patrolSwitch;
                currentState = EnemyState.PATROLING;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentState = EnemyState.IDLE;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        if (target != null)
        {
            currentState = EnemyState.ATTACKING;
        }
        else if (patrolLoc != null)
        {
            moveDest = patrolLoc.position;
            currentState = EnemyState.PATROLING;
        }
    }

    void _handleAttackState()
    {
        if (target != null)
        {
            UpdateLookAt();

            if (weaponTransform != null)
            {
                UpdateWeaponRot();
            }
            if (CanShoot())
            {
                FireAtTarget();
            }
        }
    }

    public void SetMoveDest(Vector3 pos)
    {
        moveDest = pos;
        currentState = EnemyState.MOVING;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case EnemyState.PATROLING:
                Move();
                _handlePatrolOrMoveState();
                break;
            case EnemyState.MOVING:
                Move();
                _handlePatrolOrMoveState();
                break;
            case EnemyState.ATTACKING:
                _handleAttackState();
                break;
            case EnemyState.IDLE:
                _handleIdleState();
                break;
        }

    }

    void UpdateLookAt()
    {
        float dX = target.transform.position.x - transform.position.x;
        if (dX > 0 && m_FacingRight)
        {
            // ... flip the enemy.
            Flip();
        }
    }

    bool CanShoot()
    {
        return (Time.time - lastAttack >= attackTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            // Check if enemy can see the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, attackDistance, attackMask);
            if (hit.collider.CompareTag("Player"))
            {
                currentState = EnemyState.ATTACKING;
            }
            else
            {
                Debug.Log("hit tag: " + hit.collider.tag);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
            currentState = EnemyState.MOVING;
            lastPlayerSighing = collision.gameObject.transform.position;
            moveDest = lastPlayerSighing;
        }
    }

    void FireAtTarget()
    {
        if (target == null) return;
        // TODO: Animation
        //animator.SetTrigger("attack");

        Vector3 targetPos = target.transform.position;
        Vector3 dir = ( targetPos- transform.position).normalized;

        GameObject proj = Instantiate(projectile, attackPoint.position, Quaternion.identity); //, Quaternion.Euler(new Vector3(0, 0, 0)));
        proj.transform.position = attackPoint.position;
        proj.GetComponent<Rigidbody2D>().velocity = dir * shootVel;
        proj.GetComponent<Projectile>().damage = attackDamage;
        lastAttack = Time.time;
    }

    void UpdateWeaponRot()
    {
        Vector3 diff = target.transform.position - weaponTransform.position;
        diff.Normalize();
        float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (m_FacingRight)
            rot_Z -= 180;

        weaponTransform.rotation = Quaternion.Euler(0f, 0f, rot_Z + 180);
    }

    void Move()
    {
        // Move the character by finding the target velocity
        float dX = moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        Vector3 targetVelocity = new Vector2(dX, m_Rigidbody2D.velocity.y);
        //anim.SetFloat("move", Mathf.Abs(dX));
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, 0.05f);

        if (dX > 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (dX < 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(moveDest, 0.2f);
        Handles.Label(transform.position + new Vector3(0, 1.0f, 0), currentState.ToString());

    }
}
