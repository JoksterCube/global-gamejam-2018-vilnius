using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Protectee : LivingEntity
{
    public float movementSpeed = 2f;
    public float turnSpeed = 10f;

    private Transform target;
    private int waypointIndex = 0;

    public ProtecteeManager protecteeManager;

    Rigidbody rb;

    public new event System.Action<Protectee> OnDeath;

    public event System.Action<Protectee> OnFinish;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
        if (protecteeManager == null || protecteeManager.waypoints == null) { return; }
        target = protecteeManager.waypoints.points[0];
    }

    private void FixedUpdate()
    {
        if (target == null) { return; }
        Vector3 direction = target.position - transform.position;
        Vector3 velocity = direction.normalized * movementSpeed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    protected override void Update()
    {
        base.Update();
        if (protecteeManager == null) { return; }
        if (target == null)
        {
            target = protecteeManager.GetNextWaypoint(ref waypointIndex);
            if (target == null) { return; }
        }

        if (Utility.Approximate(transform.position, target.position, 0.5f))
        {
            target = protecteeManager.GetNextWaypoint(ref waypointIndex);
        }

        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void Finish()
    {
        if (OnFinish != null)
        {
            OnFinish(this);
        }
        Destroy(gameObject);
    }

    public override void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath(this);
        }
        Destroy(gameObject);
    }
}
