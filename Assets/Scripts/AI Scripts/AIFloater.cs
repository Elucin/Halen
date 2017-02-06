using UnityEngine;
using System.Collections;

public class AIFloater : AIBase {

    //Parameters
    private int mineTrigger;

    //States
    private int patrolState;
    private int miningState;
    Vector3 point;
    public GameObject mine;
    public float patrolRange;

    float mineTimer;
    const float MINE_TIME = 4.0f;

    protected static int FloaterCount = 0;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        transform.name = "Floater-" + FloaterCount++.ToString();
        Name = transform.name.Split('-');
        point = transform.position;
        mineTimer = Time.time;
        mineTrigger = Animator.StringToHash("DropMine");
	}
	
	// Update is called once per frame
	protected override void Update () {
        basePoints = 200;
        base.Update();
        Patrol();

        if(currentAIState == patrolState)
        {
            DetectPlayer();
        }
        else if(Time.time - mineTimer > MINE_TIME)
        {
            mineTimer = Time.time;
            anim.SetTrigger(mineTrigger);
            StartCoroutine(dropMine(1f));
        }
	}

    protected override void Patrol()
    {
        if (meshAgent.remainingDistance <= meshAgent.radius && meshAgent.isOnNavMesh)
        {
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            Vector3 direction = new Vector3(x, 0, z);
            direction.Normalize();

            meshAgent.SetDestination(point + (direction * Random.Range(0f, patrolRange)));
        }
    }

    IEnumerator dropMine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(mine, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    void OnCollisionEnter(Collision c)
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 direction = new Vector3(x, 0, z);
        direction.Normalize();

        meshAgent.SetDestination(point + (direction * Random.Range(0f, patrolRange)));
    }
}
