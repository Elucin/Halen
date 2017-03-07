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
        patrolState = Animator.StringToHash("States.Patrol");
        moveState = Animator.StringToHash("States.Move");
        diveState = Animator.StringToHash("States.Dive");
        avoidState = Animator.StringToHash("States.Avoid");
        idleState = Animator.StringToHash("States.Idle");
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
        currentAttackState = anim.GetCurrentAnimatorStateInfo(2).fullPathHash;
        currentStunState = anim.GetCurrentAnimatorStateInfo(3).fullPathHash;
		if (IsGrounded () && GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled != true) {
			meshAgent.enabled = true;
			anim.applyRootMotion = true;
		}

        base.Update();
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
            else if(currentAttackState == aimState)
            {
                meshAgent.acceleration = 100f;
                meshAgent.Stop();
                meshAgent.acceleration = 8f;
                meshAgent.updateRotation = false;
                Vector3 halenGroundPos = halen.transform.position + (halen.transform.forward * PlayerControl.Speed) - transform.position;
                halenGroundPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
            else if(currentAttackState == closeAttackState)
            {
                meshAgent.updateRotation = false;
                Vector3 halenGroundPos = halen.transform.position + (halen.transform.forward * PlayerControl.Speed) - transform.position;
                halenGroundPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
            else if(currentAttackState == nullAttackState)
            {
                meshAgent.updateRotation = true;
                didDamage = false;
                attacked = false;
                if (currentAIState == patrolState)
                {
                    meshAgent.speed = walkSpeed;
                    Patrol();
                    DetectPlayer();
                }
                else if (currentAIState == idleState)
                {
                    meshAgent.speed = 0;
                    DetectPlayer();
                }
                else if (currentAIState == moveState)
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
       
        if ((currentAttackState == attackState || currentAttackState == closeAttackState) && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.1f && anim.GetCurrentAnimatorStateInfo(2).normalizedTime < 0.3f && !didDamage && c.transform.CompareTag("Player"))
        {
            if (currentAttackState == attackState)
                halen.GetComponent<PlayerControl>().damageBuffer += 60;
            else
                halen.GetComponent<PlayerControl>().damageBuffer += 40;

            didDamage = true;
        }
    }

	
}
