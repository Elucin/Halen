using UnityEngine;
using System.Collections;

public class AISniper : AIBase {

    public Transform HeadBone;
    public LargeShot largeShot;
    public Transform ShotEmitterTrans;
    private float shotDelay = 3.0f;

    //Sniper States
    private int readyState;
    private int moveState;
    private int idleState;
    private int shootState;
    private int reloadState;

    private float shootCooldownStart;
    private int currentAIFireState;
    private int aimingWeight;
    //Sniper Paramters
    private int rangeCountInt;
    private int playerCloseBool;
    protected static int SniperCount = 0;

	public ParticleSystem MuzzleFlash;

	public AudioClip shot;
	public AudioSource CurrentSound;

    // Use this for initialization
    protected override void Start () {
        transform.name = "Sniper-" + SniperCount++.ToString();
        base.Start();
        Name = transform.name.Split('-');
        basePoints = 300;
        //Initialise Gunner States
        readyState = Animator.StringToHash("States.Ready");
        moveState = Animator.StringToHash("States.Locomotion");
        idleState = Animator.StringToHash("States.Idle");
        shootState = Animator.StringToHash("Gun.Shoot");
        reloadState = Animator.StringToHash("Gun.Reload");

        //Initialise Gunner Parameters
        rangeCountInt = Animator.StringToHash("rangeCount");
        playerCloseBool = Animator.StringToHash("playerClose");
        meshAgent.acceleration = 15f;
        shootCooldownStart = Time.time;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        //anim.SetFloat(speedFloat, GetComponent<Rigidbody>().velocity.magnitude);
        if (currentAIState == idleState)
        {
            DetectPlayer();
        }
        else if(currentAIState == readyState)
        {
            Shoot();
        }
        else if(currentAIState == moveState)
        {
            
            StartCoroutine(Retreat());
        }

        if (triggerCount > 1)
        {
            anim.SetBool(playerCloseBool, true);
            meshAgent.Resume();
        }
        else if (triggerCount <= 1)
        {
            anim.SetBool(playerCloseBool, false);
            meshAgent.Stop();
        }
    }

    void LateUpdate()
    {
        if(triggerCount <= 1 && currentAIState != idleState)
            HeadBone.LookAt(halen.transform.position + new Vector3(0, 1, 0));
    }

    void Shoot()
    {
        if(Time.time - shootCooldownStart >= shotDelay)
        {
            shootCooldownStart = Time.time;
            LargeShot newShot = Instantiate(largeShot, ShotEmitterTrans.position, Quaternion.identity) as LargeShot;
            newShot.GetComponent<ParticleSystem>().startColor = new Color(184f/255f, 100f/255f, 234f / 255f);
			newShot.GetComponent<LargeShot>().emitter = ShotEmitterTrans;
            newShot.GetComponent<LargeShot>().bulletSpeed = 200f;
			MuzzleFlash.Play ();
			CurrentSound.PlayOneShot (shot, 1);
        }
    }


    IEnumerator Retreat()
    {
        bool moving = false;
        float degrees = 10f;
        Vector3 checkVector = (transform.position - halenPos).normalized;
        int c = 0;
        while (moving == false && c < 36)
        {
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
                c++;
            }
        }
        meshAgent.SetDestination(transform.position + checkVector);

        yield return new WaitForSeconds(0.3f); ;

    }
    /*
    protected void Retreat()
    {
        if (health > 0)
        {
            meshAgent.SetDestination(transform.position - (halen.transform.position - transform.position));
            //transform.LookAt(halen.transform, Vector3.up);
        }
    }*/


}
