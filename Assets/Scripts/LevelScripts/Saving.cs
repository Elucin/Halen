using UnityEngine;
using System.Collections;

public class Saving : MonoBehaviour
{
    //public static int Score = 0;
    //public static int CheckpointID = -1;
    public static bool doLoad = false;
    public static bool twoArm = false;
    public static GameObject halen;
    public static GameObject halen2Arm;

    void Start()
    {

        halen = Resources.Load("Prefabs/Characters/Halen/Halen") as GameObject;
        halen2Arm = Resources.Load("Prefabs/Characters/Halen/Halen_2Arm") as GameObject;
        
        if(doLoad)
        {
            Load();
        }
    }

    public static void Respawn(bool dead = true)
    {
        Scoring.AddScore(GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform, 0, -1000, 0);
        //Score -= 1000;
		//Scoring.PlayerScore = Score;
        Scoring.comboCounter = 0;
        PlayerControl.Health = 100f;
        PlayerControl.Ammo = PlayerControl.MAX_SHOTS;
        if (dead)
        {
            GameObject g;
            if (!twoArm)
            {
                 g = Instantiate(halen, GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform.position, Quaternion.identity) as GameObject;
            }
            else
            {
                g = Instantiate(halen2Arm, GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform.position, Quaternion.identity) as GameObject;
            }
            Camera.main.GetComponent<ThirdPersonOrbitCam>().player = g.transform;
            GameObject[] gs = GameObject.FindGameObjectsWithTag("Ragdoll");
            foreach (GameObject r in gs)
            {
                Destroy(r);
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform.position;
        }
            
        
    }

    public static void Load()
    {
        doLoad = false;
        PlayerPrefs.GetInt("Brawlers", 0);
        PlayerPrefs.GetInt("Gunners", 0);
        PlayerPrefs.GetInt("Snipers", 0);
        PlayerPrefs.GetInt("Chargers", 0);
        PlayerPrefs.GetInt("Floaters", 0);
        PlayerPrefs.GetInt("Combo", 0);
        Scoring.PlayerScore = PlayerPrefs.GetInt("Score", 0);
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform.position;
    }
}
