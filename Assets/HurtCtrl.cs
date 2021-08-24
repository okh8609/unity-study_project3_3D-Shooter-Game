using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCtrl : MonoBehaviour
{
    public float power;
    public float period;
    float next_can_hurt;

    private void Start()
    {
        next_can_hurt = Time.time;
    }
    public float GetHurt()
    {
        if (next_can_hurt < Time.time)
        {
            next_can_hurt += period;
            return power;
        }
        else
        {
            return 0;
        }
    }
}
