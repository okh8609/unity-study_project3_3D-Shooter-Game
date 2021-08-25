using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombunnyCtrl : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;

    public AudioClip DeathAudio;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = this.GetComponent<CapsuleCollider>().radius + target.GetComponent<CapsuleCollider>().radius - 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            agent.SetDestination(target.transform.position);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(DeathAudio, this.transform.position, 1);
        this.enabled = false;
        this.GetComponent<HurtCtrl>().enabled = false;
        this.agent.enabled = false;
        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }
}
