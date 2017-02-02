using UnityEngine;
using System.Collections;

public class LargeShot : MonoBehaviour {

	Vector3 direction;
    Vector3 origin;
    public Transform emitter;
	public float bulletSpeed;
	float startTime;
	float lifeTime;
    float bulletDamage;
    bool ricochet;
    Vector3 currVel;
	public Object explode_B;
    public string damageType = "HalenLargeShot";
	// Use this for initialization
	void Awake() {
        bulletDamage = 50f;
		bulletSpeed = 100f;
		lifeTime = 2f;
		startTime = Time.time;
        ricochet = false;
		
	}

    void Start()
    {
        origin = emitter.position;
        direction = emitter.forward;
        //velocity = (Camera.main.transform.TransformDirection(Vector3.forward) * 10000.0f - transform.position).normalized * bulletSpeed;
        GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        if (emitter.root.tag == "Enemy")
            damageType = "SniperLargeShot";
    }
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - startTime) >= lifeTime) {
			Destroy (gameObject);
		}
        currVel = -1 * transform.TransformDirection(GetComponent<Rigidbody>().velocity).normalized;

    }

	void OnCollisionEnter(Collision c)
	{
        if (c.transform.root != null && emitter.transform.root != null)
        {
            if (c.transform.root != emitter.transform.root)
            {
                if (c.transform.tag == "Player")
                {
                    c.gameObject.GetComponent<PlayerControl>().damageBuffer += bulletDamage;
                    DestroyBullet();
                }
                else if (c.transform.tag == "Enemy" && !c.transform.name.Contains("Charger"))
                {
                    c.gameObject.GetComponent<AIBase>().health = 0f;
                    c.gameObject.GetComponent<AIBase>().stylePoints.deathType = damageType;
                    c.gameObject.GetComponent<AIBase>().stylePoints.bankshot = ricochet;
                    if (damageType == "HalenLargeShot")
                        c.gameObject.GetComponent<AIBase>().stylePoints.largeShotTime = Time.time - startTime;
                    DestroyBullet();
                }
                else if (c.transform.tag == "Rival")
                {
                    c.gameObject.GetComponent<AIBase>().health -= 20;
                    DestroyBullet();
                }

                else if (c.transform.tag == "Mine")
                {
                    c.transform.gameObject.GetComponent<MineScript>().triggered = true;
                    DestroyBullet();
                }
                else {
                    if (ricochet == false)
                    { //Can only bounce once
                        Vector3 contactPointAverage = new Vector3();

                        foreach (ContactPoint p in c.contacts)
                        {
                            contactPointAverage += p.normal;
                        }

                        contactPointAverage /= c.contacts.Length;

                        float angle = Vector3.Angle(contactPointAverage, currVel);
                        if (angle < 50 && !c.transform.name.Contains("Charger"))
                            DestroyBullet();
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
		GameObject impact = Instantiate(explode_B, transform.position, Quaternion.identity) as GameObject;
        impact.GetComponent<ParticleSystem>().startColor = GetComponent<ParticleSystem>().startColor;
        Destroy(this.gameObject);
    }
}
