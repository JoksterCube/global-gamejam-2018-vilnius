using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : LivingEntity
{
    [Header("Attack")]
    public float attackReach = 1f;
    public float attackChargeDistance = 1f;
    public float attackDuration = .5f;
    public float attackCooldown = .75f;
    public int attackDamage = 1;
    [Header("Movement")]
    public float movementSpeed = 3f;
    public float turnSpeed = 10f;
    public float range = 10f;
    [Header("Search")]
    public float lookingInterval = 1f;

    [HideInInspector]
    public Protectee targetProtectee;
    [HideInInspector]
    public Player targetPlayer;
    [HideInInspector]
    public EnemyManager enemyManager;

    bool isAttacking = false;
    Transform target;
    Rigidbody rb;

    float nextAttack = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("GetNewProtectee", 0, lookingInterval);
    }

    protected override void Update()
    {
        base.Update();
        if (targetProtectee == null)
        {
            GetNewProtectee();
        }
        if (targetPlayer == null)
        {
            targetPlayer = Player.Instance;
        }
        if (!isAttacking)
        {
            PerformAction();
        }

        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    void PerformAction()
    {
        float distTarget = float.MaxValue;
        float distPlayer = float.MaxValue;

        if (targetProtectee != null)
        {
            distTarget = Vector3.Distance(transform.position, targetProtectee.transform.position);
        }
        if (targetPlayer != null)
        {
            distPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);
        }

        if (distTarget <= attackReach)
        {
            Attack(targetProtectee);
        }
        else if (distPlayer <= attackReach)
        {
            Attack(targetPlayer);
        }
        else if (targetProtectee != null && distTarget > attackReach && distTarget <= range)
        {
            MoveToTarget(targetProtectee.transform);
        }
        else if (targetPlayer != null && distPlayer > attackReach && distPlayer <= range)
        {
            MoveToTarget(targetPlayer.transform);
        }
        else { }
    }

    void MoveToTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (target == null) { return; }
        if (!isAttacking)
        {
            Vector3 direction = target.position - transform.position;
            Vector3 velocity = direction.normalized * movementSpeed;

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void GetNewProtectee()
    {
        //Debug.Log("Looking");
        if (enemyManager != null)
        {
            targetProtectee = enemyManager.GetNearestProtectee(transform.position);
        }
    }

    void Attack(IDamagable target)
    {
        isAttacking = true;
        StartCoroutine(Cooldown());
        target.TakeDamage(attackDamage);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackDuration + attackCooldown);
        isAttacking = false;
        yield return null;
    }

    //IEnumerator AttackCoroutine(IDamagable target)
    //{
    //    if (target is LivingEntity)
    //    {
    //        Debug.Log("ok");
    //        Transform targetT = ((LivingEntity)target).transform;


    //        float percent = 0;
    //        float speed = 1 / attackDuration;

    //        //Vector3 attackDir = (targetT.position - transform.position).normalized;
    //        //Vector3 attackDest = attackDir * attackChargeDistance + rb.position;
    //        //attackDest.y = rb.position.y;
    //        //Vector3 attackStart = rb.position;
    //        //rb.isKinematic = true;
    //        //while (percent < 1)
    //        //{
    //        //    percent += Time.deltaTime * speed;
    //        //    transform.localPosition = Vector3.Lerp(attackStart, attackDest, percent);
    //        yield return null;
    //        //}

    //        //percent = 0;
    //        //speed = 1 / attackCooldown;

    //        //while (percent < 1)
    //        //{
    //        //    percent += Time.deltaTime * speed;
    //        //    transform.localPosition = Vector3.Lerp(attackDest, attackStart, percent);
    //        //    yield return null;
    //        //}
    //        //rb.isKinematic = false;
    //        target.TakeDamage(attackDamage);
    //    }
    //    isAttacking = false;
    //}
}
