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

    [FMODUnity.EventRef]
    public string FireProjectileEvent;

    [FMODUnity.EventRef]
    public string NoAmmoEvent;

    FMOD.Studio.EventInstance PE;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        PE = FMODUnity.RuntimeManager.CreateInstance(FireProjectileEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PE, transform, GetComponent<Rigidbody2D>());
        PE.release();
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

        FMODUnity.RuntimeManager.PlayOneShotAttached(FireProjectileEvent, gameObject);
    }

    public void AttemptFire()
    {
        if(Time.time - playerStats.attackTime >= lastFire)
        {
            bool hasBullet = PlayerInventory.Instance.ExpendAmmo();
            if (!hasBullet)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(NoAmmoEvent, gameObject);
                return;
            }
            FireAtMouse();
            lastFire = Time.time;
        }
    }
}
