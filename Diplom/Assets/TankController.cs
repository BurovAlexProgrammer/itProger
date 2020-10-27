using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public static TankController instance;

    [SerializeField]
    float accForce = 1000;
    Rigidbody rigidbody;

    [SerializeField]
    GameObject target = null;

    [SerializeField]
    LookAt tower = null, cannon = null;

    [Header("Fire")]
    [SerializeField]
    GameObject shellPrefab = null;
    [SerializeField]
    GameObject firePoint = null;
    [SerializeField]
    float fireRate = 0.5f;
    [SerializeField]
    float fireSpeed = 100f;
    [SerializeField]
    float explosiveForce = 30f;
    [SerializeField]
    GameObject shellContainer = null;
    bool allowFire = true;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("TankController found dublicate.");
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        SetTarget(target);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0 && allowFire)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        allowFire = false;
        var shell = Instantiate(shellPrefab, firePoint.transform.position, firePoint.transform.rotation, shellContainer.transform);
        var shellRigid = shell.AddComponent<Rigidbody>();
        shellRigid.useGravity = false;
        var shellCollider = shell.AddComponent<MeshCollider>();
        shellCollider.convex = true;
        shellRigid.AddRelativeForce(Vector3.forward * fireSpeed);
        //shell.AddComponent<>
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            tower.enabled = true;
            cannon.enabled = true;
            tower.SetTarget(newTarget);
            cannon.SetTarget(newTarget);
        }
        else
        {
            tower.enabled = false;
            cannon.enabled = false;
        }
        
    }
}
