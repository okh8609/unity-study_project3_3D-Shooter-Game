using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 10;
        float mx = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
            this.transform.Rotate(0, mx * speed, 0, Space.World);
        else if (Input.GetMouseButton(1))
            this.transform.Rotate(0, mx * speed, 0, Space.Self);


        //transform.Translate(0.01f, 0, 0); // 根據 model space 軸向
        //transform.position += new Vector3(0.01f, 0, 0); // 根據 world space 軸向

        //transform.Rotate(0, 0, 0);

        //float x = transform.localEulerAngles.x, y = transform.localEulerAngles.y, z = transform.localEulerAngles.z;
        //transform.localEulerAngles = new Vector3(0, 0, 0);

        //float xx = transform.eulerAngles.x, y = transform.eulerAngles.y, zz = transform.eulerAngles.z;
        //transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
