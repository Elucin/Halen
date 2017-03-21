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
    private int meleeState;
    private float shootCooldownStart;
    private int currentAIWeaponState;
    private int currentAIMeleeState;
    private int aimingWeight;
    //Gunner Paramters
    private int rangeCountInt;
    protected static int GunnerCount = 0;

	public ParticleSystem GunnerMuzzleFlash;
    bool didMeleeDamage = false;

	public AudioClip shootSFX;
	public AudioClip reloadSFX;
	public AudioSource CurrentSound;

    // Use this for initialization
    protected override void Start()
    {
        transform.name = "Gunner-" + GunnerCount++.ToString();
        base.Start();
        basePoints = 150;
        //Initialise Gunner States
        patrolState = Animator.StringToHash("Base.Patrol");
        moveState = Animator.StringToHash("Base.Move");
        diveState = Animator.StringToHash("Base.Dive");
        avoidState = Animator.StringToHash("Base.Avoid");
        idleState = Animator.StringToHash("Base.Idle");
        shootState = Animator.StringToHash("States2.Shoot");
        aimState = Animator.StringToHash("States2.Aim");
        meleeState = Animator.StringToHash("Melee.Melee");
        //meleeRecoverState = Animator.StringToHash("Melee.MeleeRecover");

        //Initialise Gunner Parameters
        rangeCountInt = Animator.StringToHash("rangeCount");

        shootCooldownStart = Time.time;
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
        if (IsGrounded() && meshAgent.enabled != true)
        {
            meshAgent.enabled = true;
            anim.applyRootMotion = true;
        }
        base.Update();

        if (distanceToPlayer < 3f)
        {
            triggerCount = 4;
        }
        else if (distanceToPlayer < 30f)
        {
            triggerCount = 3;
        }
        else if (distanceToPlayer < 60f)
        {
            triggerCount = 2;
        }
        else if (distanceToPlayer < 80f)
        {
            triggerCount = 1;
        }
        else
            triggerCount = 0;

        currentAIWeaponState = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;
        currentAIMeleeState = anim.GetCurrentAnimatorStateInfo(2).fullPathHash;
        anim.SetInteger(rangeCountInt, triggerCount);

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
            else if (triggerCount >= 3)
            {
                meshAgent.speed = walkSpeed;
                Retreat();
            }

            if (triggerCount <= 3 && !PlayerControl.isDead)
                aimingWeight = 1;
            else
                aimingWeight = 0;
        }

        if (currentAIWeaponState == shootState && health > 0 && currentAIMeleeState != meleeState && triggerCount < 4)
        {
            Shoot();
        }

        if(currentAIMeleeState != meleeState)
        {
            didMeleeDamage = false;
        }

        /*
        if (currentAIWeaponState == shootState || currentAIWeaponState == aimState && currentAIMeleeState != meleeState && !anim.IsInTransition(3) && rangeCountInt < 4)
            aimingWeight = 1;
        else
            aimingWeight = 0;
            */
    }

    protected void Shoot()
    {

		if (Time.time - shootCooldownStart >= shootDelay) {
			shootCooldownStart = Time.time;
			GunnerMuzzleFlash.Play ();
			SmallShot newShot = Instantiate (smallShot, ShotEmitterTrans.position, Quaternion.identity) as SmallShot;
			newShot.GetComponent<SmallShot> ().emitter = ShotEmitterTrans;
			newShot.GetComponent<SmallShot> ().bulletSpeed = 50f;
			CurrentSound.pitch = 0.8f;
			CurrentSound.PlayOneShot (shootSFX, 1.5f);
		}
    }

    protected void Retreat()
    {
        if (health > 0)
        {
            meshAgent.updateRotation = false;
            destination = transform.position - transform.forward;
            Vector3 halenGroundPos = PlayerControl.position - transform.position;
            halenGroundPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            //transform.LookAt(PlayerControl.halenGO.transform, Vector3.up);
        }
    }
    void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(aimingWeight);
        anim.SetLookAtPosition(PlayerControl.halenGO.transform.position + new Vector3(0,0.9f,0));

        //anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, aimingWeight);
        //anim.SetIKHintPosition(AvatarIKHint.LeftElbow, target.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, aimingWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, PlayerControl.halenGO.transform.position + new Vector3(0,0.9f,0));

    }

	public bool IsGrounded() {
		RaycastHit hit;
		float offset = GetComponent<CapsuleCollider> ().height / 2;
		return Physics.Raycast (transform.position + Vector3.up * offset, -transform.up, out hit, offset + 0.01f); 
		//Debug.DrawLine(transform.position + new Vector3(0, distToGround, 0), (transform.position + new Vector3(0, distToGround, 0)) - Vector3.up * (distToGround + 0.1f));
		//return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
	}

    public void OnChildCollisionEnter(Collider c)
    {
        if(c.CompareTag("Player") && currentAIMeleeState == meleeState && !didMeleeDamage)
        {
            didMeleeDamage = true;
            PlayerControl.playerControl.damageBuffer += 30f;
        }
    }

}
