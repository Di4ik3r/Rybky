using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoids : MonoBehaviour
{
    public GameObject boidPrefab;
    public Vector3 mapSize;
    public int boidsCount = 5;
    public float maxSpeed = 5f;
    public Material material;


    private GameObject[] boids;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateBoids();
       //Move();
    }

    void Update()
    {
       Move();
    }
    
    public void InstantiateBoids()
    {
        boids = new GameObject[boidsCount];
        for (int i = 0; i < boidsCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(1f, mapSize.y), Random.Range(1f, mapSize.y), Random.Range(1f, mapSize.y));
            boids[i] = Instantiate(boidPrefab, position, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)));
        }
    }

    public void Move()
    {
        for (int i = 0; i < boidsCount; i++)
        {
            boids[i].transform.position += new Vector3(Random.Range(1f, mapSize.y), Random.Range(1f, mapSize.y), Random.Range(1f, mapSize.y)) * 0.001f;
        }
    }

    public void Align()
    {
        int boidsRange = 50;
        Vector3 avg = new Vector3();
        int x = -1;
        GameObject[] boidsGroup = new GameObject[boidsCount];
        for(int i = 0; i < boidsCount; i++)
        {            
            if (Vector3.Distance(boids[0].transform.position, boids[i].transform.position) < 50)
            {
                boidsGroup[++x] = boids[i];
                avg += boids[i].transform.position;
            }            
        }
    }
}
