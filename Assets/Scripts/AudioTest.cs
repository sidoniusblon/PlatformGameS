using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioTest : MonoBehaviour
{
    public static AudioTest Instance;
    [SerializeField] AudioSource backgroundMusic,pointMusicp,DeadMusic,damage,fall,win,enemydead;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI tmp;
    public float vol;
    private void Awake()
    {
        if (Instance == null) { Instance = this; startMUsic(); }
        else Destroy(this.gameObject);

        tmp.text = "Volume: %" + (int)(100 * slider.value);
        slider.value = PlayerPrefs.GetFloat("SoundLevel");
        vol = slider.value;
        DontDestroyOnLoad(this.gameObject);

    }
    public void Slider()
    {
        tmp.text = "Volume: %" + (int)(100*slider.value);
        backgroundMusic.volume = slider.value;
        pointMusicp.volume = slider.value;
        DeadMusic.volume = slider.value;
        damage.volume = slider.value;
        fall.volume = slider.value;
        win.volume = slider.value;
        enemydead.volume = slider.value;
        vol = slider.value;
        PlayerPrefs.SetFloat("SoundLevel", slider.value);
    }
    void startMUsic()
    {
        backgroundMusic.Play();
    }
    public void pointMusic()
    {
        pointMusicp.Play();
    }
    public void deadMusicPlay()
    {
        DeadMusic.Play();
    }
    public void deadMusicstop()
    {
        DeadMusic.Stop();
    }
    public void damageMusic()
    {
        damage.Play();
    }
    public void fallMusic()
    {
        fall.Play();
    }
    public void winMusic()
    {
        win.Play();
    }
    public void enemyDeadMusic()
    {
        enemydead.Play();
    }
}
