using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightCtrl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ChangeLightColor(this.GetComponent<Light>(), 0.5f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ChangeLightColor(Light light, float time)
    {
        float I = light.intensity;

        while (true)
        {
            light.intensity = Mathf.Clamp(genNormalRandom(I / 2.0f, 35.0f), 5.0f, I + 10.0f);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.35f));
        }

        //while (true)
        //{
        //    while (light.intensity > 5.0f)
        //    {
        //        yield return new WaitForSeconds(time);
        //        light.intensity -= 1.0f;
        //    }
        //    while (light.intensity < I)
        //    {
        //        yield return new WaitForSeconds(time);
        //        light.intensity += 1.0f;
        //    }
        //}
    }

    public static float genNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }
}
