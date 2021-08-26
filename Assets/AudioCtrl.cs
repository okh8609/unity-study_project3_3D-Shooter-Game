using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject VolCtrlPanel;

    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider EffectVolume;

    // Start is called before the first frame update
    void Start()
    {
        VolCtrlPanel.SetActive(false);
        MasterVolume.onValueChanged.AddListener(delegate { mixer.SetFloat("Master", MasterVolume.value); });
        MusicVolume.onValueChanged.AddListener(delegate { mixer.SetFloat("Master", MusicVolume.value); });
        EffectVolume.onValueChanged.AddListener(delegate { mixer.SetFloat("Master", EffectVolume.value); });
    }

    // Update is called once per frame
    void Update()
    {
        float master_adj = 0;
        if (Input.GetKey(KeyCode.PageUp))
            master_adj = 20.0f * Time.deltaTime;
        if (Input.GetKey(KeyCode.PageDown))
            master_adj = -20.0f * Time.deltaTime;
        if (master_adj > 0.00001 || master_adj < -0.00001)
        {
            float master_volume;
            mixer.GetFloat("Master", out master_volume);
            master_volume = Mathf.Clamp(master_volume + master_adj, -80, 20);
            mixer.SetFloat("Master", master_volume);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1;
                VolCtrlPanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                VolCtrlPanel.SetActive(true);
            }
        }

    }
}
