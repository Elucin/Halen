using UnityEngine;
using System.Collections;

public class AIBrawler : AIBase {

    const float runSpeed = 3.5f;
    const float walkSpeed = 1f;

    //Brawler States
    private int patrolState;
    private int moveState;
    private int diveState;
    private int avoidState;
    private int idleState;
    private int aimState;
    private int attackState;
    private float flashTimer;
    private const float flashTime = 0.1f;
    private bool attacked = false;

    //Brawler Paramters
    private int inRangeBool;
    protected static int BrawlerCount = 0;
    
	public ParticleSystem AttackParticle;

	// Use this for initialization
	protected override void Start () {
		
        base.Start(); 
        transform.name = "Brawler-" + BrawlerCount++.ToString();
        Name = transform.name.Split('-');
        basePoints = 100;
        //GetComponent<MeshRenderer>().material.color = Color.clear;
        flashTimer = Time.time;
        //Initialise Brawler States
        patrolState = Animator.StringToHash("States.Patrol");
        moveState = Animator.StringToHash("States.Move");
        diveState = Animator.StringToHash("States.Dive");
        avoidState = Animator.StringToHash("States.Avoid");
        idleState = Animator.StringToHash("States.Idle");
        aimState = Animator.StringToHash("States.Attack.Aim");
        attackState = Animator.StringToHash("States.Attack.Attack");

        //Initialise Brawler Parameters
        inRangeBool = Animator.StringToHash("inRange");
	
    }

    // Update is called once per frame
    protected override void Update()
    {
		if (IsGrounded () && GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled != true) {
			meshAgent.enabled = true;
			anim.applyRootMotion = true;
		}
        base.Update();
        if (health > 0)
        {
            if (triggerCount == 1 && anim.GetBool(alertBool))
                anim.SetBool(inRangeBool, true);
            else
                anim.SetBool(inRangeBool, false);

            if (currentAIState == patrolState)
            {
                meshAgent.speed = walkSpeed;
                Patrol();
                DetectPlayer();
            }
            else if (currentAIState == moveState)
            {
                meshAgent.speed = runSpeed;
                Move();
            }
            else if (currentAIState == aimState)
            {
                attacked = false;
                meshAgent.Stop();
                Vector3 halenGroundPos = halen.transform.position - transform.position;
                halenGroundPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
                /*
                if (Time.time - flashTimer >= flashTime)
                {
                    
                    if (GetComponent<MeshRenderer>().material.color == Color.clear)
                        GetComponent<MeshRenderer>().material.color = Color.red;
                    else
                        GetComponent<MeshRenderer>().material.color = Color.clear;

                    flashTimer = Time.time;
                }*/
            }
            if (currentAIState == attackState)
            {
				
                //GetComponent<MeshRenderer>().material.color = Color.clear;
                if (!attacked)
                {
					AttackParticle.Play ();
                    //GetComponent<Rigidbody>().AddForce(Vector3.up * 10000f, ForceMode.Impulse);
                    attacked = true;


                }
            }
            
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if(currentAIState == attackState)
        {
            if(c.transform.tag == ("Player"))
            {
                halen.GetComponent<PlayerControl>().damageBuffer += 40;
            }

        }
    }

	
}
