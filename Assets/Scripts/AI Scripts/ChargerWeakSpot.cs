using UnityEngine;
using System.Collections;

public class ChargerWeakSpot : MonoBehaviour {
    //public ParticleSystem sparks;
    public bool exposed = false;
    public GameObject[] backpanels;
    private AIBase chargerScript;
    static GameObject chargerChunks;
	public GameObject BackPanelChunks;

    void Start()
    {
        
        chargerScript = GetComponentInParent<AIBase>();
    }

	void OnCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("Player"))
        {
            //PlayerControl halen = c.transform.GetComponent<PlayerControl>();
            if (PlayerControl.IsDashing() && exposed)
            {
                if (!transform.root.name.Contains("Broken"))
                {
                    chargerScript.health = 0;
                    PlayerControl.dashTimer = Time.time - PlayerControl.DASH_COOLDOWN;
                }
                else
                {
                    Instantiate(AIBase.explosion, transform.position + Vector3.up, Quaternion.identity);
                    Instantiate(AIBase.chargerChunks, transform.position + Vector3.up, Quaternion.identity);
                    Destroy(transform.root.gameObject);
                }
            }
        }
        else if (c.transform.name.Contains("LargeShot"))
        {
            if (!exposed)
            {
                chargerScript.DoAlerted();
                exposed = true;
                Destroy(backpanels[0]);
                Destroy(backpanels[1]);
				Instantiate (BackPanelChunks,transform.position, Quaternion.identity);
				Instantiate(AIBase.explosion, transform.position + Vector3.up, Quaternion.identity);
                //sparks.Play();
                GetComponent<Light>().color = Color.red;
            }
        }
        else if (c.transform.name.Contains("SmallShot") && exposed)
        {
            chargerScript.doStun(1f);
        }
    }
}
