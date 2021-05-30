using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
    public static AudioController main;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource buttons;
    [SerializeField] private AudioSource rocket;

    [SerializeField] private AudioClip crash;

    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject soundButton;


    // Set singleton object to AudioController.main
    void Awake() {
        if (main == null) {
            main = this;
            DontDestroyOnLoad(this);
            music.Play();
        } else
            Destroy(this.gameObject);
    }

    // Keep audio with camera
    private void Update() {
        this.transform.position = Camera.main.transform.position;
    }

    // Turn music on or off depending on last value
    private void Start() {
        if (PlayerPrefs.GetInt("Music") == 1) {
            music.mute = true;
            musicButton.GetComponent<Toggle>().isOn = true;
            musicButton.transform.GetChild(0).GetComponent<SVGImage>().enabled = true;
        }
        if (PlayerPrefs.GetInt("Sound") == 1) {
            buttons.mute = true;
            rocket.mute = true;
            soundButton.GetComponent<Toggle>().isOn = true;
            soundButton.transform.GetChild(0).GetComponent<SVGImage>().enabled = true;
        }
        buttons.Stop();
    }

    // Turns off music and saves preference for the next time the app is opened
    public void muteMusic(bool b) {
        if (b) {
            PlayerPrefs.SetInt("Music", 1);
            music.mute = true;
        } else {
            PlayerPrefs.SetInt("Music", 0);
            music.mute = false;
        }
    }

    // Turns off sounds and saves preference for the next time the app is opened
    public void muteSound(bool b) {
        if (b) {
            PlayerPrefs.SetInt("Sound", 1);
            buttons.mute = true;
            rocket.mute = true;
        } else {
            PlayerPrefs.SetInt("Sound", 0);
            buttons.mute = false;
            rocket.mute = false;

        }
    }

    // Plays rocket booster sound
    public void playRocketBooster() {
        rocket.Stop();
        StopAllCoroutines();
        rocket.volume = 0.05f;
        rocket.Play();
    }

    // Stops rocket booster sound
    public void stopRocketBooster() {
        rocket.Stop();
    }

    // Start coroutine to fade rocket booster sound
    public void fadeRocketBooster() {
        // Stop sound here
        StopAllCoroutines(); // So you don't get overlapping behavior
        StartCoroutine(fadeBooster(0.1f));
    }

    private IEnumerator fadeBooster(float fadeRate) {
        while (rocket.volume > Mathf.Epsilon) {
            rocket.volume -= Time.deltaTime * fadeRate;
            yield return null;
        }
        rocket.volume = 0f;
        rocket.Stop();
    }

    // Plays rocket crash sound
    public void playRocketCrash() {
        rocket.Stop();
        StopAllCoroutines();
        rocket.volume = 0.2f;
        rocket.PlayOneShot(crash, 0.25f);
    }    
}
