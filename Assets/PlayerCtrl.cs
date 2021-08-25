using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    Rigidbody rigidbody;

    public Camera camera1;
    public Camera camera3;

    public Slider HP_bar;

    int floor_id;

    public AudioClip DeathAudio;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
        rigidbody = this.GetComponent<Rigidbody>();
        floor_id = LayerMask.GetMask("Floor");
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    float dy = Input.GetAxis("Vertical");
    //    float dx = Input.GetAxis("Horizontal");
    //    //print($"dy:{dy} ； dx:{dx}");

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
        //print($"dy:{dy} ； dx:{dx}");

        PlayWalkingAnimation(dy, dx);

        //RigidbodyMovement_3P(dy, dx);
        //RigidbodyMovement_1P(dy, dx);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            camera1.enabled = !camera1.enabled;
            camera3.enabled = !camera3.enabled;
        }

        if (camera1.enabled)
            RigidbodyMovement_1P(dy, dx);
        else if (camera3.enabled)
            RigidbodyMovement_3P(dy, dx);

        camera1.GetComponent<AudioListener>().enabled = camera1.enabled;
        camera3.GetComponent<AudioListener>().enabled = camera3.enabled;
    }

    void RigidbodyMovement_3P(float dy, float dx)
    {
        float speed = 5.0f;
        if (Input.GetKey(KeyCode.Space)) speed *= 2.0f;
        Vector3 direction = (new Vector3(dx, 0, dy)).normalized;
        Vector3 movement = direction * speed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            rigidbody.MovePosition(transform.position + movement);
            //rigidbody.MoveRotation(Quaternion.LookRotation(movement));
        }

        #region 取得滑鼠位置並旋轉
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 200.0f, floor_id))
        {
            Vector3 to_mouse = raycastHit.point - this.transform.position;
            to_mouse.y = 0;
            if (to_mouse.magnitude > 0.75f)
                this.rigidbody.MoveRotation(Quaternion.LookRotation(to_mouse));
        }
        #endregion
    }

    void RigidbodyMovement_1P(float dy, float dx)
    {
        float move_speed = 5.0f;
        if (Input.GetKey(KeyCode.Space)) move_speed *= 2.0f;
        Vector3 movement = this.transform.forward * dy + transform.right * dx; // model space -> world space
        rigidbody.MovePosition(transform.position + movement.normalized * move_speed * Time.fixedDeltaTime);

        float rotate_speed = 12.5f;
        Vector3 rotateAngle = new Vector3(0, this.transform.eulerAngles.y + Input.GetAxis("Mouse X") * rotate_speed, 0);
        rigidbody.MoveRotation(Quaternion.Euler(rotateAngle));

        //float rotate_speed2 = 12.5f;
        //this.transform.Rotate(0, Input.GetAxis("Mouse X") * rotate_speed2, 0, Space.Self);
        ////this.transform.Rotate(0, Input.GetAxis("Mouse X") * rotate_speed2, 0, Space.World);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(DeathAudio, this.transform.position, 1);
        this.GetComponent<Rigidbody>().drag = 1.0e6f;
        this.enabled = false;
        this.HP_bar.fillRect.gameObject.SetActive(false);
        this.GetComponentInChildren<GunCtrl>().enabled = false;
        StartCoroutine(Clear());
    }
    IEnumerator Clear()
    {
        camera1.enabled = false;
        camera3.enabled = true;

        yield return new WaitForSeconds(7.5f);

        Destroy(this.gameObject);
    }
}
