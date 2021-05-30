using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject pause;
    [Header("Layout 0")]
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject rockets;
    [Header("Layout 1")]
    [SerializeField] private GameObject backLeft;
    [Header("Layout 2")]
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject sound;
    [SerializeField] private GameObject backRight;
    [SerializeField] private SVGImage difficulty;
    [SerializeField] private Text diffLabel;
    [Header("Load")]
    [SerializeField] private Sprite offSwitch;
    [SerializeField] private Sprite onSwitch;


    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCrash += turnOffMain;
        GameEvents.current.onOutOfFuel += turnOffMain;
        GameEvents.current.onLostInSpace += turnOffMain;
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onStart += onStart;

        restart.SetActive(false);
        rockets.SetActive(false);
        settings.SetActive(false);
        pause.SetActive(false);
        main.SetActive(true);

        if (PlayerPrefs.GetInt("Difficulty") == 1) {
            setHardCore(true);
            difficulty.GetComponent<Toggle>().isOn = true;
        } else
            setHardCore(false);
    }

    // Hide runtime buttons
    void turnOffMain() {
        pause.SetActive(false);
        main.SetActive(false);
    }

    // Show endscreen buttons
    void onGameOver() {
        restart.SetActive(true);
        rockets.SetActive(true);
        settings.SetActive(true);
    }

    // Lowers original buttons
    public void buttonsDown(int from) {
        switch (from) {
            case 0:
                restart.GetComponent<Animator>().Play("Button Down 2", -1, 0f);
                rockets.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                settings.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                break;
            case 1:
                backLeft.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                break;
            case 2:
                sound.GetComponent<Animator>().Play("Button Down 2", -1, 0f);
                backRight.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                music.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                break;
            case 3:
                rockets.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                settings.GetComponent<Animator>().Play("Button Down 1", -1, 0f);
                break;
            default:
                break;
        }
    }

    // Raises next buttons
    public void buttonsUp(int to) {
        StartCoroutine(buttonUpCoroutine(to));
    }

    // Button animation coroutine
    private IEnumerator buttonUpCoroutine(int to) {
        yield return new WaitForSeconds(0.2f);
        
        // Raise buttons
        switch (to) {
            case 0:
                restart.GetComponent<Animator>().Play("Button Up 1", -1, 0f);
                rockets.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                settings.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                break;
            case 1:
                backLeft.GetComponent<Animator>().Play("Button Up 1", -1, 0f);
                break;
            case 2:
                sound.GetComponent<Animator>().Play("Button Up 1", -1, 0f);
                backRight.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                music.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                pause.SetActive(false);
                break;
            case 3:
                rockets.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                settings.GetComponent<Animator>().Play("Button Up 2", -1, 0f);
                break;
            default:
                break;
        }
    }

    // Resets high score
    public void resetHighScore() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Rockets Unlocked", 1);
        SettingsPanelController.main.prepareSettings();
    }

    // Show pause when the game actually starts
    void onStart() {
        pause.SetActive(true);
    }

    // Toggle difficulty
    public void setHardCore(bool b) {
        if (b) {
            PlayerPrefs.SetInt("Difficulty", 1);
            difficulty.sprite = onSwitch;
            diffLabel.text = "Hardcore";
        } else {
            PlayerPrefs.DeleteKey("Difficulty");
            difficulty.sprite = offSwitch;
            diffLabel.text = "Normal";
        }
    }
}
