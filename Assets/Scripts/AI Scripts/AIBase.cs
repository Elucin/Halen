using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour {
    public struct StylePointsData
    {
        public string deathType;
        public float largeShotTime;
        public bool airKill;
        public bool collat;
        public bool bankshot;
    };

    public float health;
    protected UnityEngine.AI.NavMeshAgent meshAgent;
    public Object explosion;
    public GameObject patrolSet;
    protected UIScript ui;
    //Patrol Points
    Vector3 point1;
    Vector3 point2;

    //Halen Variables
    public GameObject halen;
    protected Vector3 halenPos;
    Vector3 halenDir;
    float halenSpeed;
    bool halenAlive;
    public bool Idle;
    protected int basePoints;

    //AI Parameters
    protected int alertBool;
    protected int chargerHazardBool;
    protected int rangeHazard;
    protected int playerUnreachable;
    protected int playerDead;
    protected int speedFloat;
    protected int stunnedBool;
    protected int stunTrigger;
    protected int idleBool;
    protected bool destroyed = false;

    //Other Stuff
    protected Animator anim;
    protected int currentBaseState;
    protected int currentAIState;
    public StylePointsData stylePoints;
    public int triggerCount;
    float oldHealth = 100f;
    protected string[] Name;

    //SFX
    public BrawlerSFXManager _BrawlerSFXManager;

    // Use this for initialization
    protected virtual void Start () {
        Name = transform.name.Split('-');
        ui = GameObject.Find("UI").GetComponent<UIScript>();
        stylePoints = new StylePointsData();
        triggerCount = 0;
        health = 100f;
        //Initialise Animator
        anim = GetComponent<Animator>();
        if (Name[0] != "Sniper" && Name[0] != "Floater")
            anim.SetBool(idleBool, Idle);
        //Initialize NavMeshAgent
        meshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Inititalize Halen
        halen = GameObject.Find("Halen");

        if (!transform.CompareTag("Rival"))
        {
            //Initialize Parameters
            alertBool = Animator.StringToHash("Alerted");
            chargerHazardBool = Animator.StringToHash("chargerHazard");
            rangeHazard = Animator.StringToHash("rangeHazard");
            playerUnreachable = Animator.StringToHash("playerUnreachable");
            playerDead = Animator.StringToHash("playerDead");
            speedFloat = Animator.StringToHash("speed");
            stunTrigger = Animator.StringToHash("Stun");
            idleBool = Animator.StringToHash("Idle");

        }
        stunnedBool = Animator.StringToHash("Stunned");

        //oldHealth = health;
    }

    // Update is called once per frame
    protected virtual void Update () {

        //Update Halen's Info
        //Should probably add these variables to Halen's script :L
        if (halen == null)
            halen = GameObject.FindGameObjectWithTag("Player");
        halenPos = halen.transform.position;
        halenDir = halen.GetComponent<Rigidbody>().velocity.normalized;
        halenAlive = !PlayerControl.isDead;
        if (!halenAlive && anim.GetBool(alertBool) == true)
        {
            anim.SetBool(alertBool, false);
            meshAgent.SetDestination(transform.position);
        }
        halenSpeed = halen.GetComponent<Rigidbody>().velocity.magnitude;
        //anim.SetFloat(speedFloat, meshAgent.speed);
        if(Name[0] != "Sniper" && Name[0] != "Floater")
            anim.SetBool(idleBool, Idle);
        //Update Current State
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        currentAIState = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;

        if (health <= 0 && !destroyed)
        {
            destroyed = true;
            StartCoroutine(Death(basePoints));
        }

        if (PlayerControl.isDead && anim.GetBool(alertBool) == true)
            anim.SetBool(alertBool, false);

        if (TakenDamage() && anim.GetBool(alertBool) == false)
        {
            DoAlerted();
        }

    }

    protected virtual void Patrol()
    {
        //Random Point In A Set
        Transform[] points;
        if (patrolSet != null && meshAgent != null && meshAgent.isOnNavMesh && !Idle)
        {
            points = patrolSet.GetComponentsInChildren<Transform>();
            if (meshAgent.remainingDistance <= meshAgent.radius)
            {
                if (health > 0)
                    meshAgent.SetDestination(points[Random.Range(0, points.Length)].position);
            }
        }
    }

    protected virtual void Move()
    {
        if (health > 0 && !anim.GetBool(stunnedBool) && meshAgent != null && meshAgent.isOnNavMesh)
        { 
            meshAgent.updateRotation = true;
            meshAgent.SetDestination(halenPos);
            meshAgent.Resume();
        }

    }

    bool TakenDamage()
    {
        bool damaged = oldHealth > health;
        oldHealth = health;
        return damaged;
    }

    protected virtual void DetectPlayer()
    {
        if (Vector3.Angle(transform.forward, halenPos - transform.position) < 85f && (Vector3.Distance(halenPos, transform.position) < 50f || Name[0] == "Sniper") && halenAlive)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, (halenPos - transform.position).normalized, out hit, 100f, LayerMasks.terrainAndPlayer, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.tag == "Player")
                {
                    DoAlerted();
                }
            }
        }
    }

    public void DoAlerted()
    {
        //Play Sound?
        anim.SetBool(alertBool, true);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        { 
            if (transform != e.transform && Vector3.Distance(transform.position, e.transform.position) < 20f)
            {
                if(e.GetComponent<AIBase>().anim.GetBool(alertBool) == false)
                    e.GetComponent<AIBase>().DoAlerted();
            }
        }
    }

    protected virtual IEnumerator Death(int basePoints)
    {
        GameObject sp = ui.AddStylePoint(basePoints, Name[0]);
        sp.GetComponent<UnityEngine.UI.Text>().color = new Color(1f, 0.8f, 0f);
        sp.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
		GameObject explode = (GameObject)Instantiate(explosion, transform.position+Vector3.up, Quaternion.identity) as GameObject;
		if (Name[0] == "Floater") {
			explode.transform.localScale = Vector3.one * 0.5f;
			explode.transform.GetChild (0).localScale = Vector3.one * 0.6f;
            Scoring.floatersKilled++;
		}
		else if (Name[0] == "Brawler") {
			explode.transform.localScale = Vector3.one * 0.7f;
			explode.transform.GetChild (0).localScale = Vector3.one * 0.8f;
            Scoring.brawlersKilled++;
		}
		else if (Name[0] == "Gunner") {
			explode.transform.localScale = Vector3.one * 0.7f;
			explode.transform.GetChild (0).localScale = Vector3.one * 0.8f;
            Scoring.gunnersKilled++;
		}
		else if (Name[0] == "Sniper") {
			explode.transform.localScale = Vector3.one * 0.3f;
			explode.transform.GetChild (0).localScale = Vector3.one * 0.6f;
            Scoring.snipersKilled++;
		}
        else if(Name[0] == "Charger")
        {
            Scoring.chargersKilled++;
        }
        


        yield return StartCoroutine(StylePoints());
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void doStun(float time)
    {
        if (transform.name.Contains("Charger"))
        {
            if (!GetComponentInChildren<ChargerWeakSpot>().exposed)
                return;
        }
        StartCoroutine(Stun(time));
    }

    IEnumerator Stun(float time)
    {
        anim.SetBool(stunnedBool, true);
        meshAgent.Stop();
        yield return new WaitForSeconds(time);
        meshAgent.Resume();
        anim.SetBool(stunnedBool, false);
    }

    protected virtual bool IsGrounded()
    {
        RaycastHit hit;
        float offset = GetComponent<CapsuleCollider>().height / 2;
        return Physics.Raycast(transform.position + Vector3.up * offset, -Vector3.up, out hit, offset + 0.05f);
      
        //return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
    }

    protected IEnumerator StylePoints()
    {
        int additionalPoints = 0;
        if(stylePoints.deathType == "HalenLargeShot" || stylePoints.deathType == "HalenSmallShot")
        {
            if (stylePoints.largeShotTime > 0.7f)
            {
                additionalPoints += Points.LONGSHOT;
                ui.AddStylePoint(Points.LONGSHOT, "Long Shot");
            }
            if(stylePoints.bankshot)
            {
                additionalPoints += Points.BANKSHOT;
                ui.AddStylePoint(Points.BANKSHOT, "Bank Shot");
            }
        }
        else if(stylePoints.deathType == "HalenDash")
        {
            if (stylePoints.airKill)
            {
                additionalPoints += Points.AIRKILL;
                ui.AddStylePoint(Points.AIRKILL, "Aerial Kill");
            }

            if(stylePoints.collat)
            {
                additionalPoints += Points.COLLATERAL;
                ui.AddStylePoint(Points.COLLATERAL, "Collateral");
            }

            if(Name[0] == "Sniper")
            {
                additionalPoints += Points.CLOSEUP;
                ui.AddStylePoint(Points.CLOSEUP, "Close-Up");
            }
        }
        else if(stylePoints.deathType == "GunnerSmallShot" || stylePoints.deathType == "ChargerSmash" || stylePoints.deathType == "SniperLargeShot" || stylePoints.deathType == "FloaterMine")
        {
            additionalPoints += Points.FRIENDLYFIRE;
            ui.AddStylePoint(Points.FRIENDLYFIRE, "Friendly Fire");
        }

        if(PlayerControl.Health <= 20f)
        {
            additionalPoints += Points.LASTSTAND;
            ui.AddStylePoint(Points.LASTSTAND, "Last Stand");
        }

        if(anim.GetBool(stunnedBool))
        {
            additionalPoints += Points.STUNNED;
            ui.AddStylePoint(Points.STUNNED, "Sitting Duck");
        }

        Scoring.AddScore(transform, PlayerControl.comboMultiplier, basePoints, additionalPoints);
        yield return null;
    }
}
