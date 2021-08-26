using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Ctrl : MonoBehaviour
{
    public float HP;

    public AudioSource GetHurtAudio;

    public float hurt_period;
    float next_can_hurt;

    public Image HurtMask;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetFloat("HP", HP);
        next_can_hurt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionStay(Collision collision)
    {
        HurtCtrl hurt = collision.gameObject.GetComponent<HurtCtrl>();

        if (hurt && HP > 0.001
            && collision.gameObject.GetComponent<HP_Ctrl>().HP > 0.001
            && next_can_hurt < Time.time) // 且必須是對手
        {
            next_can_hurt = Time.time + hurt_period;
            float power = hurt.GetHurt();
            if (power > 0)
            {
                PlayGetHurtAudio();
                HP -= power;
                //print($"角色：{this.name} 之 HP = {this.HP} [BY:{collision.gameObject.name}]");
                this.GetComponent<Animator>().SetFloat("HP", HP);
                if (this.name == "Player")
                {
                    this.gameObject.GetComponent<PlayerCtrl>().HP_bar.value = HP;
                    StartCoroutine(ShowHurtMask());
                }
            }
            if (HP < 0.001) SendMessage("Die");
        }
    }

    public HP_Ctrl PlayGetHurtAudio()
    {
        //AudioSource.PlayClipAtPoint(GetHurtAudio, this.transform.position, 1);
        GetHurtAudio.Play();
        return this;
    }

    IEnumerator ShowHurtMask()
    {
        Color c = Color.red;
        c.a = 20.0f / 256.0f;

        this.HurtMask.color = c;

        yield return new WaitForSeconds(0.1f);

        this.HurtMask.color = Color.clear;
    }
}
