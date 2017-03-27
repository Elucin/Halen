using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StylePointObject : MonoBehaviour {
    float lifetime = 5f;
    float startTime;
    float targetY;
    bool destroyed = false;
    // Use this for initialization
    void Start () {
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 0f);
        transform.SetParent( GameObject.Find("Score Feed").GetComponent<RectTransform>());
        transform.localPosition = new Vector3(0, 58f, 0);
        transform.localScale = Vector3.one;
        startTime = Time.time;
        targetY = transform.localPosition.y;
        GameObject[] pointObjs = GameObject.FindGameObjectsWithTag("StylePoint");
        foreach(GameObject p in pointObjs)
        {
            if (p.gameObject != gameObject)
            {
                StylePointObject s = p.GetComponent<StylePointObject>();
                //s.targetY -= 12;
                //StartCoroutine(Scoring.Bump(s.targetY, s.transform));
                StartCoroutine(s.Bump());
            }
        }
        StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > lifetime)
            StartCoroutine(FadeOut());
        if (transform.localPosition.y <= -58f)
            Destroy(gameObject);

    }

    void OnDestroy()
    {
        destroyed = true;
    }

    IEnumerator FadeIn()
    {
        float alpha = 0f;
        while (GetComponent<Text>().color.a < 1f)
        {
            alpha += Time.deltaTime * 2f;
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (GetComponent<Text>().color.a > 0f)
        {
            alpha -= Time.deltaTime * 2f;
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator Bump()
    {
        targetY -= 12f;
        StopCoroutine(UpdatePosition());
        yield return StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        do
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, targetY, 0), Time.deltaTime * 2);
        } while (!destroyed && transform.localPosition.y >= targetY + 0.005f);
        /*
        if(destroyed)
            yield return null;
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, targetY, 0), Time.deltaTime * 2);
        if (transform.localPosition.y >= targetY + 0.005f && !destroyed)
        {
            StartCoroutine(UpdatePosition());
            yield return null;
        }
        else*/
        yield return null;
    }
}
