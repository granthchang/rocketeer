using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [Header("End Panel")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text endText;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text highScore;
    [Header("Score Panel")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Text HUDScore;
    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Animator pauseAnim;
    [Header("Buttons")]
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject mainButton;
    [Header("Other")]
    [SerializeField] private GameObject blur;
    [SerializeField] private Animator whiteoutAnim;
    [Header("Fonts")]
    [SerializeField] private Font heavy;
    [SerializeField] private Font light;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGameOver += showEndPanel;
        GameEvents.current.onCrash += onCrash;
        GameEvents.current.onOutOfFuel += onOutOfFuel;
        GameEvents.current.onLostInSpace += onLostInSpace;

        blur.SetActive(false);
        endPanel.SetActive(false);
        restartButton.SetActive(false);
        whiteoutAnim.gameObject.SetActive(false);
    }

    // Show end panel
    void showEndPanel() {
        // Set values
        if (float.Parse(HUDScore.text) > PlayerPrefs.GetFloat("High Score")) {
            PlayerPrefs.SetFloat("High Score", float.Parse(HUDScore.text));
            highScore.font = heavy;
        } else
            highScore.font = light;
        highScore.text = PlayerPrefs.GetFloat("High Score").ToString();
        finalScore.text = HUDScore.text;
        
        // Show objects
        blur.SetActive(true);
        endPanel.SetActive(true);
        restartButton.SetActive(true);

        // Hide objects
        pauseButton.SetActive(false);
    }

    // Set end text for crash
    void onCrash() {
        whiteoutAnim.gameObject.SetActive(true);
        whiteoutAnim.Play("Whiteout00", -1, 0f);
        endText.text = "You crashed into a planet.";
        pauseButton.SetActive(false);
        mainButton.SetActive(false);
    }

    // Set end text for fuel
    void onOutOfFuel() {
        whiteoutAnim.gameObject.SetActive(true);
        whiteoutAnim.Play("Whiteout00", -1, 0f);
        endText.text = "You ran out of fuel.";
        pauseButton.SetActive(false);
        mainButton.SetActive(false);
    }

    // Set end text for lost
    void onLostInSpace() {
        whiteoutAnim.gameObject.SetActive(true);
        whiteoutAnim.Play("Whiteout00", -1, 0f);
        endText.text = "You got lost in space.";
        pauseButton.SetActive(false);
        mainButton.SetActive(false);
    }
}
