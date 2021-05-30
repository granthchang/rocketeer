using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    private Image overlay;
    private bool fading;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        overlay = this.GetComponent<Image>();
        fading = false;
        StartCoroutine(fade());
        color.a = 0;
    }

    IEnumerator fade() {
        yield return new WaitForSeconds(4);
        fading = true;
        StartCoroutine(startGame());
    }

    private void FixedUpdate() {
        if (fading) {
            color.a += 0.05f;
            overlay.color = color;
        }
    }

    IEnumerator startGame() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }
}
