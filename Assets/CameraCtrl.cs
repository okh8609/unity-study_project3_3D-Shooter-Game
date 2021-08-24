using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - target.transform.position;
        //transform.parent = null;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Vector3 target_pos = target.transform.position + offset;
    //    this.transform.position = Vector3.Lerp(this.transform.position, target_pos, 3.0f * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        Vector3 target_pos = target.transform.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, target_pos, 10.0f * Time.fixedDeltaTime);
    }
}
