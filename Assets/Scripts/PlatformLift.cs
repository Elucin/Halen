using UnityEngine;
using System.Collections;

public class PlatformLift : MonoBehaviour {

    public Transform start;
    public Transform destination;
    float speed = 0.2f;
    float startTime;
    float journeyLength;
    float fracJourney;

    GameObject Halen;



    bool moving = false;

    // Use this for initialization
    void Start()
    {
        fracJourney = 0f;
        journeyLength = destination.position.y - start.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(start.position, destination.position, fracJourney);
            


        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player" && !moving)
        {
            moving = true;
            startTime = Time.time;
            
            
        }
    }
}
