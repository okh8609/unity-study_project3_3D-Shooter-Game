using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Ctrl : MonoBehaviour
{
    public float HP;

    public AudioClip GetHurtAudio;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetFloat("HP", HP);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionStay(Collision collision)
    {
        HurtCtrl hurt = collision.gameObject.GetComponent<HurtCtrl>();

        if (hurt && HP > 0.001) // 且必須是對手
        {
            float power = hurt.GetHurt();
            if (power > 0)
            {
                PlayGetHurtAudio();
                HP -= power;
            }
            //print($"角色：{this.name} 之 HP = {this.HP} [BY:{collision.gameObject.name}]");
            this.GetComponent<Animator>().SetFloat("HP", HP);
            if (this.name == "Player")
                this.gameObject.GetComponent<PlayerCtrl>().HP_bar.value = HP;
            if (HP < 0.001) SendMessage("Die");
        }
    }

    public HP_Ctrl PlayGetHurtAudio()
    {
        AudioSource.PlayClipAtPoint(GetHurtAudio, this.transform.position, 1);
        return this;
    }
}
