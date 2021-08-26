using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombunnyCtrl : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;

    public AudioSource DeathAudio;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        if (target != null)
        {
            agent = this.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = this.GetComponent<CapsuleCollider>().radius + target.GetComponent<CapsuleCollider>().radius - 0.05f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            agent.SetDestination(target.transform.position);
        else
        {
            agent.enabled = false;
            this.GetComponent<Animator>().SetBool("GameOver", true);
        }
    }

    void Die()
    {
        //AudioSource.PlayClipAtPoint(DeathAudio, this.transform.position, 1);
        DeathAudio.Play();
        this.enabled = false;
        this.GetComponent<HurtCtrl>().enabled = false;
        this.agent.enabled = false;
        StartCoroutine(Clear());
        StartCoroutine(OffLight(this.GetComponent<Light>()));
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }

    IEnumerator OffLight(Light l)
    {
        l.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].DisableKeyword("_EMISSION");
        while (l.intensity > 0.05f)
        {
            //print(l.intensity);
            l.intensity -= 0.05f;
            yield return new WaitForSeconds(0.08f);
        }
        l.enabled = false;
        //yield return new WaitForSeconds(0.0f);
    }
}
