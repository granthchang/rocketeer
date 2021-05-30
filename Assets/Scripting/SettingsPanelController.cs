using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public static SettingsPanelController main;

    [SerializeField] private Text values;

    private void Awake() {
        main = this;
    }

    // Prepares settingsPanel to be shown
    public void prepareSettings() {
        // Set values
        values.text = PlayerPrefs.GetFloat("Normal High Score") + "\n\n";
        values.text += PlayerPrefs.GetFloat("Hard High Score") + "\n\n";
        values.text += ((int)PlayerPrefs.GetFloat("Total Distance")) + "\n\n";
        values.text += PlayerPrefs.GetInt("Total Games") + "\n\n";
        values.text += PlayerPrefs.GetInt("Rockets Unlocked") + " / 9" + "\n\n";
        values.text += PlayerPrefs.GetInt("Times Crashed") + "\n\n";
        values.text += PlayerPrefs.GetInt("Times Lost") + "\n\n";
        values.text += PlayerPrefs.GetInt("Times Fueled");
    }

}

