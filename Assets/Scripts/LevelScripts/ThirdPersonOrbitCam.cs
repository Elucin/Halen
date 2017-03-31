using UnityEngine;
using System.Collections;

public class ThirdPersonOrbitCam : MonoBehaviour 
{
	public Transform player;
	public Texture2D crosshair;
	
	public Vector3 pivotOffset = new Vector3(0.0f, 1.0f,  0.0f);
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.0f);

	private float smooth = 25f;

	public Vector3 aimPivotOffset = new Vector3(0.0f, 1.7f,  -0.3f);
	public Vector3 aimCamOffset   = new Vector3(0.8f, 0.0f, -1.0f);

	float horizontalAimingSpeed = Options.mouseSensitivity;
	float verticalAimingSpeed = Options.mouseSensitivity;
	private float maxVerticalAngle = 80f;
	private float minVerticalAngle = -80f;

	public float sprintFOV = 100f;
	
	private PlayerControl playerControl;
	private float angleH = 0;
	private float angleV = 0;
	private Transform cam;

	private Vector3 relCameraPos;
	private float relCameraPosMag;
	
	private Vector3 smoothPivotOffset;
	private Vector3 smoothCamOffset;
	private Vector3 targetPivotOffset;
	private Vector3 targetCamOffset;

	private float defaultFOV;
	private float targetFOV;
    string[] joysticks;

    protected int CamAngleH;
    protected int CamAngleV;

    Animator playerAnim;
    void Awake()
	{
        CamAngleH = Animator.StringToHash("CamAngleH");
        CamAngleV = Animator.StringToHash("CamAngleV");
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		cam = transform;
		playerControl = player.GetComponent<PlayerControl> ();

		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;

		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;

		defaultFOV = cam.GetComponent<Camera>().fieldOfView;
	}

	void OnPreRender()
	{
        if(playerAnim == null)
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (playerControl == null)
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        //Apply button should do this
        horizontalAimingSpeed = Options.mouseSensitivity;
        int invert = Options.invertY ? -1 : 1;
        verticalAimingSpeed = invert * Options.mouseSensitivity;
        joysticks = Input.GetJoystickNames();
        if (Cursor.visible == false)
        {
            float xAxis;
            float yAxis;
            /*if(Input.GetJoystickNames()[0].Contains("Xbox"))
            {
                xAxis = Input.GetAxis("Camera X Xbox");
                yAxis = Input.GetAxis("Camera Y Xbox");
            }
            else if(Input.GetJoystickNames()[0].Contains("PS"))
            {
                xAxis = Input.GetAxis("Camera X PS");
                yAxis = Input.GetAxis("Camera Y PS");
            }
            else
            {
                xAxis = Input.GetAxis("Camera X Mouse");
                yAxis = Input.GetAxis("Camera Y Mouse");
            }*/
            if (Input.GetAxis("Camera X Xbox") != 0 || Input.GetAxis("Camera Y Xbox") != 0)
            {
                xAxis = Input.GetAxis("Camera X Xbox") / 1.25f;
                yAxis = Input.GetAxis("Camera Y Xbox") / 1.25f;
            }
            else
            {
                xAxis = Input.GetAxis("Camera X Mouse");
                yAxis = Input.GetAxis("Camera Y Mouse");
            }

            if (!PlayerControl.isAiming)
            {
                angleH += Mathf.Clamp(xAxis, -5, 5) * horizontalAimingSpeed * Time.deltaTime;
                angleV += Mathf.Clamp(yAxis, -5, 5) * verticalAimingSpeed * Time.deltaTime;
            }
            else
            {
                angleH += xAxis * horizontalAimingSpeed / 3 * Time.deltaTime;
                angleV += yAxis * verticalAimingSpeed / 3 * Time.deltaTime;
            }


            if (angleH > 195f)
                angleH -= 360f;
            else if (angleH < -195f)
                angleH += 360f;
            
            angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
            //playerAnim.SetFloat(CamAngleH, angleH);

            //playerAnim.SetFloat(CamAngleV, angleV);
            
        }
            Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
            Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
            cam.rotation = Quaternion.Lerp(cam.rotation, aimRotation, Time.deltaTime * smooth);

            if (PlayerControl.isAiming)
            {
                targetPivotOffset = aimPivotOffset;
                targetCamOffset = aimCamOffset;
            }
            else
            {
                targetPivotOffset = pivotOffset;
                targetCamOffset = camOffset;
            }

            //targetFOV = 60 + PlayerControl.Speed * 5.0f;
            /*
            if(playerControl.isSprinting())
            {
                targetFOV = sprintFOV;
            }
            else
            {
                targetFOV = defaultFOV;
            }*/
            //cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp (cam.GetComponent<Camera>().fieldOfView, targetFOV,  Time.deltaTime);

            // Test for collision
            Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
            Vector3 tempOffset = targetCamOffset;
            for (float zOffset = targetCamOffset.z; zOffset <= -0.5f; zOffset += 0.5f)
            {
                tempOffset.z = zOffset;
                if (DoubleViewingPosCheck(baseTempPosition + aimRotation * tempOffset) || zOffset == -0.5f)
                {
                    targetCamOffset.z = tempOffset.z;
                    break;
                }
            }

        if (PlayerControl.IsDashing())
            smooth = Mathf.Lerp(smooth, 10f, Time.deltaTime * 10f);
        else
            smooth = Mathf.Lerp(smooth, 25f, Time.deltaTime * 10f);

        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        if (player.gameObject.GetComponent<PlayerControl>().IsDead())
            smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset + new Vector3(0,0,-5), smooth * Time.deltaTime);
        smoothCamOffset = Vector3.Lerp(smoothCamOffset, targetCamOffset, smooth * Time.deltaTime);
        cam.position = Vector3.Lerp(cam.position, player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset, smooth / 2 * Time.deltaTime);

        if(player.gameObject.GetComponent<PlayerControl>().IsDead())
            cam.position = Vector3.Lerp(cam.position, player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset + new Vector3(0,2,0), smooth / 2 * Time.deltaTime);


    }

	// concave objects doesn't detect hit from outside, so cast in both directions
	bool DoubleViewingPosCheck(Vector3 checkPos)
	{
		Vector3 playerFocusHeight = PlayerControl.halenGO.transform.TransformPoint(player.GetComponent<CapsuleCollider> ().center);
        //Debug.Log(checkPos);
		return ViewingPosCheck (checkPos, playerFocusHeight) && ReverseViewingPosCheck (checkPos, playerFocusHeight);
	}

	bool ViewingPosCheck (Vector3 checkPos, Vector3 colliderCenter)
	{
		RaycastHit hit;
       
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, colliderCenter - checkPos, out hit, relCameraPosMag, LayerMasks.terrainOnly, QueryTriggerInteraction.Ignore))
		{
            //Debug.DrawRay(colliderCenter, checkPos - colliderCenter, Color.green, 0.05f, false);
            // ... if it is not the player...
            if (hit.transform != player && hit.transform != transform)
			{
                //Debug.Log(hit.transform.name);
                // This position isn't appropriate.
                return false;
			}
        }
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		return true;
	}

	bool ReverseViewingPosCheck(Vector3 checkPos, Vector3 colliderCenter)
	{

        RaycastHit hit;

		if(Physics.Raycast(colliderCenter, checkPos - colliderCenter, out hit, relCameraPosMag, LayerMasks.terrainOnly, QueryTriggerInteraction.Ignore))
		{
			if(hit.transform != player && hit.transform != transform)
			{
				return false;
			}
		}
		return true;
	}

	// Crosshair
	void OnGUI () 
	{
		if(!Cursor.visible)
			GUI.DrawTexture(new Rect(Screen.width/2-(crosshair.width * Screen.width / 1280f) * 0.5f, 
			                Screen.height/2 - (crosshair.width * Screen.width / 1280f) * 0.5f, 
			                crosshair.width * Screen.width / 1280f, crosshair.height * Screen.width / 1280f), crosshair);
	}
}
