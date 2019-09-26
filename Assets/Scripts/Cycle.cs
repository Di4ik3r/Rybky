using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public float Radius = 300;
    public int width = 50;
    public int length = 50;  

    void Start()
    {
        CreateObjects();
    }
    public void CreateObjects()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.position = new Vector3(0, 0, 0);
        floor.GetComponent<Renderer>().sharedMaterial.color = Color.cyan;
        floor.transform.localScale = new Vector3( width, length, length );
        sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.name = "Sun";
        sun.GetComponent<Renderer>().sharedMaterial.color = Color.yellow;
        sun.AddComponent<Light>().type = LightType.Directional;
        sun.GetComponent<Light>().shadows = LightShadows.Hard;
        sun.GetComponent<Light>().color = new Color(1, 1, 0.5f);
        sun.transform.position = new Vector3(width / 2, length / 2, ( length / 2 ) + Radius);
        sun.transform.localScale = new Vector3(40,40,40);
        moon.name = "Moon";
        moon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        moon.GetComponent<Renderer>().sharedMaterial.color = Color.grey;
        moon.AddComponent<Light>().type = LightType.Directional;
        moon.GetComponent<Light>().color = Color.white;
        moon.transform.position = new Vector3(width / 2, length / 2, ( length / 2 ) - Radius);
        moon.transform.localScale = new Vector3(40,40,40);
    }

    public const float daytimeRLSeconds = 5.0f;
    public const float duskRLSeconds = 1.5f;
    public const float nighttimeRLSeconds = 7.0f;
    public const float sunsetRLSeconds = 1.5f;
    public const float gameDayRLSeconds = daytimeRLSeconds + duskRLSeconds + nighttimeRLSeconds + sunsetRLSeconds;

    public const double startOfDaytime = 0.1;
    public const float startOfDusk = daytimeRLSeconds / gameDayRLSeconds;
    public const float startOfNighttime = startOfDusk + duskRLSeconds / gameDayRLSeconds;
    public const float startOfSunset = startOfNighttime + nighttimeRLSeconds / gameDayRLSeconds;


    private float timeRT = 0;
    public float TimeOfDay
    {
        get { return timeRT / gameDayRLSeconds; }
        set { timeRT = value * gameDayRLSeconds; }
    }

    void Update()
    {
        timeRT = (timeRT + Time.deltaTime) % gameDayRLSeconds;
        float sunangle = TimeOfDay * 360;
        float moonangle = TimeOfDay * 360 + 180;
        Vector3 midpoint = new Vector3(width / 2, length / 2, (length / 2)); 
        sun.transform.position = midpoint + Quaternion.Euler(0, 0, sunangle) * (Radius * Vector3.right);
        sun.transform.LookAt(midpoint);
        moon.transform.position = midpoint + Quaternion.Euler(0, 0, moonangle) * (Radius * Vector3.right);
        moon.transform.LookAt(midpoint);
    }
}
