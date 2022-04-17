using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    internal string ismute = "ismute";
    float vol;
    private void Awake()
    {
        if(this.gameObject.name!= "SceneGeçiş")
        {
            if (PlayerPrefs.HasKey(ismute))
            {
                vol = PlayerPrefs.GetFloat(ismute);
                audioSource.volume = vol;
                sound_Of_On.isOn = (vol > 0.0f) ? true : false;
            }
        }
        
    }

    public Toggle sound_Of_On;
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip hitToWall;
    public AudioClip switchLevelPage;
    public AudioClip toRed, toGreen;
    public AudioClip inBasket, completedSound;

    public void PlayClickSound()//click ile
    {
        print("sound method");
        audioSource.clip = click;
        audioSource.Play();
    }
    public void PlayHitToWallSound()//boş
    {
        audioSource.clip = hitToWall;
        audioSource.Play();
    }
    public void PlaySwitchLevelSound()//tıklamayla
    {
        audioSource.clip = switchLevelPage;
        audioSource.Play();
    }
    public void PlayİnBasketSound()
    {
        audioSource.clip = inBasket;
        audioSource.Play();
    }
    public void PlayCompletedSound()
    {
        audioSource.clip = completedSound;
        audioSource.Play();
    }
    public void SoundMute()
    {
        if (sound_Of_On.isOn)
        {
            audioSource.volume = .1f;
            PlayerPrefs.SetFloat(ismute, .1f);
        }
        else
        {
            audioSource.volume = 0f;
            PlayerPrefs.SetFloat(ismute, 0);
        }
    }
}
