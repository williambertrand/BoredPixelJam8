using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    IDLE,
    MOVING,
    ATTACKING,
}

public class BasicEnemy : MonoBehaviour
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

    [Tooltip("Where attack/projectiles start")]
    public Transform attackPoint;

    [Tooltip("Weapon Sprite")]
    public Transform weaponTransform;

    [Tooltip("Enemy Health")]
    public int maxHealth;
    public int currentHealth;

    [Tooltip("How fast can the enemy move?")]
    public float moveSpeed;

    GameObject target;
    Vector3 lastPlayerSighing;
    Vector3 moveDest;

    // For determining which way the enemy is currently facing.
    private bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 velocity = Vector3.zero;


    public EnemyState currentState;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentState = EnemyState.IDLE;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(target != null)
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

        if(currentState == EnemyState.MOVING)
        {
            MoveTowardsDest();
        }

    }

    void UpdateLookAt()
    {
        float dX = target.transform.position.x - transform.position.x;
        if (dX < 0 && m_FacingRight)
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
        currentState = EnemyState.ATTACKING;
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState = EnemyState.MOVING;
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
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
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, rot_Z + 180);
    }


    void MoveTowardsDest()
    {
        float dX = moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        Vector3 targetVelocity = new Vector2(dX, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, .05f);

        if (dX < 0 && m_FacingRight)
        {
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
}
