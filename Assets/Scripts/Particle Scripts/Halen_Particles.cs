using UnityEngine;
using System.Collections;

public class Halen_Particles : MonoBehaviour {
	PlayerControl Halen;
	public ParticleSystem DashEffect;
	public ParticleSystem DashGlow;
	//public ParticleSystem GunGlow;
	//public ParticleSystem LeftFoot;
	//public ParticleSystem RightFoot;
	public ParticleSystem sprintDust;
	public ParticleSystem StopDust;
    public bool dashStorage;
	public bool sprintStorage;
	public bool landingStorage;
    bool twoArm = false;

	public AudioClip landing;
	public AudioSource CurrentSound;


	// Use this for initialization
	void Start () {
		Halen = GameObject.Find("Halen").GetComponent<PlayerControl>();
        //DashEffect = GameObject.Find("Dash_Trail").GetComponent<ParticleSystem> ();
        //DashGlow = GameObject.Find("Dash_Glow").GetComponent<ParticleSystem> ();
        //GunGlow = GameObject.FindGameObjectWithTag ("HalenGun").GetComponent<ParticleSystem> ();
        //LeftFoot = GameObject.Find("jnt_L_toe").GetComponent<ParticleSystem> ();
        //RightFoot = GameObject.Find("jnt_R_toe").GetComponent<ParticleSystem> ();

        twoArm = Halen.twoArm;

        dashStorage = false;
		sprintStorage = false;
		landingStorage = false;
}

	// Update is called once per frame
	void Update () {
		if (Halen == null) {
            Halen = GameObject.FindObjectOfType<PlayerControl>();
		}
        /*
        if (LeftFoot == null)
            LeftFoot = GameObject.Find("jnt_L_toe").GetComponent<ParticleSystem>();
        if(RightFoot == null)
            RightFoot = GameObject.Find("jnt_R_toe").GetComponent<ParticleSystem>();
            */
        if (!twoArm)
        {
            if (DashGlow == null)
                DashGlow = GameObject.Find("Dash_Glow").GetComponent<ParticleSystem>();
            if (DashEffect == null)
                DashEffect = GameObject.Find("Dash_Trail").GetComponent<ParticleSystem>();
        }
		if(StopDust == null)
			StopDust = GameObject.Find ("Dust_Puff").GetComponent<ParticleSystem> ();

        if (!PlayerControl.isDead)
        {
            // DASH PARTICLES
            if (PlayerControl.IsDashing() && !twoArm) 
			{
                if(dashStorage == false)
                {
                    DashGlow.Play();
                    DashEffect.Play();
                    dashStorage = true;
                }
			}
			else{
                if (dashStorage)
                {
                    DashGlow.Stop();
                    DashEffect.Stop();
                    dashStorage = false;
                }
            }

            /*
            // GUN GLOW PARTICLES
            if (Halen.IsAiming())
            {
                GunGlow.Play();
            }
            else {
                GunGlow.Stop();

            }*/


			//LANDING DUST

			if (!Halen.IsGrounded ()) {
				landingStorage = false;
			}
			if (Halen.IsGrounded () && landingStorage == false) {
				landingStorage = true;
				StopDust.Play ();


				if (!Halen.roll) {
					CurrentSound.pitch = Random.Range (0.8f, 1.2f);
					CurrentSound.PlayOneShot (landing, 0.3f);
				}

			}

            // RUN DUST PARTICLES

            if (Halen.isSprinting() && Halen.IsGrounded())
            {
				sprintStorage = true;
				if (!sprintDust.isPlaying) {

					sprintDust.Play ();
				}
				/*
                if (!RightFoot.isPlaying)
                    RightFoot.Play();
                if (!LeftFoot.isPlaying)
                    LeftFoot.Play();
                    */


            }
            else {
				if (sprintDust.isPlaying) {
					
					sprintDust.Stop ();
				}
				/*
                if(LeftFoot.isPlaying)
                    LeftFoot.Stop();
                if(RightFoot.isPlaying)
                    RightFoot.Stop();
                    */

            }
			if (Halen.IsGrounded () && sprintStorage == true && !(Halen.isSprinting())) {
				sprintStorage = false;
				StopDust.Play ();
			}

        }

	}
}