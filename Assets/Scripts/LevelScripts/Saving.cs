using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    //public static int Score = 0;
    //public static int CheckpointID = -1;
    public static bool doLoad = false;
    public static bool twoArm = false;
    public static int CP = 0;
    public static GameObject halen;
    public static GameObject halen2Arm;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start()
    {

        halen = Resources.Load("Prefabs/Characters/Halen/Halen") as GameObject;
        halen2Arm = Resources.Load("Prefabs/Characters/Halen/Halen_2Arm") as GameObject;
        
        if(doLoad)
        {
            Load();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Checkpoint.numCheckpoints = 0;
        MineScript.mineList = new System.Collections.Generic.List<Transform>();
        if (scene.name != "ScoreScreen" && scene.name != "Cutscene")
        {
            PlayerPrefs.SetInt("BrawlersL", Scoring.brawlersKilled);
            PlayerPrefs.SetInt("ChargersL", Scoring.chargersKilled);
            PlayerPrefs.SetInt("GunnersL", Scoring.gunnersKilled);
            PlayerPrefs.SetInt("SnipersL", Scoring.snipersKilled);
            PlayerPrefs.SetInt("FloatersL", Scoring.floatersKilled);
            PlayerPrefs.SetInt("ComboL", Scoring.biggestCombo);
            PlayerPrefs.SetInt("ScoreL", Scoring.PlayerScore);
        }
    }

    public static void Respawn(bool dead = true)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 6)
            Reload();
        else
        {
            Scoring.AddScore(GameObject.Find("Checkpoint" + PlayerPrefs.GetInt("Checkpoint", 0).ToString()).transform, 0, -200, 0);
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
            
        
    }

    public static void Load()
    {
        doLoad = false;
        Vector3 checkpoint = GameObject.Find("Checkpoint" + CP).transform.position;
        GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint;
    }

    public static void Reload()
    {
        doLoad = false;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
