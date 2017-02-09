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
	private float maxVerticalAngle = 70f;
	private float minVerticalAngle = -70f;

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
    void Awake()
	{
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
        if (playerControl == null)
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        //Apply button should do this
        horizontalAimingSpeed = Options.mouseSensitivity;
        verticalAimingSpeed = Options.mouseSensitivity;
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


            if (angleH > 180f)
                angleH -= 360f;
            else if (angleH < -180f)
                angleH += 360f;
            
            angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
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

            //targetFOV = 60 + playerControl.speed * 5.0f;
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
		float playerFocusHeight = player.GetComponent<CapsuleCollider> ().height *0.5f;
		return ViewingPosCheck (checkPos, playerFocusHeight) && ReverseViewingPosCheck (checkPos, playerFocusHeight);
	}

	bool ViewingPosCheck (Vector3 checkPos, float deltaPlayerHeight)
	{
		RaycastHit hit;
		
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.position+(Vector3.up* deltaPlayerHeight) - checkPos, out hit, relCameraPosMag))
		{
			// ... if it is not the player...
			if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				// This position isn't appropriate.
				return false;
			}
		}
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		return true;
	}

	bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight)
	{
		RaycastHit hit;

		if(Physics.Raycast(player.position+(Vector3.up* deltaPlayerHeight), checkPos - player.position, out hit, relCameraPosMag))
		{
			if(hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
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
			    GUI.DrawTexture(new Rect(Screen.width/2-(crosshair.width*0.5f), 
			                         Screen.height/2-(crosshair.height*0.5f), 
			                         crosshair.width, crosshair.height), crosshair);
	}
}
