using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Header("Text Boxes")]
        [SerializeField] private Text center;
        [SerializeField] private Text score;
        [SerializeField] private Text unit;
        [SerializeField] private Text finalScore;
        [SerializeField] private Text finalUnit;

    [Header("Event Text")]
        [SerializeField, TextArea] private string gameOver = "Game Over";

    [Header("Fonts")]
    [SerializeField] private Font lightFont;
    [SerializeField] private Font heavyFont;

    private Vector3 scorePlace, unitPlace;

    // Called at the very beginning of the scene
    private void Start() {
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onStart += onStart;

        center.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        unit.gameObject.SetActive(false);
        scorePlace = score.transform.localPosition;
        unitPlace = unit.transform.localPosition;
    }

    // Updates score
    private void Update() {
        score.text = (Camera.main.transform.position.y + 5000).ToString("F2");
    }

    // Game over text
    private void onGameOver() {
        center.text = gameOver;
        center.gameObject.SetActive(true);
    }

    // Moves score to a specific part of the screen (with different formatting
    void moveScore(String place) {
        if (place.Equals("top")) {
            score.font = heavyFont;
            score.fontSize = 100;
            score.transform.localPosition = scorePlace;
            unit.text = "AU";
            unit.fontSize = 60;
            unit.transform.localPosition = unitPlace;
        } else if (place.Equals("middle")) {
            score.font = lightFont;
            score.fontSize = 150;
            score.transform.localPosition = new Vector3(0,-100);
            unit.text = "Astronomical Units travelled";
            unit.fontSize = 70;
            unit.transform.localPosition = new Vector3(0, -272);
        }
    }

    // Show score text when the game starts (with 0.5 second delay)
    void onStart() {
        score.gameObject.SetActive(true);
        unit.gameObject.SetActive(true);
    }
}
