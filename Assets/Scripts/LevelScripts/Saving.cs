using UnityEngine;
using System.Collections;

public class Saving : MonoBehaviour
{
    public static int Score = 0;
    public static int CheckpointID = -1;
    
    public static GameObject halen;

    void Start()
    {
        halen = Resources.Load("Prefabs/Characters/Halen") as GameObject;
    }

    public static void Respawn(bool dead = true)
    {
        Scoring.AddScore(GameObject.Find("Checkpoint" + CheckpointID.ToString()).transform, 0, -1000, 0);
        //Score -= 1000;
		//Scoring.PlayerScore = Score;
        Scoring.comboCounter = 0;
        PlayerControl.Health = 100f;
        PlayerControl.Ammo = PlayerControl.MAX_SHOTS;
        if (dead)
        {
            GameObject g = Instantiate(halen, GameObject.Find("Checkpoint" + CheckpointID.ToString()).transform.position, Quaternion.identity) as GameObject;
            Camera.main.GetComponent<ThirdPersonOrbitCam>().player = g.transform;
            GameObject[] gs = GameObject.FindGameObjectsWithTag("Ragdoll");
            foreach (GameObject r in gs)
            {
                Destroy(r);
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("Checkpoint" + CheckpointID.ToString()).transform.position;
        }
            
        
    }
}
