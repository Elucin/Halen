using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    //Cheats
    bool godMode = false;

	bool step = true;

    public float OffsetY;

    float healTimer;
    const float HEAL_START_TIME = 5f; //Length of time Halen has to avoid damage to start healing.
    const float HEAL_AMOUNT = 0.2f; //
    private static float health = 100f;
    public float damageBuffer;
    public float damageReduction;

    public static int MAX_SHOTS = 6;
    public const int SHOT_RECOVER_TIME = 10;
    public static int currentShots = 6;
    public static float shotRecoverTimer;

    public LargeShot largeShot;
	public SmallShot smallShot;
	public Transform ShotEmitterTrans;

    public Transform[] grabNodes;
    bool footGrab;

	private float walkSpeed = 3f;
	private float runSpeed = 7f;
	public float sprintSpeed;

	private float turnSmoothing = 30.0f;
	public float aimTurnSmoothing = 15.0f;
	public float speedDampTime = 0.1f;

	private float jumpHeight = 700.0f;
	public float jumpCooldown = 1.0f;

	private float timeToNextJump = 0;

    public const float DASH_COOLDOWN = 3.0f;
    public static float dashTimer;
    private bool collateral = false;

	public static float speed;

    const float LONG_SHOT_COOLDOWN = 2f;
    const float shortShotCooldown = 0.1f;
	public static float longShootCooldownStart;
    private float shortShootCooldownStart;
	
	private Vector3 dashDirection;
	private Vector3 lastDirection;

	private Vector3 secondPos;
	private float dashPower;

    private float prevDegree = 0;
    private float targetAngle;
    private bool swapping = false;
    private float swapTimer;
    private const float SWAP_TIME = 0.25f;

	private Animator anim;
	private int speedFloat;
	private int vSpeedFloat;
	private int jumpBool;
	private int hFloat;
	private int vFloat;
	private int aimBool;
	private int rollBool;
	private int dashBool;
	private int doDashBool;
	private int groundedBool;
	private int shootBool;
	private int dashTimerFloat;
	private int dashPowerFloat;
	private int dashVelocityFloat;
	private int dashHeldFloat;
	private int CamAngleHFloat;
	private int wallHeldBool;
    private int footHoldFloat;
	private int wallRunBool;
    private int backFlipTrig;
    private int slashTrig;
	private Transform cameraTransform;
	private Vector3 targetDirection;
	private bool canDoubleJump = true;
	private int doDoubleJump;

	private int aimingWeight;

	private float dashVelocityCoefficient;

	private float gravityMod;

	private float h;
	private float v;

    private float hk;
    private float vk;

	private static bool aim;
	public bool roll;
	private bool run;
	private bool sprint;
	private bool jump;
	private bool dashDown;
	private bool dashHeld;
	private bool dashUp;
	private bool wallHold;
    private bool walk;
	private bool shoot;
    private bool cancel;

	private bool wallSliding;
	private bool wallHolding;
	private Vector3 wallLook;
    private int wallHoldStatus;
    private int previousHoldStatus;

	private Vector3 characterVel;
	private bool isMoving;

	private float distToGround;
	private float sprintFactor;

	//Anim States
	private static int rollState;
	//private static int movingJumpState;
	private static int doubleJumpState;
	private static int jumpState;
	//private static int staticJumpState;
	private static int fallingState;
	private static int idleState;
	private static int movingState;
	private static int dashWindupState;
	private static int dashState;
	private static int dashFinishState;
    private static int slashState;
	private static int noSlashState;
    private static int backFlipState;

	private int currentBaseState;
	public static int currentDashState;
	private int currentSlashState;
	private AnimatorStateInfo baseStateInfo;

    private System.DateTime expireDate = new System.DateTime(2016, 10, 31);

	public ParticleSystem MuzzleFlash;
	public ParticleSystem MomentumShield;

    private GameObject pauseMenu;
    private GameObject optionsMenu;

    private Texture2D reticleDot;
    private Texture2D reticleCircle;

	//SFX
	public PlayerSFXManager _PlayerSFXManager;

    public static Vector3 position;
    public static float comboMultiplier = 1f;

	bool wallRun = false;
    bool canWallRun = true;
	float wallSpeed;

    bool clickToRespawn = false;

    static bool charged;
    
    public static bool isDead{get{return health <= 0;}}
    public static bool isAiming { get { return IsAiming(); } }
    public static float Health { get { return health;} set { health = value; }}
    public static int Ammo { get { return currentShots; } set { currentShots = value; } }
    public static float DashCooldown { get { return (Mathf.Clamp(Time.time - dashTimer, 0, DASH_COOLDOWN)) / DASH_COOLDOWN;} set { dashTimer = value; } }
    public static float ShotCharge { get { return (Mathf.Clamp(Time.time - shotRecoverTimer, 0, SHOT_RECOVER_TIME)) / SHOT_RECOVER_TIME; } }
    public static float ShotCooldown { get { return (Mathf.Clamp(Time.time - longShootCooldownStart, 0, LONG_SHOT_COOLDOWN)) / LONG_SHOT_COOLDOWN; } }
    public static bool Charged { get { return charged; } set { charged = value; } }
    public static float Speed { get { return speed; } }

	MeleeWeaponTrail SliceTrail;

	//colours for sharp status
	Color colour_DashReady;
	Color colour_DashNotReady;

	bool dashManagement;
	int dashFlashManagement;

	halenEyes_Script eyeScript;

    public bool twoArm = false;

    void Awake()
	{
		eyeScript = GameObject.FindObjectOfType<halenEyes_Script> ();

		dashManagement = false;
		dashFlashManagement = 0;
		colour_DashReady = Color.red;
		colour_DashNotReady = new Color (0.3f, 0, 0);
        Saving.twoArm = twoArm = name.Contains("2Arm");
        if(!twoArm)
		    SliceTrail = GameObject.Find ("Sword_Model").GetComponent<MeleeWeaponTrail> ();

        transform.name = "Halen";
        health = 100f;
        damageBuffer = 0f;
        pauseMenu = GameObject.Find("Pause_Menu");
        optionsMenu = GameObject.Find("OptionsMenu");
        //if (System.DateTime.Now.Ticks > expireDate.Ticks)
            //Application.Quit();

        /*
                string[] joysticks = Input.GetJoystickNames();
                Debug.Log(joysticks.Length);
                foreach (string js in joysticks)
                    Debug.Log(js);

                if (joysticks.Length > 0)
                {
                    noJoysticks = false;
                    joystick1 = Input.GetJoystickNames()[0];
                    if (joystick1.Contains("Xbox"))
                    {

                        //GameObject.Find("EventSystem PS").GetComponent<Scrip>
                    }
                }
                else
                    noJoysticks = true;
                     Debug.Log(noJoysticks);
        */

        Cursor.visible = false;
		anim = GetComponent<Animator> ();
		cameraTransform = Camera.main.transform;

		speedFloat = Animator.StringToHash("Speed");
		jumpBool = Animator.StringToHash("Jump");
		shootBool = Animator.StringToHash ("Shoot");
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");
		aimBool = Animator.StringToHash("Aim");
		rollBool = Animator.StringToHash ("Roll");
		vSpeedFloat = Animator.StringToHash ("vSpeed");
		dashBool = Animator.StringToHash ("Dash");
		doDashBool = Animator.StringToHash ("doDash");
		dashTimerFloat = Animator.StringToHash ("dashTimer");
		dashPowerFloat = Animator.StringToHash ("dashPower");
		dashVelocityFloat = Animator.StringToHash ("DashVelocity");
		dashHeldFloat = Animator.StringToHash ("dashHeld");
		CamAngleHFloat = Animator.StringToHash ("CamAngleH");
		wallHeldBool = Animator.StringToHash ("WallHeld");
        footHoldFloat = Animator.StringToHash("footGrab");
		wallRunBool = Animator.StringToHash ("wallRun");
        backFlipTrig = Animator.StringToHash("Backflip");
        slashTrig = Animator.StringToHash("Slash");

		rollState = Animator.StringToHash ("Base.Rolling");
		jumpState = Animator.StringToHash("Base.GroundJump");
		doubleJumpState = Animator.StringToHash ("Base.AirJump");
		fallingState = Animator.StringToHash ("Base.Falling");
		idleState = Animator.StringToHash ("Base.Idle");
		movingState = Animator.StringToHash ("Base.Locomotion");
        backFlipState = Animator.StringToHash("Base.Backflip");
		dashState = Animator.StringToHash ("Dash.Dash");
        slashState = Animator.StringToHash("SwordSlash.Slash");
		noSlashState = Animator.StringToHash("SwordSlash.NoSlash");
		//dashState = Animator.StringToHash ("Base.Dash");
		doDoubleJump = Animator.StringToHash ("doubleJump");

		groundedBool = Animator.StringToHash("Grounded");
		distToGround = GetComponent<Collider>().bounds.extents.y;
		sprintFactor = sprintSpeed / runSpeed;
		longShootCooldownStart = Time.time - LONG_SHOT_COOLDOWN;
        shortShootCooldownStart = Time.time - shortShotCooldown;
        wallHoldStatus = 0;
        healTimer = Time.time;
        shotRecoverTimer = Time.time;
        dashTimer = Time.time - DASH_COOLDOWN;

	
		reticleCircle = Resources.Load("decal_crosshair") as Texture2D;
        reticleDot = Resources.Load("wide_reticule") as Texture2D; 

        _PlayerSFXManager = GameObject.Find("SoundManager").GetComponent<PlayerSFXManager>();
	}

	public bool IsGrounded() {
		RaycastHit hit;
		bool ray =  Physics.SphereCast(transform.position + Vector3.up * 0.87f, 0.2f, -transform.up, out hit , 0.87f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore);
		if (hit.transform != null) {
			if (hit.transform.name == "Halen")
				return false;
		}
        return ray;
        //Debug.DrawLine(transform.position + new Vector3(0, distToGround, 0), (transform.position + new Vector3(0, distToGround, 0)) - Vector3.up * (distToGround + 0.1f));
        //return Physics.Raycast(transform.position + new Vector3(0, distToGround, 0), -Vector3.up, distToGround + 0.1f);
    }

    void Update()
    {
		if (!IsAiming ()) {
			GameObject.Find ("Main Camera").GetComponent<ThirdPersonOrbitCam> ().crosshair = reticleDot;
		} else {
			GameObject.Find ("Main Camera").GetComponent<ThirdPersonOrbitCam> ().crosshair = reticleCircle;
		}

        if (Input.GetKeyDown(KeyCode.BackQuote))
            godMode = !godMode;

        //GameObject.Find ("DamageImage").GetComponent<Image> ().color = new Color (255f, 0, 0, 0.5f-(0.5f*(health / 100f)));

        if (!twoArm)
        {
            if (currentSlashState != slashState && currentDashState != dashState)
            {
                SliceTrail.Emit = false;
            }
            else if (currentSlashState == slashState || currentDashState == dashState)
            {
                SliceTrail.Emit = true;
            }
        }

        /*
        if (Input.GetButtonDown("EditorPause"))
            UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused; */

        if (!isDead)
        {
            position = transform.position;
            wallHold = Input.GetButton("WallHold Xbox");
            aim = Input.GetAxis("Aim Xbox") > 0 || Input.GetButton("Aim Mouse");
            roll = Input.GetButton("Roll Xbox");
            jump = Input.GetButtonDown("Jump Xbox");

            dashDown = Input.GetButtonDown("Dash Xbox");
            dashHeld = Input.GetButton("Dash Xbox");
            dashUp = Input.GetButtonUp("Dash Xbox");
            shoot = Input.GetAxis("Shoot Xbox") > 0 || Input.GetButton("Shoot Mouse");
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (!(wallHoldStatus == 1))
                {
                    h = Input.GetAxis("Horizontal");
                    v = Input.GetAxis("Vertical");
                }
                run = (Mathf.Sqrt(h * h + v * v) >= 0.3f && Mathf.Sqrt(h * h + v * v) < 0.85f);
                sprint = (Mathf.Sqrt(h * h + v * v) >= 0.85f);
            }
            else
            {
                walk = Input.GetButton("Walk");
                run = !walk;
               // sprint = Input.GetButton("Sprint");
				sprint = true;
                if (!(wallHoldStatus == 1))
                {
                    h = Input.GetAxis("Key_Horizontal"); ;
                    v = Input.GetAxis("Key_Vertical");
                }
            }
            if (wallRun)
            {
              
                RaycastHit hit;
                Physics.SphereCast(transform.position + Vector3.up * 1.35f - transform.forward * 0.45f, 0.45f, transform.forward, out hit, 2f);

                //Physics.Raycast(transform.position + Vector3.up * 0.9f, transform.forward, out hit, 1f);
                if (hit.transform == null)
                {
                    wallRun = false;
                    GetComponent<Rigidbody>().AddForce(Vector3.up * 500f, ForceMode.Impulse);
                    GetComponent<Rigidbody>().useGravity = true;
                }
            }
            isMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;
			anim.SetBool (groundedBool, IsGrounded ());
			anim.SetBool (wallHeldBool, wallHoldStatus > 0);
			anim.SetFloat (CamAngleHFloat, getCamPlayerAngle ());
			anim.SetBool (wallRunBool, wallRun);
			JumpManagement ();
			RollManagement ();
	        MovementManagement(h, v, run, sprint);
            if(!twoArm)
	            DashManagement ();
			ShootManagement ();
	        previousHoldStatus = wallHoldStatus;
	        wallHoldStatus = WallGrabManagement(wallHoldStatus);
	        Healing();
	        Damage();
            Scoring.UpdateCombo();
        }
        else
        {
            if (GetComponentInChildren<clsragdollhelper>() != null)
            {
                GetComponentInChildren<clsragdollhelper>().metgoragdoll();
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                wallHoldStatus = 0;
                StartCoroutine(preSpawn(3f));
            }   
        }
        cancel = Input.GetButtonDown("Cancel Xbox");
		UnityEngine.EventSystems.EventSystem e = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem> ();
        if (pauseMenu.GetComponent<Canvas>().enabled == false && optionsMenu.GetComponent<Canvas>().enabled == false)
        {
			e.sendNavigationEvents = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
			e.sendNavigationEvents = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

       
        /*
        if (joystick1.Contains("Xbox"))
            cancel = Input.GetButtonDown("Cancel Xbox");
        else
            cancel = Input.GetButtonDown("Cancel PS"); */

        if (cancel)
        {
            if (pauseMenu.GetComponent<Canvas>().enabled)
                pauseMenu.GetComponent<Canvas>().enabled = false;
            else
                pauseMenu.GetComponent<Canvas>().enabled = true;

            if(optionsMenu.GetComponent<Canvas>().enabled)
            {
                optionsMenu.GetComponent<Canvas>().enabled = false;
                pauseMenu.GetComponent<Canvas>().enabled = true;
            }
             
        }

        Cursor.visible = (pauseMenu.GetComponent<Canvas>().enabled || optionsMenu.GetComponent<Canvas>().enabled);

        if (Time.time - shotRecoverTimer >= SHOT_RECOVER_TIME && currentShots < MAX_SHOTS)
        {
            shotRecoverTimer = Time.time;
            currentShots++;
        }
        else if (currentShots == MAX_SHOTS)
            shotRecoverTimer = Time.time;

        currentShots = Mathf.Clamp(currentShots, 0, MAX_SHOTS);

        currentBaseState = anim.GetCurrentAnimatorStateInfo (0).fullPathHash;
		currentDashState = anim.GetCurrentAnimatorStateInfo (5).fullPathHash;
		currentSlashState = anim.GetCurrentAnimatorStateInfo (4).fullPathHash;
		baseStateInfo = anim.GetCurrentAnimatorStateInfo (0);

        if (!jump)
        {
            if (wallHoldStatus == 1)
                wallHeld();
            else if (wallHoldStatus == 2)
                wallSlide();
        }
        else
            wallHoldStatus = 0;

        if (wallHoldStatus > 0) {
			transform.LookAt(transform.position + wallLook);
		}
        else if(!wallRun)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        if (previousHoldStatus == 0 && wallHoldStatus == 1)
        {
            wallPositionAdjust();
        }

        if (dashHeld)
			anim.SetFloat (dashHeldFloat, 1f);
		else
			anim.SetFloat (dashHeldFloat, 2f);

        /*
        if(currentBaseState == backFlipState)
        {
            GetComponent<CapsuleCollider>().center = Vector3.Lerp(GetComponent<CapsuleCollider>().center, new Vector3(0, 0.9f, 0f), anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
       }*/
	}

	void FixedUpdate()
	{
		anim.SetBool (aimBool, IsAiming());
		anim.SetFloat(hFloat, h);
		anim.SetFloat(vFloat, v);
		anim.SetFloat (vSpeedFloat, GetComponent<Rigidbody> ().velocity.y);

		gravityMod = anim.GetFloat ("gravityWeight");

		//if (currentBaseState == staticJumpState)
			//GetComponent<ConstantForce> ().force = Physics.gravity * gravityMod;
		//else
		aimingWeight = aim.GetHashCode();
		if (!aim)
			aimingWeight = shoot.GetHashCode ();
		
		if (anim.IsInTransition (0))
			characterVel = GetComponent<Rigidbody> ().velocity;
        
        if(currentBaseState == fallingState)
        {
            GetComponent<CapsuleCollider>().center = new Vector3(0, 0.9f, 0f);
        }
	}

	void JumpManagement()
	{
		if (anim.GetBool(doDoubleJump)) {
			anim.SetBool (doDoubleJump, false);
		}

		//If the jump button is pressed
		if (jump && currentBaseState != rollState)
		{
			//If character is grounded or on the wall, set jump to true.
			if (IsGrounded () || wallHoldStatus != 0 || wallRun) {
                anim.ResetTrigger(backFlipTrig);
				anim.SetBool (jumpBool, true);
				_PlayerSFXManager.playSoundEffect("jump1");
			}
			//If not grounded, attempt to double jump
 			else if (canDoubleJump) {
				anim.SetBool (doDoubleJump, true);
				canDoubleJump = false;
				GetComponent<Rigidbody> ().velocity = new Vector3 (targetDirection.x * speed / 1.5f, 0, targetDirection.z * speed / 1.5f);
				GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpHeight * 1.5f, ForceMode.Impulse);
				_PlayerSFXManager.playSoundEffect("jump2");
			}
		}

		if (currentBaseState == jumpState && anim.GetBool (jumpBool) && wallHoldStatus == 0) {
            if(speed > 0.1f)
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            else
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight * 1.25f, ForceMode.Impulse);
            anim.SetBool(jumpBool, false);
		} else if (anim.GetBool (jumpBool) && (wallHoldStatus != 0 || wallRun)) {
            wallHoldStatus = 0;
            wallHold = false;
            wallRun = false;
			GetComponent<Rigidbody> ().AddForce (-transform.forward.normalized * 400f, ForceMode.Impulse);
			anim.SetBool (jumpBool, false);
            anim.SetTrigger(backFlipTrig);
            StartCoroutine(wallRunCooldown(1.5f));
        }

        //Resets Double Jump
		if ((IsGrounded () || wallHoldStatus != 0 || wallRun) && canDoubleJump == false) {
			canDoubleJump = true;
		}

        if(!IsGrounded())
            GetComponent<CapsuleCollider>().height = Mathf.Lerp(GetComponent<CapsuleCollider>().height, 1.0f, Time.deltaTime);
        else
            GetComponent<CapsuleCollider>().height = Mathf.Lerp(GetComponent<CapsuleCollider>().height, 1.8f, Time.deltaTime);
    }

	void RollManagement()
	{  
		anim.SetBool (rollBool, roll);
		if (anim.GetBool(rollBool) && anim.GetBool(rollBool))
        {
			_PlayerSFXManager.playSoundEffect("roll");
        }

        if (currentBaseState == rollState)
        {
            GetComponent<CapsuleCollider>().height = 0.9f + anim.GetFloat("Height") * 0.9f;
            GetComponent<CapsuleCollider>().center = new Vector3(0, 0.45f + 0.45f * anim.GetFloat("Height"), 0);

        }
        else if(IsGrounded())
        {
            GetComponent<CapsuleCollider>().height = 1.8f;
            GetComponent<CapsuleCollider>().center = new Vector3(0, 0.9f, 0);
        }

    }

	void MovementManagement(float horizontal, float vertical, bool running, bool sprinting)
	{
		Rotating(horizontal, vertical);

		if(isMoving)
		{
			if (IsAiming () && IsGrounded()) {
				speed = walkSpeed;
			}
			else if(sprinting)
			{
				speed += sprintSpeed * Time.deltaTime / 5f;
				speed = Mathf.Clamp (speed, runSpeed, sprintSpeed);
			}
			else if (running)
			{
				speed = runSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
				
		}
		else
		{
			speed = 0f;
		}
		//turnSmoothing = 10 - speed;
		anim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(transform.forward*speed*100);


        if (IsGrounded() && currentBaseState != rollState && !IsAiming())
        {
            RaycastHit hit;
            float inclineMod;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f, LayerMasks.terrainOnly, QueryTriggerInteraction.Ignore))
            { 
                float dot = Vector3.Dot(Vector3.up, hit.normal);
                inclineMod = (dot - 0.15f) / 0.85f;
                inclineMod = Mathf.Clamp(inclineMod, 0.0f, 1.0f);
            }
            else
                inclineMod = 1.0f;
            GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x * speed * inclineMod, GetComponent<Rigidbody>().velocity.y * inclineMod, transform.forward.z * speed);
        }
        else if (wallRun)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * wallSpeed;
        }
        else if (currentBaseState == rollState && IsGrounded())
        {
            if (IsAiming())
            {
                Vector3 strafeDirection = transform.forward * vertical + transform.right * horizontal;
                GetComponent<Rigidbody>().velocity = new Vector3(strafeDirection.x * 10.0f, GetComponent<Rigidbody>().velocity.y, strafeDirection.z * 10.0f);
            }
            else
                GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x * 10.0f, GetComponent<Rigidbody>().velocity.y, transform.forward.z * 10.0f);
        }
        else if (IsAiming() && IsGrounded())
        {
            Vector3 strafeDirection = transform.forward * vertical + transform.right * horizontal;
            GetComponent<Rigidbody>().velocity = new Vector3(strafeDirection.x * speed, GetComponent<Rigidbody>().velocity.y, strafeDirection.z * speed);
        }
        else if (!IsGrounded())
        {
            RaycastHit hit;
            float inclineMod;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f, LayerMasks.terrainOnly, QueryTriggerInteraction.Ignore))
            {
                float dot = Vector3.Dot(Vector3.up, hit.normal);
                inclineMod = (dot - 0.15f) / 0.85f;
                inclineMod = Mathf.Clamp(inclineMod, 0.0f, 1.0f);
            }
            else
                inclineMod = 1.0f;
            GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * speed * 50 * inclineMod, 0, transform.forward.z * speed * 50), ForceMode.Force);
        }
	}

	void DashManagement()
	{
        if (Time.time - dashTimer >= DASH_COOLDOWN)//can dash
        {
			if (dashManagement == false) {
				if (dashFlashManagement <= 0) {
					//set to ready colour
					SkinnedMeshRenderer[] arm = GameObject.Find ("sharp_grp").GetComponentsInChildren<SkinnedMeshRenderer> ();
					foreach (SkinnedMeshRenderer s in arm) {
						s.material.SetFloat ("_Outline", 0.002f);
					}
					GameObject.Find ("Sword_Model").GetComponent<MeshRenderer> ().material.SetFloat ("_Outline", 0.002f);
					dashManagement = true;

				} else {
					//flash outline
					SkinnedMeshRenderer[] arm = GameObject.Find ("sharp_grp").GetComponentsInChildren<SkinnedMeshRenderer> ();
					foreach (SkinnedMeshRenderer s in arm) {
						s.material.SetColor ("_OutlineColor", colour_DashReady);
						s.material.SetFloat ("_Outline", Mathf.Lerp(0.002f,0.01f,dashFlashManagement/20f));
					}
					GameObject.Find ("Sword_Model").GetComponent<MeshRenderer> ().material.SetColor ("_OutlineColor", colour_DashReady);
					GameObject.Find ("Sword_Model").GetComponent<MeshRenderer> ().material.SetFloat ("_Outline", Mathf.Lerp(0.002f,0.01f,dashFlashManagement/20f));
					dashFlashManagement--;
					if (dashFlashManagement == 19)
						_PlayerSFXManager.playSoundEffect("dashReady");
				}

			}

			if (dashDown) {
				if (dashManagement == true) {
					//set to not ready colour
					SkinnedMeshRenderer[] arm = GameObject.Find ("sharp_grp").GetComponentsInChildren<SkinnedMeshRenderer> ();
					foreach (SkinnedMeshRenderer s in arm) {
						s.material.SetColor ("_OutlineColor", colour_DashNotReady);
					}
					GameObject.Find ("Sword_Model").GetComponent<MeshRenderer> ().material.SetColor ("_OutlineColor", colour_DashNotReady);
					dashManagement = false;
					dashFlashManagement = 20;

				}
				anim.SetBool (dashBool, true); //Sets animator transition
				dashTimer = Time.time;
				_PlayerSFXManager.playSoundEffect ("dash");
				StartCoroutine(eyeScript.EyeExpression (8, 1f));
			}
			else

				anim.SetBool(dashBool, false);
        }
		else
			
			anim.SetBool(dashBool, false);
        

        if (anim.GetBool(dashBool))
            dashDirection = cameraTransform.TransformDirection(Vector3.forward).normalized; //Locks the current target direction as the dash direction


        //Execute only when dashing
        if (IsDashing())
        {
            dashVelocityCoefficient = anim.GetFloat(dashVelocityFloat);
            if (!Charged)
            {
                if (dashVelocityCoefficient > 0)
                    GetComponent<Rigidbody>().velocity = dashDirection * 60.0f * dashVelocityCoefficient;
            }
            else
            {
                if (dashVelocityCoefficient > 0)
                    GetComponent<Rigidbody>().velocity = dashDirection * (GameObject.FindObjectOfType<Jumo>().CheckpointY * 3.25f) * dashVelocityCoefficient;
            }
        }
        else if (collateral)
            collateral = false;


	}

	void ShootManagement()
	{
		if (shoot) {

            if (!twoArm)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hit, 10f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.CompareTag("Enemy") && Vector3.Distance(transform.position, hit.transform.position) < 2f)
                    {
                        anim.SetTrigger(slashTrig);

                        if (!hit.transform.name.Contains("Charger"))
                            hit.transform.GetComponent<AIBase>().health = 0;
                        return;

                    }
                }
            }
			StartCoroutine (eyeScript.EyeExpression (9, 0, false));
			anim.SetBool (shootBool, true);
			if (IsAiming () && (Time.time - longShootCooldownStart) >= LONG_SHOT_COOLDOWN && currentShots > 0) {
				longShootCooldownStart = Time.time;
				MuzzleFlash.Play ();
				LargeShot newShot = Instantiate (largeShot, ShotEmitterTrans.position, Quaternion.identity) as LargeShot;
				newShot.emitter = ShotEmitterTrans;
				currentShots--;
					
				_PlayerSFXManager.playSoundEffect ("largeShot");

			} else if (!IsAiming () && Time.time - shortShootCooldownStart >= shortShotCooldown && anim.GetCurrentAnimatorStateInfo (1).fullPathHash == Animator.StringToHash ("RunAndGun.RunAim") && !swapping && currentBaseState != rollState) {
				shortShootCooldownStart = Time.time;
				MuzzleFlash.Play ();
				SmallShot newShot = Instantiate (smallShot, ShotEmitterTrans.position, Quaternion.identity) as SmallShot;
				newShot.emitter = ShotEmitterTrans;
					
					
				_PlayerSFXManager.playSoundEffect ("smallShot");
			}
		} else {
			anim.SetBool (shootBool, false);
            if(halenEyes_Script.currentEyeIndex == 9)
			    StartCoroutine(eyeScript.EyeExpression (13, 0,true));
		}
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);



		float finalTurnSmoothing;

		if((IsAiming() && wallHoldStatus == 0) || IsDashing())
		{
			targetDirection = forward;
			finalTurnSmoothing = aimTurnSmoothing;
		}
		else
		{
			targetDirection = forward * vertical + right * horizontal;
			finalTurnSmoothing = turnSmoothing;
		}

		if((isMoving && targetDirection != Vector3.zero) || (IsAiming() && wallHoldStatus == 0) || IsDashing())
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
			lastDirection = targetDirection;
		}
		//idle - fly or grounded
		if(!(Mathf.Abs(h) > 0.9 || Mathf.Abs(v) > 0.9) && wallHoldStatus == 0)
		{
			Repositioning();
		}

		return targetDirection;
	}	

	private void Repositioning()
	{
		Vector3 repositioning = lastDirection;
		if(repositioning != Vector3.zero)
		{
			repositioning.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}

    public bool IsDead()
    {
        return health <= 0;
    }

	public static bool IsAiming()
	{
		return aim;
	}

	public static bool IsDashing()
	{
		return currentDashState == dashState;
	}

	public bool isSprinting()
	{
		return !IsAiming() && isMoving && speed >= sprintSpeed;
	}
    
	void OnAnimatorIK(int layerIndex)
	{
        if (!isDead)
        {
            Transform leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
            Transform rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
            float rFootWeight = anim.GetFloat("rFootWeight");
            float lFootWeight = anim.GetFloat("lFootWeight");
                RaycastHit lFtRay;
                RaycastHit rFtRay;

                Vector3 lFootPos = leftFoot.TransformPoint(Vector3.zero);
                Vector3 rFootPos = rightFoot.TransformPoint(Vector3.zero);

                Vector3 lFPos = lFootPos;
                Vector3 rFPos = rFootPos;

                Quaternion lFRot = leftFoot.rotation;
                Quaternion rFRot = rightFoot.rotation;

                if (Physics.Raycast(lFootPos, -Vector3.up, out lFtRay, 1))
                {
                    lFPos = lFtRay.point;
                    lFRot = Quaternion.FromToRotation(transform.up, lFtRay.normal) * transform.rotation;
                }

                if (Physics.Raycast(rFootPos, -Vector3.up, out rFtRay, 1))
                {
                    rFPos = rFtRay.point;
                    rFRot = Quaternion.FromToRotation(transform.up, rFtRay.normal) * transform.rotation;
                }

                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFootWeight);
                anim.SetIKPosition(AvatarIKGoal.RightFoot, rFPos + new Vector3(0, OffsetY, 0));

                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFootWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFPos + new Vector3(0, OffsetY, 0));

                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFootWeight);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, rFRot);

                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFootWeight);
                anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFRot);

                anim.SetLookAtWeight(aimingWeight);
                if (Vector3.Angle(cameraTransform.TransformDirection(Vector3.forward), transform.forward) < 85)
                    anim.SetLookAtPosition(cameraTransform.TransformDirection(Vector3.forward) * 1000.0f);
                else
                {
                    if (Vector3.Angle(cameraTransform.TransformDirection(Vector3.forward), transform.right) <= 88f)
                        anim.SetLookAtPosition((transform.right + transform.forward * 0.3f) * 1000.0f);
                    else if (Vector3.Angle(cameraTransform.TransformDirection(Vector3.forward), -transform.right) <= 88f)
                        anim.SetLookAtPosition((-transform.right + transform.forward * 0.3f) * 1000.0f);
                }

                //anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimingWeight / 10f);
                //anim.SetIKPosition(AvatarIKGoal.RightHand, cameraTransform.TransformDirection(Vector3.forward) * 1000.0f);
        }
	}

	void wallSlide()
	{
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, vel.y / 1.1f, 0);
	}

	void wallHeld()
	{
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().useGravity = false;
		//GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}


    int WallGrabManagement(int onWallStatus)
    {
            //0 = No Grab, 1 = Hold, 2 = Slide
        if (IsGrounded() || jump || health <= 0)
        {
            return 0;
        }

        if (!checkForWall() && !(previousHoldStatus == 0 && onWallStatus == 1)) //Slid off the wall
        {    
            return 0;
        }

        if (onWallStatus == 0 && wallHold)
        {
			_PlayerSFXManager.playSoundEffect("wallGrab");
            return 1;
        }
        else if (onWallStatus == 1)
        {
            if (wallHold)
                return 1;
            else
                return 2;
        }
        else if (onWallStatus == 2)
        {
            if (Vector3.Angle(wallLook, targetDirection) > 120f && !IsAiming() && isMoving)
                return 0;
            if (wallHold)
                return 1;
            else
                return 2;
        }
        else
            return 0;
 
    }

    bool checkForWall()
    {
        Vector3 normalAvg = new Vector3();
        int i = 0;
        //There are two nodes places where the player's hands are located when holding a wall. These raycast forward and, if both hit a surface with a flat normal, it will return true
        foreach(Transform g in grabNodes)
        {
            RaycastHit hit;
           
            if (i < 2)
            {
                Physics.Raycast(g.transform.position, g.transform.forward, out hit, 1f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore);
                if (hit.collider == null)
                    return false;
                if (Mathf.Abs(hit.normal.y) > 0.01f)
                    return false;
                normalAvg += hit.normal;
            }
            else
            {
                Physics.Raycast(g.transform.position, g.transform.forward, out hit, 1f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore);
                if (hit.collider == null)
                    footGrab = false;
                else
                    footGrab = true;
            }     
            i++;
        }
        if (footGrab)
            anim.SetFloat(footHoldFloat, 1f);
        else
            anim.SetFloat(footHoldFloat, 0f);
        normalAvg /= grabNodes.Length;
        wallLook = -normalAvg;
        return true;
    }

    void wallPositionAdjust()
    {
        float avgDistance = 0f;
        int i = 0;
        foreach (Transform g in grabNodes)
        {
            if (i < 2)
            {
                RaycastHit hit;
                Physics.Raycast(g.transform.position, transform.forward, out hit, 2f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore);
                if (hit.collider == null)
                    return;
                avgDistance += hit.distance;
                i++; 
            }
        }
        avgDistance /= grabNodes.Length;
        if (avgDistance > 0.1f)
        {
            transform.position += wallLook * (avgDistance - 0.01f);
        }
        
    }

	void OnCollisionEnter(Collision c)
	{
        if (c.transform.tag == "Enemy")
        {
            if (IsDashing())
            {
                RaycastHit hit;
                c.gameObject.GetComponent<AIBase>().health = 0;
                c.gameObject.GetComponent<AIBase>().stylePoints.airKill = !Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, LayerMasks.ignoreCharacters, QueryTriggerInteraction.Ignore);
                c.gameObject.GetComponent<AIBase>().stylePoints.deathType = "HalenDash";
                c.gameObject.GetComponent<AIBase>().stylePoints.collat = collateral;
                currentShots++;
                collateral = true;
                dashTimer = Time.time - DASH_COOLDOWN;
            }
        }
        else if (c.contacts[0].normal.y > -0.2f && c.contacts[0].normal.y < 0.3f && !wallRun && Vector3.Angle(transform.forward, c.contacts[0].normal) > 150f && !IsGrounded() && c.transform.tag == "Terrain" && currentBaseState != rollState)
        {
          
            if (GetComponent<Rigidbody>().velocity.y > -3f && canWallRun)
                StartCoroutine(wallRunDuration(1f));
            else
                wallHoldStatus = 2;

        }

	}
    IEnumerator wallRunCooldown(float cooldown)
    {
        canWallRun = false;
        yield return new WaitForSeconds(cooldown);
        canWallRun = true;
    }

	IEnumerator wallRunDuration(float duration)
	{
        GetComponent<CapsuleCollider>().center = new Vector3(0, 1.5f, -0.5f);
        wallRun = true;
		GetComponent<Rigidbody> ().useGravity = false;
		wallSpeed = Mathf.Clamp(speed, runSpeed, sprintSpeed);
		yield return new WaitForSeconds (duration);
        if(wallRun)
            GetComponent<Rigidbody>().AddForce(-transform.forward * 400f, ForceMode.Impulse);
        anim.SetTrigger(backFlipTrig);
        wallRun = false;
		GetComponent<Rigidbody> ().useGravity = true;
        StartCoroutine(wallRunCooldown(1.5f));
    }

    void Healing()
    {
        if (damageBuffer > 0) //If Damage Taken, reset timer
            healTimer = Time.time;

        if (Time.time - healTimer > HEAL_START_TIME && health > 0f && health < 100f)
            health += HEAL_AMOUNT * Time.timeScale;

        health = Mathf.Clamp(health, 0, 100);
    }

    //Note to self: maybe add damage as a list of vectors including damage and damage source?
    void Damage()
    {
        if (speed > 0)
            damageReduction += (speed / sprintSpeed) * (Time.deltaTime / 16);
        else
            damageReduction -= Time.deltaTime / 4;

        damageReduction = Mathf.Clamp(damageReduction, 0.0f, 0.5f);
        //Debug.Log((damageReduction / 0.5f) * 100 + ", " + damageReduction);

        if (currentBaseState == rollState)
        {
            //33% chance to avoid damage
            float random = Random.Range(0, 2);
            if (random == 0)
            {
                damageBuffer = 0f;
                return;
            }
        }
        float originalDamage = damageBuffer; //Original damage amount backed up
        damageBuffer -= damageBuffer*damageReduction; // Damage taken modified by shield
        health -= damageBuffer; //Damge subtracted from health

		if (damageBuffer > 0) {
			_PlayerSFXManager.playSoundEffect ("hit");
		}

        if(godMode)
        {
            health = Mathf.Clamp(health, 1f, 100f);
        }
        if(health > 0)
		    MomentumShield.startColor = new Color(MomentumShield.startColor.r, MomentumShield.startColor.g, MomentumShield.startColor.b, damageReduction/6f);
		
        damageReduction -= damageReduction * (originalDamage / 33); //damage reduction modified based on amount of damage taken
        damageBuffer = 0f;

    }

	float getCamPlayerAngle()
	{
        float angle = Vector3.Angle (transform.forward, cameraTransform.TransformDirection (Vector3.forward));
        if (Vector3.Angle(transform.right, cameraTransform.TransformDirection(Vector3.forward)) > 90f)
        {
            angle = -angle;
        }

        if (Mathf.Abs(prevDegree) > 150f && ((prevDegree < 0 && angle > 0) || (prevDegree > 0 && angle < 0)) && !swapping)
        {
            swapping = true;
            targetAngle = angle;
            swapTimer = Time.time;
        }

        if (swapping && (Time.time - swapTimer) / SWAP_TIME < 1f)
        {
            return Mathf.Lerp(prevDegree, targetAngle, (Time.time - swapTimer) / SWAP_TIME);
        }
        else
        {
            swapping = false;
            return prevDegree = angle;
        }

    }

    IEnumerator preSpawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        Saving.Respawn();
        //clickToRespawn = true;
    }

    
}