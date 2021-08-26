using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    public float power;

    ParticleSystem particle;
    LineRenderer line;
    public Light light;
    public Light plight;

    int shootable_id;

    public ParticleSystem hit_particle;

    public AudioSource GunShot;

    // Start is called before the first frame update
    void Start()
    {
        particle = this.GetComponentInChildren<ParticleSystem>();
        line = this.GetComponent<LineRenderer>();
        //light = this.GetComponent<Light>();
        //plight = this.GetComponentInChildren<Light>();
        shootable_id = LayerMask.GetMask("Shootable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f)
            shoot();
    }

    void shoot()
    {
        //AudioSource.PlayClipAtPoint(GunShot, this.transform.position, 1);
        GunShot.Play();

        Ray ray = new Ray();
        RaycastHit raycastHit = new RaycastHit(); // ¼uµÛÂI

        particle.Stop();
        particle.Play();
        ray.origin = this.transform.position;
        ray.direction = this.transform.forward;
        line.enabled = true;
        line.SetPosition(0, ray.origin);
        //lr.SetPosition(1, ray.origin + ray.direction * 100.0f); // ·|¬ï¶V
        if (Physics.Raycast(ray, out raycastHit, 100.0f, shootable_id))
        {
            line.SetPosition(1, raycastHit.point);
            StartCoroutine(PlayParticle(Instantiate(hit_particle, raycastHit.point, Quaternion.Euler(raycastHit.normal))));
            if (raycastHit.collider.GetComponent<ZombunnyCtrl>())
            {
                raycastHit.collider.GetComponent<Animator>().SetFloat("HP",
                    raycastHit.collider.GetComponent<HP_Ctrl>().PlayGetHurtAudio().HP -= 5.0f);
            }
        }
        else
        {
            line.SetPosition(1, ray.origin + ray.direction * 100.0f);
        }
        light.enabled = true;
        plight.enabled = true;

        StartCoroutine(Clear());
    }

    IEnumerator PlayParticle(ParticleSystem p)
    {
        p.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(p.gameObject);
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.075f);
        line.enabled = false;
        light.enabled = false;
        plight.enabled = false;
    }
}
