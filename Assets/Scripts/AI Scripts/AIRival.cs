using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIRival : AIBase {

    const float MIN_TELEPORT_DISTANCE = 20f;
    const float TELEPORT_DELAY = 3.0f;

    public GameObject teleportInParticles;
    public GameObject teleportOutParticles;

    bool teleporting = false;

    public Transform[] teleportLocation;
    public Transform teleportHub;
    protected int playerInRangeBool;
    protected int lightShotsBool;
    protected int heavyShotTrig;
    protected int playerCloseTrig;

    protected static int teleportStartState;
    protected static int teleportEndState;
    protected static int moveState;
    protected static int blockState;
    protected static int dodgeState;
    protected static int waitState;
    protected static int attackState;
   
    private int currentAttackState;

    const float shootDelay = 0.1f;
    public Transform LeftGunTrans;
    public Transform RightGunTrans;
    public SmallShot RivalShot;
    private float shootCooldownStart;
    private bool currentGun;

	public ParticleSystem RivalMuzzleFlash;



    // Use this for initialization
    protected override void Start () {
        base.Start();
        playerInRangeBool = Animator.StringToHash("PlayerInRange");
        heavyShotTrig = Animator.StringToHash("HeavyShotIncoming");
        playerCloseTrig = Animator.StringToHash("PlayerClose");

        teleportStartState = Animator.StringToHash("Base.Teleport Start");
        teleportEndState = Animator.StringToHash("Base.Teleport Finish");
        moveState = Animator.StringToHash("Base.Move");
        blockState = Animator.StringToHash("Base.Blocking");
        dodgeState = Animator.StringToHash("Base.Dodge");

        waitState = Animator.StringToHash("Attack.Wait");
        attackState = Animator.StringToHash("Attack.Attack");
        currentGun = false;
		GameObject.Find ("UI").GetComponent<UIScript> ().Theravall = true;
        
    }

    // Update is called once per frame
    protected override void Update () {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        currentAttackState = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;

		GameObject.Find ("Theravall_health").GetComponent<Image> ().fillAmount = health / 100f;

        if (health <= 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);

        if (!IsGrounded() && !meshAgent.isOnOffMeshLink)
        {
            Debug.Log("Not Grounded");
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
        if(Vector3.Distance(transform.position, halenPos)<10)
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

        if (currentAttackState == attackState && !teleporting)
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
        
        if (triggerCount > 1)
        {
            anim.SetTrigger(playerCloseTrig);
            GameObject TeleportExit = Instantiate(teleportOutParticles, transform.position, Quaternion.identity) as GameObject;
            meshAgent.Warp(teleportHub.position);
            Teleport();
        }


        
	}

    void Teleport()
    {
        if (!teleporting)
        {
            int selectedLocation = Random.Range(0, teleportLocation.Length);
            if (checkDistance(teleportLocation[selectedLocation]))
            {
                StartCoroutine(doWarp(teleportLocation[selectedLocation]));
            }
            else
                Teleport();
        }

    }

    IEnumerator doWarp(Transform location)
    {
        teleporting = true;
        GameObject TeleportEnter = Instantiate(teleportInParticles, location.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(TELEPORT_DELAY);
        meshAgent.Warp(location.position);
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
			
			if (transform.worldToLocalMatrix.MultiplyPoint (point.position).x < 0) {
				GetComponent<Rigidbody> ().AddForce (new Vector3 (-3000, 0, 0), ForceMode.Impulse);
			} else {
				GetComponent<Rigidbody> ().AddForce (new Vector3 (3000, 0, 0), ForceMode.Impulse);
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
        Debug.DrawLine(transform.position, transform.position - Vector3.up * (offset + 0.32f));
        return Physics.Raycast(transform.position, -Vector3.up, out hit, offset + 0.7f);

        //return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
    }
}

