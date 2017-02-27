using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject brawler_Prefab; //Prefab
    [Tooltip("How many to spawn")]
    public int brawler_Spawn_Count; //How many to spawn
    [Tooltip("Minimum time to spawn")]
    public float brawlerDelay;  //Minimum delay for spawn
    [Tooltip("Range of time beyond the minimum to spawn (Delay + Variance = Max Time To Spawn)")]
    public float brawlerVariance; //Range of time to spawn (Delay + Variance is the max spawn time)
	float spawnTimerBrawler; //Time stamp
    float spawnTimeBrawler; //Time to next spawn
    private int brawlerCount = 0; //How many have been spawned

    public GameObject gunner_Prefab;
    public int gunner_Spawn_Count;
    public float gunnerDelay;
    public float gunnerVariance;
    public bool doInitialSpawn = true;
    bool initialSpawn;
    float spawnTimerGunner;
    float spawnTimeGunner;
    private int gunnerCount = 0;

    [Tooltip("The location where the enemies spawn.")]
    public Transform[] Spawn_Point;

    int PlayerInTrigger = 0;
    [Tooltip("If TRUE, spawner will only spawn while player is in the trigger. Otherwise, it will not stop even if the player exits the trigger.")]
    public bool DoToggleSpawn = true;

    [Tooltip("Radius of the spherical area in which enemies spawn. Value of 0 will spawn from a single point.")]
    public float SpawnArea = 1f;

    [Tooltip("If TRUE, they will only spawn within a circle on the XZ plane, instead of in a sphere.")]
    public bool FlatSpawn = true;

	// Use this for initialization
	void Start () {
        initialSpawn = doInitialSpawn;
        spawnTimeBrawler = brawlerDelay + Random.Range(0, brawlerVariance);
        spawnTimerBrawler = Time.time;

        spawnTimeGunner = gunnerDelay + Random.Range(0, gunnerVariance);
        spawnTimerGunner = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - spawnTimerBrawler >= spawnTimeBrawler || initialSpawn) && PlayerInTrigger > 0 && brawlerCount < brawler_Spawn_Count && !PlayerControl.isDead) {
            if (initialSpawn)
                initialSpawn = false;
			Vector3 pos = Spawn_Point[Random.Range(0, Spawn_Point.Length)].transform.position + Random.insideUnitSphere * SpawnArea;
            if (FlatSpawn)
                pos.y = Spawn_Point[Random.Range(0, Spawn_Point.Length)].transform.position.y;
            GameObject newbrawler = Instantiate(brawler_Prefab, pos , Quaternion.identity) as GameObject;
            newbrawler.GetComponent<Animator> ().SetBool ("Alerted", true);
            spawnTimerBrawler = Time.time;
            spawnTimeBrawler = brawlerDelay + Random.Range(0, brawlerVariance);
            brawlerCount++;
        }

        if ((Time.time - spawnTimerGunner >= spawnTimeGunner || initialSpawn) && PlayerInTrigger > 0 && gunnerCount < gunner_Spawn_Count && !PlayerControl.isDead)
        {
            if (initialSpawn)
                initialSpawn = false;
            Vector3 pos = Spawn_Point[Random.Range(0, Spawn_Point.Length)].transform.position + Random.insideUnitSphere * 10.0f;
            if (FlatSpawn)
                pos.y = Spawn_Point[Random.Range(0, Spawn_Point.Length)].transform.position.y;
            GameObject newgunner = Instantiate(gunner_Prefab, pos, Quaternion.identity) as GameObject;
            newgunner.GetComponent<Animator>().SetBool("Alerted", true);
            spawnTimerGunner = Time.time;
            spawnTimeGunner = gunnerDelay + Random.Range(0, gunnerVariance);
            gunnerCount++;
        }

    }

    public void OnChildTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
			++PlayerInTrigger;
    }

    public void OnChildTriggerExit(Collider c)
    {
        if (c.transform.tag == "Player" && DoToggleSpawn)
            --PlayerInTrigger;
    }
}
