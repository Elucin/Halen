﻿using UnityEngine;
using System.Collections;

public class AIGunner : AIBase
{
    const float runSpeed = 3.5f;
    const float walkSpeed = 1.5f;
    const float shootDelay = 0.05f;
    public Transform ShotEmitterTrans;
    public SmallShot smallShot;

    //Gunner States
    private int patrolState;
    private int moveState;
    private int diveState;
    private int avoidState;
    private int idleState;
    private int shootState;
    private int aimState;
    private float shootCooldownStart;
    private int currentAIWeaponState;
    private int aimingWeight;
    //Gunner Paramters
    private int rangeCountInt;
    protected static int GunnerCount = 0;

	public ParticleSystem GunnerMuzzleFlash;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        transform.name = "Gunner-" + GunnerCount++.ToString();
        Name = transform.name.Split('-');
        basePoints = 150;
        //Initialise Gunner States
        patrolState = Animator.StringToHash("States.Patrol");
        moveState = Animator.StringToHash("States.Move");
        diveState = Animator.StringToHash("States.Dive");
        avoidState = Animator.StringToHash("States.Avoid");
        idleState = Animator.StringToHash("States.Idle");
        shootState = Animator.StringToHash("States2.Shoot");
        aimState = Animator.StringToHash("States2.Aim");

        //Initialise Gunner Parameters
        rangeCountInt = Animator.StringToHash("rangeCount");

        shootCooldownStart = Time.time;

       
            
    }

    // Update is called once per frame
    protected override void Update()
    {
		if (IsGrounded () && GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled != true) {
			GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = true;
			GetComponent<Animator> ().applyRootMotion = true;
		}
        base.Update();
        currentAIWeaponState = anim.GetCurrentAnimatorStateInfo(2).fullPathHash;
        anim.SetInteger(rangeCountInt, triggerCount);
        //Debug.Log(triggerCount);
        if (currentAIState == patrolState)
        {
            meshAgent.speed = walkSpeed;
            Patrol();
            DetectPlayer();
        }
        else if (currentAIState == moveState)
        {
            if (triggerCount == 0)
            {
                meshAgent.speed = runSpeed;
                Move();
            }
            else if (triggerCount == 1)
            {
                meshAgent.speed = walkSpeed;
                Move();
            }
            else if (triggerCount == 3)
            {
                meshAgent.speed = walkSpeed;
                Retreat();
            }
        }

        if(currentAIWeaponState == shootState && health > 0)
        {
            Shoot();
        }

        if (currentAIWeaponState == shootState || currentAIWeaponState == aimState)
            aimingWeight = 1;
        else
            aimingWeight = 0;


    }

    protected void Shoot()
    {

        if (Time.time - shootCooldownStart >= shootDelay )
        {
            shootCooldownStart = Time.time;
			GunnerMuzzleFlash.Play ();
			SmallShot newShot = Instantiate(smallShot, ShotEmitterTrans.position, Quaternion.identity) as SmallShot;
            newShot.GetComponent<ParticleSystem>().startColor = new Color(108f / 255f, 103f/255f, 227f / 255f);
            newShot.GetComponent<SmallShot>().emitter = ShotEmitterTrans;
            newShot.GetComponent<SmallShot>().bulletSpeed = 50f;
        }
    }

    protected void Retreat()
    {
        if (health > 0)
        {
            meshAgent.updateRotation = false;
            meshAgent.SetDestination(transform.position - transform.forward);
            Vector3 halenGroundPos = PlayerControl.position - transform.position;
            halenGroundPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            //transform.LookAt(halen.transform, Vector3.up);
        }
    }
    void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(aimingWeight);
        anim.SetLookAtPosition(halen.transform.position + new Vector3(0,0.9f,0));

        //anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, aimingWeight);
        //anim.SetIKHintPosition(AvatarIKHint.LeftElbow, target.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, aimingWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, halen.transform.position + new Vector3(0,0.9f,0));

    }

	public bool IsGrounded() {
		RaycastHit hit;
		float offset = GetComponent<CapsuleCollider> ().height / 2;
		return Physics.Raycast (transform.position + Vector3.up * offset, -transform.up, out hit, offset + 0.01f); 
		//Debug.DrawLine(transform.position + new Vector3(0, distToGround, 0), (transform.position + new Vector3(0, distToGround, 0)) - Vector3.up * (distToGround + 0.1f));
		//return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
	}

}