using UnityEngine;
using System.Collections;

public class SmallShot : MonoBehaviour {

	public float bulletSpeed;
    public float bulletDamage;
	float startTime;
	float lifeTime;
    public Transform emitter;
	float scaleLimit;
	float z;
    Vector3 currVel;
    bool ricochet;
	public Object explode_S;
    public string damageType = "HalenSmallShot";

	public BrawlerSFXManager _BrawlerSFXManager;

	// Use this for initialization
	void Awake() {
        ricochet = false;
        bulletDamage = 5.0f;
		scaleLimit = 0.5f;
		z = 17.0f;
		bulletSpeed = 200f;
		lifeTime = 2f;
		startTime = Time.time;		
	}

    void Start()
    {
        float randomRadius = Random.Range(0, scaleLimit);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 direction = Random.insideUnitCircle * scaleLimit;
        direction.z = z;
        //direction = Camera.main.transform.TransformDirection (direction.normalized);
        direction = emitter.TransformDirection(direction.normalized);
        //velocity = (Camera.main.transform.TransformDirection (Vector3.forward) * 10000.0f - transform.position).normalized;
        //velocity = new Vector3(direction.x * velocity.x, direction.y * velocity.y, direction.z * velocity.z).normalized;
        GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        if (emitter.root.tag == "Enemy")
            damageType = "GunnerSmallShot";
    }

	// Update is called once per frame
	void Update () {
		if ((Time.time - startTime) >= lifeTime) {
			Destroy (gameObject);
		}
        currVel = -1 * transform.TransformDirection(GetComponent<Rigidbody>().velocity).normalized;
        //Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(GetComponent<Rigidbody>().velocity).normalized, Color.red);
    }

	void OnCollisionEnter(Collision c)
	{
        if (emitter != null)
        {
            if (c.transform.root != emitter.transform.root)
            {
                if (c.transform.tag == "Player")
                {
                    c.gameObject.GetComponent<PlayerControl>().damageBuffer += bulletDamage;
                    DestroyBullet();

                    //Play SFX of Halen Being Hurt
                }
                else if (c.transform.tag == "Mine")
                {
                    c.transform.gameObject.GetComponent<MineScript>().triggered = true;
                    DestroyBullet();
                }
                else if ((c.transform.tag == "Enemy" || c.transform.tag == "Rival") && !c.transform.name.Contains("Charger"))
                {
                    //Play SFX of Enemy Being Hit
                    if (c.transform.tag == "Enemy")
                    {
                        _BrawlerSFXManager = c.gameObject.GetComponentInChildren<BrawlerSFXManager>();
                        //_BrawlerSFXManager.playSoundEffect("hit");
                        c.gameObject.GetComponent<AIBase>().health -= bulletDamage;
                        if (c.gameObject.GetComponent<AIBase>().health <= 0)
                            c.gameObject.GetComponent<AIBase>().stylePoints.deathType = damageType;
                    }
                        c.gameObject.GetComponent<AIBase>().doStun(1f);
                    DestroyBullet();
                }
                else
                {
                    if (ricochet == false) //Can only bounce once
                    {
                        Vector3 contactPointAverage = new Vector3();

                        foreach (ContactPoint p in c.contacts)
                        {
                            contactPointAverage += p.normal;
                        }

                        contactPointAverage /= c.contacts.Length;

                        float angle = Vector3.Angle(contactPointAverage, currVel);

                        if (angle < 50 && !c.transform.name.Contains("Charger"))
                        {
                            DestroyBullet();
                        }
                        else
                            ricochet = true;
							
                    }
                    else
                        DestroyBullet();
                }
            }
        }
	}

    void DestroyBullet()
    {
		GameObject impact = Instantiate(explode_S, transform.position, Quaternion.identity) as GameObject;
        impact.GetComponent<ParticleSystem>().startColor = GetComponent<ParticleSystem>().startColor;
		impact.GetComponentInChildren<ParticleSystem>().startColor = GetComponent<ParticleSystem>().startColor;
		Destroy(gameObject);
    }


}
