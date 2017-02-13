using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    PlayerControl Halen;
    public GameObject Score_UI;
    public Image healthBar;
    public Image dashBar;
    public Image dashBarReady;
    public Image shotRechargeBar;
    public Image shotFills;
    public Image shotFillsWrist;
    public Image shotCooldownBarWrist;
    public Image shotCooldownBar;
    public GameObject stylePointObject;
    public Image DamageHaze;
    Text HP_Text;
    Text Ammo_Text;
    Text ShotCDText;
    Text DashCDText;
    bool aiming;
	public Canvas wristUI;
    public Text comboCounter;
    public Image comboTimer;
    public Text comboMultiplier;

	public bool Theravall;
	bool TheravallUIset;

    // Use this for initialization
    void Start () {
        Halen = GameObject.Find("Halen").GetComponent<PlayerControl>();

		if (GameObject.Find ("Rival 1") != null) {
			Theravall = true;
		} else {
			Theravall = false;
		}
	
		TheravallUIset = false;

        wristUI = GameObject.Find("aimShotUI").GetComponent<Canvas>();

    }
	
	// Update is called once per frame
	void Update () {
        
		if (shotCooldownBarWrist == null && PlayerControl.Health>0) {
			shotCooldownBarWrist = GameObject.Find ("wrist_RechargeFill").GetComponent<Image>();
            shotFillsWrist = GameObject.Find("wrist_BulletFill").GetComponent<Image>();
			wristUI = GameObject.Find ("aimShotUI").GetComponent<Canvas>();
		}


		if (Theravall && !TheravallUIset) {
			TheravallUIset = true;
			GameObject.Find ("Theravall_healthBG").SetActive (true);
		} else if(!TheravallUIset){
			TheravallUIset = true;
			GameObject.Find ("Theravall_healthBG").SetActive (false);
		}

        aiming = PlayerControl.isAiming;

        healthBar.fillAmount = PlayerControl.Health / 100f;
       // healthBar.color = Color.Lerp(Color.red, Color.cyan, Halen.damageReduction * 3f);

        Score_UI.transform.GetComponent<Text>().text = "Score:  " + Scoring.PlayerScore.ToString();

        //float dashTimer = Mathf.Clamp(2.0f - (Time.time - Halen.dashTimer), 0, 2);
        dashBar.fillAmount = PlayerControl.DashCooldown;
        shotCooldownBar.fillAmount = 0.925f - PlayerControl.ShotCooldown * 0.925f;
        shotFills.fillAmount = 0.032f * PlayerControl.Ammo;
        shotRechargeBar.fillAmount = 0.25f * PlayerControl.ShotCharge;
        DamageHaze.color = Color.Lerp(DamageHaze.color, new Color(DamageHaze.color.r, DamageHaze.color.g, DamageHaze.color.b, 1f - PlayerControl.Health / 100f), Time.deltaTime * 2f);

        if (Scoring.comboCounter == 0)
            comboCounter.transform.parent.gameObject.SetActive(false);
        else
        {
            comboCounter.transform.parent.gameObject.SetActive(true);
            comboTimer.fillAmount = (Scoring.TIMER_LENGTH - (Time.time - Scoring.comboTimer)) / Scoring.TIMER_LENGTH;
            comboTimer.color = new Color(1f - comboTimer.fillAmount, comboTimer.fillAmount, 0f);
            comboCounter.text = Scoring.comboCounter.ToString();
            comboCounter.color = new Color(Scoring.comboCounter / 20f, (20f - Scoring.comboCounter) / 20f, 0f);
            comboMultiplier.text = "x" + Scoring.comboMultiplier.ToString("F2");
        }

		if (wristUI != null) {
			if (aiming)
				wristUI.enabled = true;
			else
                wristUI.enabled = false;

			shotCooldownBarWrist.fillAmount = 0.25f * PlayerControl.ShotCharge;
			shotFillsWrist.fillAmount = 0.032f * PlayerControl.Ammo;


		}
        
        /*
        if (dashTimer > 0f)
            DashCDText.text = "Dash Cooldown: " + dashTimer.ToString("F1");
        else
            DashCDText.text = "Dash Cooldown: Ready";
            */

    }

    public GameObject AddStylePoint(int points, string source)
    {
        GameObject stylePoint = (GameObject)Instantiate(stylePointObject, Vector3.zero, Quaternion.identity) as GameObject;
        stylePoint.GetComponent<Text>().text = " +" + points.ToString() + " " + source;
        return stylePoint;
    }
}
