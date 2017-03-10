using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIRival : AIBase {

    const float MIN_TELEPORT_DISTANCE = 20f;
    const float TELEPORT_DELAY = 3.0f;
    const float TELEPORT_MELEE_DELAY = 1.0f;

    public GameObject teleportInParticles;
    public GameObject teleportOutParticles;

    bool teleporting = false;
    bool startTeleport = false;

    public Transform[] teleportLocation;
    public Transform teleportHub;
    protected int playerInRangeBool;
    protected int lightShotsBool;
    protected int heavyShotTrig;
    protected int playerCloseTrig;
    protected int bossStageInt;
    protected int finishTeleportTrig;
    protected int doMeleeBool;
    protected int doMeleeTeleportBool;
    protected int xMove;
    protected int zMove;
    protected int dodgeVelocityCoefficient;

    protected static int teleportStartState;
    protected static int teleportEndState;
    protected static int moveState;
    protected static int blockState;
    protected static int dodgeState;
    protected static int waitState;
    protected static int attackState;
    protected static int meleeState;
    private int currentAttackState;

    const float shootDelay = 0.1f;
    public Transform LeftGunTrans;
    public Transform RightGunTrans;
    public SmallShot RivalShot;
    private float shootCooldownStart;
    private bool currentGun;

	public ParticleSystem RivalMuzzleFlash;

    int dodgeDirection = -1;
    

    // Use this for initialization
    protected override void Start () {
        base.Start();
        playerInRangeBool = Animator.StringToHash("PlayerInRange");
        heavyShotTrig = Animator.StringToHash("HeavyShotIncoming");
        playerCloseTrig = Animator.StringToHash("PlayerClose");
        bossStageInt = Animator.StringToHash("Stage");
        finishTeleportTrig = Animator.StringToHash("FinishTeleport");
        doMeleeBool = Animator.StringToHash("doMelee");
        doMeleeTeleportBool = Animator.StringToHash("doMeleeTeleport");
        xMove = Animator.StringToHash("X_Move");
        zMove = Animator.StringToHash("Z_Move");
        dodgeVelocityCoefficient = Animator.StringToHash("DodgeCurve");

        teleportStartState = Animator.StringToHash("Base.Teleport Start");
        teleportEndState = Animator.StringToHash("Base.Teleport Finish");
        moveState = Animator.StringToHash("Base.Move");
        blockState = Animator.StringToHash("Base.Blocking");
        dodgeState = Animator.StringToHash("Base.Dodge");
        meleeState = Animator.StringToHash("Base.Melee");

        waitState = Animator.StringToHash("Attack.Wait");
        attackState = Animator.StringToHash("Attack.Attack");
        currentGun = false;
		GameObject.Find ("UI 1").GetComponent<UIScript> ().Theravall = true;
        
    }

    // Update is called once per frame
    protected override void Update () {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        currentAttackState = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;
        if (halen == null)
            halen = GameObject.FindGameObjectWithTag("Player");
		GameObject.Find ("Theravall_health").GetComponent<Image> ().fillAmount = health / 100f;
        anim.SetFloat(xMove, transform.InverseTransformDirection(meshAgent.velocity).normalized.x);
        anim.SetFloat(zMove, transform.InverseTransformDirection(meshAgent.velocity).normalized.z);

        if (health < 100f && health > 40f)
            anim.SetInteger(bossStageInt, 2);
        else if (health <= 40f && health > 0f)
            anim.SetInteger(bossStageInt, 3);
        else if (health <= 0f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        

        if (!IsGrounded() && !meshAgent.isOnOffMeshLink)
        {
            meshAgent.enabled = false;
            GetComponent<Rigidbody>().drag = 0;
        }
        else
        {
            GetComponent<Rigidbody>().drag = 10;
            meshAgent.enabled = true;
            meshAgent.updateRotation = false;
        }

        //meshAgent.SetDestination(transform.position - transform.forward);
        if (currentBaseState == moveState)
        {
            if (Vector3.Distance(transform.position, halenPos) < 10)
            {
                StartCoroutine(Retreat());
            }
            else
            {
                Patrol();
            }
            Vector3 halenGroundPos = PlayerControl.position - transform.position;
            halenGroundPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(halenGroundPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
        else if(currentBaseState == dodgeState)
        {
            GetComponent<Rigidbody>().velocity = transform.right * dodgeDirection * 20.0f * anim.GetFloat(dodgeVelocityCoefficient);

        }

        if (currentAttackState == attackState && !teleporting && currentBaseState != teleportEndState && currentBaseState != teleportStartState)
        {

            if (currentGun)
            {
                Shoot(LeftGunTrans);
                
            }
            else
            {
                Shoot(RightGunTrans);
            }
        }

        anim.SetBool(playerInRangeBool, triggerCount > 0);
        if (triggerCount > 1 && currentBaseState != meleeState && currentBaseState != teleportEndState)
        {
            anim.SetBool(doMeleeBool, false);
            anim.SetBool(doMeleeTeleportBool, false);
            anim.SetTrigger(playerCloseTrig);
            startTeleport = true;
        }

        if(!teleporting && currentBaseState == teleportEndState && startTeleport)
        {
            startTeleport = false;
            GameObject TeleportExit = Instantiate(teleportOutParticles, transform.position + Vector3.up, Quaternion.identity) as GameObject;
            meshAgent.Warp(teleportHub.position);
            bool doMelee = false;
            if (anim.GetInteger(bossStageInt) > 1)
            {
                if (Random.Range(0, 2) == 1)
                    doMelee = true;
                anim.SetBool(doMeleeTeleportBool, true);
            }
            anim.ResetTrigger(finishTeleportTrig);
            anim.ResetTrigger(playerCloseTrig);
            Teleport(doMelee);
        }
  
	}

    void Teleport(bool doMelee)
    {
        if (!teleporting)
        {
            if (!doMelee)
            {
                int selectedLocation = Random.Range(0, teleportLocation.Length);
                if (checkDistance(teleportLocation[selectedLocation]))
                {
                    StartCoroutine(doWarp(teleportLocation[selectedLocation]));
                }
                else
                    Teleport(false);
            }
            else
            {
                int angle = 0;
                do
                {
                    Vector3 location = Quaternion.Euler(0, angle, 0) * (-halen.transform.forward * 4f);
                    if(!Physics.Raycast(PlayerControl.position + halen.transform.up, halen.transform.InverseTransformDirection(location), 4.5f))
                    {
                        StartCoroutine(doMeleeWarp(location));
                        return;
                    }
                    angle += 15;

                } while (angle < 360);
                Teleport(false);
            }
        }
    }

    IEnumerator doWarp(Transform location)
    {
        teleporting = true;
        GameObject TeleportEnter = Instantiate(teleportInParticles, location.position + Vector3.up, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(TELEPORT_DELAY);
        meshAgent.Warp(location.position);
        anim.SetTrigger(finishTeleportTrig);
        teleporting = false;
    }

    IEnumerator doMeleeWarp(Vector3 location)
    {
        if(Random.Range(0, 2) == 1)
            anim.SetBool(doMeleeBool, true);
        teleporting = true;
        GameObject TeleportEnter = Instantiate(teleportInParticles, halen.transform.TransformPoint(location), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(TELEPORT_MELEE_DELAY);
        meshAgent.enabled = false;
        transform.position = halen.transform.TransformPoint(location);
        if (anim.GetBool(doMeleeBool))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 100f, ForceMode.Impulse);
        }
        teleporting = false;
    }

    bool checkDistance(Transform location)
    {
        return (location.position - PlayerControl.position).magnitude > MIN_TELEPORT_DISTANCE;
    }


    protected void Shoot(Transform ShotTrans)
    {

        if (Time.time - shootCooldownStart >= shootDelay)
        {
            shootCooldownStart = Time.time;
			ParticleSystem newFlash = Instantiate (RivalMuzzleFlash, ShotTrans.position, Quaternion.identity) as ParticleSystem;
			newFlash.transform.rotation = ShotTrans.transform.rotation;
            SmallShot newShot = Instantiate(RivalShot, ShotTrans.position, Quaternion.identity) as SmallShot;
            newShot.GetComponent<ParticleSystem>().startColor = new Color(0, 1, 33 / 255);
            newShot.GetComponent<SmallShot>().emitter = ShotTrans;
            newShot.GetComponent<SmallShot>().bulletSpeed = 200f;
            newShot.GetComponent<SmallShot>().bulletDamage = 10f;
            currentGun = !currentGun;
        }
    }
    public void DodgeDirection(Transform point)
    {
		
        if (currentBaseState != teleportEndState)
        {
            anim.SetTrigger(heavyShotTrig);
            meshAgent.acceleration = 1000f;
            meshAgent.Stop();
            meshAgent.acceleration = 8f;
			if (transform.worldToLocalMatrix.MultiplyPoint (point.position).x < 0) {
                dodgeDirection = 1;
                //GetComponent<Rigidbody> ().AddForce (new Vector3 (-4000, 0, 0), ForceMode.Impulse);
			} else {
				//GetComponent<Rigidbody> ().AddForce (new Vector3 (4000, 0, 0), ForceMode.Impulse);
                dodgeDirection = -1;
            }
        }
    }

    IEnumerator Retreat()
    {
        bool moving = false;
        float degrees = 10f;
        Vector3 checkVector = -transform.forward;
        while (moving == false) {
            if (!Physics.Raycast(transform.position, checkVector, 5))
            {
                moving = true;       
            }
            else
            {
                if (transform.worldToLocalMatrix.MultiplyPoint(halenPos).x < 0)
                    checkVector = Quaternion.Euler(0, degrees, 0) * checkVector;
                else
                    checkVector = Quaternion.Euler(0, -degrees, 0) * checkVector;
            }
        }
        meshAgent.SetDestination(transform.position + checkVector);

        yield return new WaitForSeconds(0.3f); ;

    }
    protected override bool IsGrounded()
    {
        RaycastHit hit;
        float offset = GetComponent<CapsuleCollider>().height / 2;
        return Physics.Raycast(transform.position, -Vector3.up, out hit, offset + 0.7f);

        //return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
    }
}

