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
    public Image ChargeHaze;
	public Image SwordGlow;
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

	bool dashFlash;
	float dashCounter;

    float chargeHazeAlpha = 0f;

	public Image ReticleSharpFlash;
	public Image ReticleSharpReady;

    // Use this for initialization
    void Start () {
        Halen = GameObject.FindObjectOfType<PlayerControl>();

		dashFlash = false;

		if (GameObject.FindGameObjectWithTag ("Rival") != null) {
			Theravall = true;
		} else {
			Theravall = false;
		}
	
		TheravallUIset = false;

        wristUI = GameObject.Find("aimShotUI").GetComponent<Canvas>();
        shotCooldownBarWrist = GameObject.Find("wrist_RechargeFill").GetComponent<Image>();
        shotFillsWrist = GameObject.Find("wrist_BulletFill").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        dashBar.gameObject.SetActive(!Halen.twoArm);
        
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
		if (!Halen.twoArm) {
			dashBar.fillAmount = PlayerControl.DashCooldown;
			if (dashBar.fillAmount ==1f) {
																
				if (dashFlash == false) {
					//set up flash

					ReticleSharpFlash.color = new Color (1f, 1f, 1f, 0f);
					SwordGlow.color = new Color (1f, 1f, 1f, 1f);
					dashFlash = true;
					dashCounter = 20f;

				} else {
					if (dashCounter > 0) {
						//dash flashing
						SwordGlow.color = new Color (1f, 1f, 1f, Mathf.Lerp (1f, 0f, 1f - (dashCounter / 20f)));
						ReticleSharpFlash.color = new Color (1f, 1f, 1f, Mathf.Lerp (1f, 0f, 1f - (dashCounter / 20f)));
						dashCounter--;
					}
					if (dashCounter == 0) {
						ReticleSharpFlash.color = new Color (1f, 1f, 1f, 0f);
						ReticleSharpReady.color = new Color (1f, 1f, 1f, 1f);
					}
				}
				
			}else {
				//dash not ready
				dashFlash = false;
				SwordGlow.color = new Color (1f, 1f, 1f, 0f);
				ReticleSharpFlash.color = new Color (1f, 1f, 1f, 0f);
				ReticleSharpReady.color = new Color (1f, 1f, 1f, 0f);
			}
		}
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


			//combo count changes colour in stages
			if (Scoring.comboCounter <= 5f) {
				comboCounter.color = new Color (0f, 1f, 0f);
			} else if (Scoring.comboCounter <= 10f) {
				comboCounter.color = new Color (1f, 1f, 0f);
			} else if (Scoring.comboCounter <= 15f) {
				comboCounter.color = new Color (1f, 0.5f, 0f);
			} else if (Scoring.comboCounter <= 20f){
				comboCounter.color = new Color (1f, 0f, 0f);
			}
				


            //comboCounter.color = new Color(Scoring.comboCounter / 20f, (20f - Scoring.comboCounter) / 20f, 0f);
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
        
        if(PlayerControl.Charged)
        {
            chargeHazeAlpha = 0.5f;
        }
        else
        {
            chargeHazeAlpha -= Time.deltaTime;
        }
        chargeHazeAlpha = Mathf.Clamp(chargeHazeAlpha, 0f, 0.5f);
        ChargeHaze.color = Color.Lerp(ChargeHaze.color, new Color(ChargeHaze.color.r, ChargeHaze.color.g, ChargeHaze.color.b, chargeHazeAlpha), Time.deltaTime * 3);
    }

    public GameObject AddStylePoint(int points, string source)
    {
        GameObject stylePoint = (GameObject)Instantiate(stylePointObject, Vector3.zero, Quaternion.identity) as GameObject;
        stylePoint.GetComponent<Text>().text = " +" + points.ToString() + " " + source;
        return stylePoint;
    }
}
