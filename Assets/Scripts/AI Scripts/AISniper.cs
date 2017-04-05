using UnityEngine;
using System.Collections;

public class AISniper : AIBase {

    public Transform HeadBone;
    public LargeShot largeShot;
    public Transform ShotEmitterTrans;
    private float shotDelay = 3.0f;
    bool retreating = false;

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
        //anim.SetFloat(speedFloat, GetComponent<Rigidbody>().velocity.magnitude);

        if (distanceToPlayer < 24)
            triggerCount = 2;
        else if (distanceToPlayer < 90)
            triggerCount = 1;
        else
            triggerCount = 0;

        if (currentBaseState == idleState)
        {
            DetectPlayer();
        }
        else if (currentBaseState == readyState)
        {
            Shoot();
        }
        else if (currentBaseState == moveState)
        {
            if (!retreating)
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
        if(triggerCount <= 1 && currentBaseState != idleState)
            HeadBone.LookAt(PlayerControl.halenGO.transform.position + new Vector3(0, 1, 0));
    }

    void Shoot()
    {
        if(Time.time - shootCooldownStart >= shotDelay)
        {
            RaycastHit hit;
            Physics.Raycast(ShotEmitterTrans.position, PlayerControl.position - ShotEmitterTrans.position, out hit, 90f, LayerMasks.terrainPlayerEnemies, QueryTriggerInteraction.Ignore);
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    shootCooldownStart = Time.time;
                    LargeShot newShot = Instantiate(largeShot, ShotEmitterTrans.position, Quaternion.identity) as LargeShot;
                    newShot.GetComponent<ParticleSystem>().startColor = new Color(184f / 255f, 100f / 255f, 234f / 255f);
                    newShot.GetComponent<LargeShot>().emitter = ShotEmitterTrans;
                    newShot.GetComponent<LargeShot>().bulletSpeed = 200f;
                    MuzzleFlash.Play();
                    CurrentSound.PlayOneShot(shot, 1);
                }
            }
        }
    }

    IEnumerator Retreat()
    {
        retreating = true;
        bool moving = false;
        float degrees = 15f;
        Vector3 checkVector = Quaternion.Euler(0, Random.Range(-15f, 15f), 0) * (transform.position - PlayerControl.Position).normalized;
        Vector3 checkVectorDown = Quaternion.Euler(45, 0, 0) * checkVector;
        int c = 0;
        while (moving == false && c < 24)
        {
            if (!Physics.Raycast(transform.position + transform.up, checkVector, 5f) && Physics.Raycast(transform.position + transform.up, checkVectorDown, 3f))
            {
                moving = true;
            }
            else
            {
                if (transform.worldToLocalMatrix.MultiplyPoint(PlayerControl.Position).x > 0)
                    checkVector = Quaternion.Euler(0, degrees, 0) * checkVector;
                else
                    checkVector = Quaternion.Euler(0, -degrees, 0) * checkVector;
                checkVectorDown = Quaternion.Euler(45, 0, 0) * checkVector;
                c++;
            }
        }
        destination = transform.position + checkVector * 10;

        yield return new WaitForSeconds(1f);
        retreating = false;
    }
}
