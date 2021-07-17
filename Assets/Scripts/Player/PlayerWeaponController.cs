using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Instance;

    public Transform attackPoint;
    public PlayerStats playerStats;
    public GameObject projectile;
    public float shootVel;
    public int attackDamage;
    float lastFire;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireAtMouse()
    {

        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        pos = Camera.main.ScreenToWorldPoint(pos);

        Quaternion q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
        GameObject proj = Instantiate(projectile, attackPoint.position, q);
        proj.GetComponent<Rigidbody2D>().AddForce(proj.transform.up * 500.0f);
        proj.GetComponent<Projectile>().damage = attackDamage;
    }

    public void AttemptFire()
    {
        if(Time.time - playerStats.attackTime >= lastFire)
        {
            bool hasBullet = PlayerInventory.Instance.ExpendAmmo();
            if (!hasBullet) return;
            FireAtMouse();
            lastFire = Time.time;
        }
    }
}
