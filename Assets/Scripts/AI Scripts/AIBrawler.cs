using UnityEngine;
using System.Collections;

public class AIBrawler : AIBase {

    float runSpeed = 7f;
    float walkSpeed = 2f;

    //Brawler States
    private int patrolState;
    private int moveState;
    private int diveState;
    private int avoidState;
    private int idleState;
    private int aimState;
    private int attackState;
    private int closeAttackState;
    private int stunState;
    private int nullAttackState;
    private float flashTimer;
    private const float flashTime = 0.1f;
    private bool attacked = false;
    private bool didDamage = false;

    //Brawler Paramters
    private int inRangeBool;
    private int closeAttackBool;
    protected static int BrawlerCount = 0;
    protected int currentAttackState;
    protected int currentStunState;
    public ParticleSystem AttackParticle;

	// Use this for initialization
	protected override void Start () {
        runSpeed += Random.Range(-2f, 2f);
        transform.name = "Brawler-" + BrawlerCount++.ToString();
        base.Start(); 
        //Name = transform.name.Split('-');
        basePoints = 100;
        //GetComponent<MeshRenderer>().material.color = Color.clear;
        flashTimer = Time.time;
        //Initialise Brawler States
        patrolState = Animator.StringToHash("Base.Patrol");
        moveState = Animator.StringToHash("Base.Move");
        diveState = Animator.StringToHash("Base.Dive");
        avoidState = Animator.StringToHash("Base.Avoid");
        idleState = Animator.StringToHash("Base.Idle");
        aimState = Animator.StringToHash("Attack.Aim");
        attackState = Animator.StringToHash("Attack.Attack");
        closeAttackState = Animator.StringToHash("Attack.CloseAttack");
        nullAttackState = Animator.StringToHash("Attack.None");
        stunState = Animator.StringToHash("Stunned.Stunned");

        //Initialise Brawler Parameters
        inRangeBool = Animator.StringToHash("inRange");
        closeAttackBool = Animator.StringToHash("CloseAttack");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (count == updateCount)
        {
            ActuallyUpdate();
        }
        else
        {
            ++count;
        }
        if (count >= 10)
            count = 0;
    }

    void ActuallyUpdate()
    {
        base.Update();

        if (distanceToPlayer < 2)
            triggerCount = 2;
        else if (distanceToPlayer < 6)
            triggerCount = 1;
        else
            triggerCount = 0;

        currentAttackState = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;
        currentStunState = anim.GetCurrentAnimatorStateInfo(2).fullPathHash;
        if (IsGrounded() && GetComponent<UnityEngine.AI.NavMeshAgent>().enabled != true)
        {
            meshAgent.enabled = true;
            anim.applyRootMotion = true;
        }

        
        if (health > 0 && currentStunState != stunState)
        {
            anim.SetBool(inRangeBool, triggerCount >= 1 && !PlayerControl.isDead && anim.GetBool(alertBool));
            anim.SetBool(closeAttackBool, triggerCount == 2 && !PlayerControl.isDead && anim.GetBool(alertBool));

            if (currentAttackState == attackState || currentAttackState == closeAttackState)
            {
                if (!attacked)
                {
                    AttackParticle.Play();
                    attacked = true;
                }
            }
            else if (currentAttackState == aimState)
            {
                meshAgent.acceleration = 100f;
                meshAgent.Stop();
                meshAgent.acceleration = 8f;
                meshAgent.updateRotation = false;
                Vector3 halenGroundPos = PlayerControl.halenGO.transform.position + (PlayerControl.halenGO.transform.forward * PlayerControl.Speed / Random.Range(3.5f, 5.5f)) - transform.position;
                halenGroundPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
            else if (currentAttackState == closeAttackState)
            {
                meshAgent.updateRotation = false;
                Vector3 halenGroundPos = PlayerControl.halenGO.transform.position + (PlayerControl.halenGO.transform.forward * PlayerControl.Speed / Random.Range(3.5f, 5.5f)) - transform.position;
                halenGroundPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
            else if (currentAttackState == nullAttackState)
            {
                meshAgent.updateRotation = true;
                didDamage = false;
                attacked = false;
                if (currentBaseState == patrolState)
                {
                    meshAgent.speed = walkSpeed;
                    Patrol();
                    DetectPlayer();
                }
                else if (currentBaseState == idleState)
                {
                    meshAgent.speed = 0;
                    DetectPlayer();
                }
                else if (currentBaseState == moveState)
                {
                    meshAgent.speed = runSpeed;
                    Move();
                }
            }
        }
    }
    void OnCollisionEnter(Collision c)
    {
        OnChildCollisionEnter(c);
    }

    public void OnChildCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("Player"))
        {
            if ((currentAttackState == attackState || currentAttackState == closeAttackState) && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.1f && anim.GetCurrentAnimatorStateInfo(1).normalizedTime < 0.3f && !didDamage)
            {
                if (currentAttackState == attackState)
                    PlayerControl.playerControl.damageBuffer += 50;
                else
                    PlayerControl.playerControl.damageBuffer += 30;

                didDamage = true;
            }
        }
    }

	
}
