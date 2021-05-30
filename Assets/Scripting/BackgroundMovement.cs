using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    
    [SerializeField] private Sprite mainSprite;
    [SerializeField] private Sprite blurSprite;
    [SerializeField, Tooltip("How fast to move the background relative to the camera")]
        private float multiplier;

    private float length, startPos;

    // Initial values
    private void Start() {
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onPause += onPause;
        GameEvents.current.onResume += onResume;

        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        this.GetComponent<SpriteRenderer>().sprite = mainSprite;
    }

    // Shift the background up
    void Update() {
        // Shift background
        float camPos = Camera.main.transform.position.y;
        this.transform.position = new Vector3(0, startPos + camPos * multiplier, 0);

        // Move Background if necessary
        if (camPos - this.transform.position.y > length)
            startPos += 2 * length;
    }

    // When game ends
    void onGameOver() {
        this.GetComponent<SpriteRenderer>().sprite = blurSprite;
    }

    // When game is paused
    private void onPause() {
        this.GetComponent<SpriteRenderer>().sprite = blurSprite;
    }

    // When game is resumed
    private void onResume() {
        this.GetComponent<SpriteRenderer>().sprite = mainSprite;
    }
}
