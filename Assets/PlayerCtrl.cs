using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    float dy = Input.GetAxis("Vertical");
    //    float dx = Input.GetAxis("Horizontal");
    //    //print($"dy:{dy} ¡F dx:{dx}");

    //    PlayWalkingAnimation(dy, dx);

    //    //SimpleMovement(dy, dx);
    //    CCMovement(dy, dx);
    //}

    void PlayWalkingAnimation(float dy, float dx)
    {
        if (dy > 0.1 || dy < -0.1 || dx > 0.1 || dx < -0.1)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);
    }
    void SimpleMovement(float dy, float dx)
    {
        float speed = 3.5f;
        Vector3 direction = (new Vector3(dx, 0, dy)).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            transform.Translate(movement, Space.World);
            transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(dx, dy) * Mathf.Rad2Deg, 0));
        }
    }
    void CCMovement(float dy, float dx)
    {
        float speed = 3.5f;
        Vector3 direction = (new Vector3(dx, 0, dy)).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            cc.Move(movement);
            transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(dx, dy) * Mathf.Rad2Deg, 0));
        }
    }

    private void FixedUpdate()
    {
        float dy = Input.GetAxis("Vertical");
        float dx = Input.GetAxis("Horizontal");
        //print($"dy:{dy} ¡F dx:{dx}");

        PlayWalkingAnimation(dy, dx);

        RigidbodyMovement(dy, dx);
    }

    void RigidbodyMovement(float dy, float dx)
    {
        float speed = 3.5f;
        Vector3 direction = (new Vector3(dx, 0, dy)).normalized;
        Vector3 movement = direction * speed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            rigidbody.MovePosition(transform.position + movement);
            rigidbody.MoveRotation(Quaternion.LookRotation(movement));
        }
    }


}
