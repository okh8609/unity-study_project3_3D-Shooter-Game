using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilGen : MonoBehaviour
{
    public GameObject evil;
    public float period;
    float next_time_for_gen;

    // Start is called before the first frame update
    void Start()
    {
        next_time_for_gen = Time.time;
        Instantiate(evil, new Vector3(Random.Range(0.0f, 20.0f), 0.0f, Random.Range(0.0f, 20.0f)), Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > next_time_for_gen && GameObject.Find("Player"))
        {
            next_time_for_gen = Time.time + Random.Range(5.0f, period);
            Instantiate(evil, new Vector3(Random.Range(-15.0f, 15.0f), 0.0f, Random.Range(-15.0f, 15.0f)), Quaternion.Euler(0, 0, 0));
        }
    }
}
