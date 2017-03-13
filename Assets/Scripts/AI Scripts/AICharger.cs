﻿using UnityEngine;
using System.Collections;

public class AICharger : AIBase {

    const float runSpeed = 3.5f;
    const float walkSpeed = 1f;
    const float chargeSpeed = 15f;
    const float walkAngularSpeed = 120f;
    const float chargeAngularSpeed = 30f;
    
    bool dealSmashDamage = true;

    //Charger States
    private int patrolState;
    private int moveState;
    private int chargeAimState;
    private int chargeChargeState;
    private int chargeRecoverState;
    private int smashAimState;
    private int smashExecuteState;
    private int aimState;
    private int recoverState;
    private int idleState;

    //Paramaters
    private int smashBool;
    private int inRangeBool;
    private int lineOfSightBool;
    private int stopChargeTrigger;
    private int hitWallTrigger;

    public Transform leftFin;
    public Transform rightFin;
    private float backPanelsTimer;
    private float backPanelsCycleTime = 3.0f;
    private bool finsOpen = false;
    Vector3 leftOpen;
    Vector3 leftClosed;
    Vector3 rightOpen;
    Vector3 rightClosed;
    public GameObject weakSpot;
    protected static int ChargerCount = 0;

	public ParticleSystem DustTrail;
	public ParticleSystem Smash;

    // Use this for initialization
    protected override void Start () {
        transform.name = "Charger-" + ChargerCount++.ToString();
        base.Start();
        //Name = transform.name.Split('-');
        basePoints = 500;
        patrolState = Animator.StringToHash("Base.Patrol");
        moveState = Animator.StringToHash("Base.Move");
        chargeAimState = Animator.StringToHash("Base.Aim");
        chargeChargeState = Animator.StringToHash("Base.Charge");
        chargeRecoverState = Animator.StringToHash("Base.Recover");
        smashAimState = Animator.StringToHash("Base.Smash_Aim");
        smashExecuteState = Animator.StringToHash("Base.Smash_Execute");
        aimState = Animator.StringToHash("Base.Aim");
        recoverState = Animator.StringToHash("Base.Recover");
        idleState = Animator.StringToHash("Base.Idle");

        smashBool = Animator.StringToHash("Smash");
        inRangeBool = Animator.StringToHash("inRange");
        lineOfSightBool = Animator.StringToHash("LineOfSight");
        stopChargeTrigger = Animator.StringToHash("StopCharge");
        hitWallTrigger = Animator.StringToHash("HitWall");
        StartCoroutine(DelayGetLineOfSight());
    }
	
	// Update is called once per frame
	protected override void Update () {
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
        if (IsGrounded() && !meshAgent.enabled)
        {
            meshAgent.enabled = true;
            anim.applyRootMotion = true;
        }

        base.Update();

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
            if (DustTrail.isPlaying)
            {
                DustTrail.Stop();
            }
            dealSmashDamage = true;
            meshAgent.speed = runSpeed;
            destination = halen.transform.position;
        }
        else if (currentBaseState == smashAimState)
        {
            if (!Smash.isPlaying)
            {
                Smash.Play();
            }
            meshAgent.speed = 0;
            meshAgent.updateRotation = false;
            Vector3 halenGroundPos = halen.transform.position - transform.position;
            halenGroundPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6);
            meshAgent.updateRotation = true;
        }
        else if (currentBaseState == chargeAimState)
        {
            meshAgent.speed = 0;
            meshAgent.updateRotation = false;
            Vector3 halenGroundPos = halen.transform.position - transform.position;
            halenGroundPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            meshAgent.updateRotation = true;
        }
        else if (currentBaseState == chargeChargeState)
        {
            if (!DustTrail.isPlaying)
            {
                DustTrail.Play();
            }

            destination = halen.transform.position;
            meshAgent.speed = chargeSpeed;
            meshAgent.angularSpeed = chargeAngularSpeed;
            if (GetAngleToPlayer()) { anim.SetTrigger(stopChargeTrigger); }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, 3f, LayerMasks.terrainOnly, QueryTriggerInteraction.Ignore))
                    if (hit.transform.CompareTag("Terrain")) { anim.SetTrigger(hitWallTrigger); meshAgent.acceleration = 100f; meshAgent.speed = 0; }
            }
        }
        else if (currentBaseState == chargeRecoverState)
        {
            anim.ResetTrigger(stopChargeTrigger);
            meshAgent.speed = 0;
            meshAgent.angularSpeed = walkAngularSpeed;
        }
        anim.SetBool(inRangeBool, triggerCount >= 1);
        anim.SetBool(smashBool, triggerCount == 2);
    }

    IEnumerator DelayGetLineOfSight()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(lineOfSightBool, GetLineOfSight());
        StartCoroutine(DelayGetLineOfSight());
    }
    protected override bool IsGrounded()
    {
        RaycastHit hit;
        float offset = capsuleCollider.height / 2;
        return Physics.Raycast(transform.position + Vector3.up * offset, -transform.up, out hit, offset + 0.01f);
    }

    public void OnChildCollisionEnter(Collision c)
    {
        if (currentBaseState == smashExecuteState)
        {
            if (c.transform.CompareTag("Player") && dealSmashDamage)
            {
                dealSmashDamage = false;
                playerControl.damageBuffer += 80;
                c.rigidbody.AddForce(transform.TransformDirection(new Vector3(0, 500, 1000)), ForceMode.Impulse);
            }
            else if (c.transform.CompareTag("Enemy"))
                c.gameObject.GetComponent<AIBase>().health = 0;
            
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if(currentBaseState == chargeChargeState)
        {
            if (c.transform.CompareTag("Player") && dealSmashDamage)
            {
                dealSmashDamage = false;
                playerControl.damageBuffer += 75;

                Vector3 collisionDir = c.contacts[0].normal;
                collisionDir.y = Mathf.Abs(collisionDir.y);
                collisionDir.z *= -1;
                c.rigidbody.AddForce((collisionDir * 2000f + new Vector3(0, 500, 0)), ForceMode.Impulse);

            }
            else if (c.transform.CompareTag("Enemy"))
            {
                c.gameObject.GetComponent<AIBase>().health = 0;
                c.gameObject.GetComponent<AIBase>().stylePoints.deathType = "ChargerSmash";
            }
        }

    }

    bool GetLineOfSight()
    {
        RaycastHit hit;
        //Physics.SphereCast(transform.position + Vector3.up, 0.5f, transform.TransformDirection(Vector3.forward), out hit, 100f, LayerMasks.ignoreEnemies, QueryTriggerInteraction.Ignore);
        Physics.SphereCast(transform.position + Vector3.up + transform.forward, 0.4f, (halen.transform.position - transform.position + Vector3.up).normalized, out hit, 100f, LayerMasks.terrainAndPlayer, QueryTriggerInteraction.Ignore);

        if (hit.transform == null)
            return false;
        if (hit.transform.CompareTag("Player"))
            return true;
        else
            return false;
    }

    bool GetAngleToPlayer()
    {
        return Mathf.Abs(Vector3.Angle(transform.TransformDirection(Vector3.forward), halen.transform.position - transform.position)) > 100;
    }

    public bool IsCharging()
    {
        return currentAIState == chargeChargeState;
    }
}
