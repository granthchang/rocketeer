using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour {
    public static GameEvents current;

    [Header("Settings")]
    [SerializeField, Tooltip("Delay before game over screen")] private float gameOverDelay;
    [SerializeField, Tooltip("Delay before game restarts")] private float restartDelay;


    // Singleton event system
    void Awake() { current = this; }

    // Game Over event
    public event Action onGameOver;
    public void gameOver() {
        StartCoroutine(gameOverCoroutine());
    }
    IEnumerator gameOverCoroutine() {
        yield return new WaitForSeconds(gameOverDelay);
        onGameOver?.Invoke();
    }

    // Crash event
    public event Action onCrash;
    public void crash() {
        onCrash?.Invoke();
        gameOver();
    }

    // Out of fuel event
    public event Action onOutOfFuel;
    public void outOfFuel() {
        onOutOfFuel?.Invoke();
        gameOver();
    }

    // Lost in space event
    public event Action onLostInSpace;
    public void lostInSpace() {
        onLostInSpace?.Invoke();
        gameOver();
    }

    // Tap and start event
    public event Action onTap;
    public event Action onStart;
    public void tap() {
        if (RuntimeController.current.phase != RuntimeController.Phase.paused)
            onTap?.Invoke();
        if (RuntimeController.current.phase == RuntimeController.Phase.pre)
            StartCoroutine(startCoroutine());
    }
    IEnumerator startCoroutine() {
        yield return new WaitForSeconds(gameOverDelay);
        onStart?.Invoke();
    }

    // Restarts game
    public event Action onRestart;
    public void restart() {
        StartCoroutine(restartCoroutine());
    }
    IEnumerator restartCoroutine() {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene("Game");
    }

    // Pauses game
    public event Action onPause;
    public event Action onResume;
    public void pause(bool b) {
        if (b)
            onPause?.Invoke();
        else
            onResume?.Invoke();
    }
}
