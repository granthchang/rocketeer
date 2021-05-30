using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketSelect : MonoBehaviour {
    [SerializeField] private Sprite unlocked;
    [SerializeField] private Sprite locked;
    [SerializeField] private int rocketNumber;
    private SVGImage image;
    private Button button;

    private void Start() {
        GameEvents.current.onGameOver += lockRocket;

        image = this.GetComponent<SVGImage>();
        button = this.GetComponent<Button>();
        image.sprite = locked;
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void unlock() {
        image.sprite = unlocked;
        var temp = image.color;
        temp.a = 1;
        image.color = temp;
        button.enabled = true;

        // Show selected box if default
        if (PlayerPrefs.GetInt("Default Rocket") == rocketNumber)
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void lockRocket() {
        image.sprite = locked;
        var temp = image.color;
        temp.a = 0.2f;
        image.color = temp;
        button.enabled = false;
    }
}
