using UnityEngine;
using UnityEngine.UI;

public class EndPanelController : MonoBehaviour {
    [SerializeField] private Animator whiteout;
    [SerializeField] private GameObject blur;
    [SerializeField] private Text HUDScore;
    [SerializeField] private Text centerText;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Font heavy;
    [SerializeField] private Font light;

    [SerializeField] private Sprite[] rockets;
    [SerializeField] private SVGImage rocket;

    private void Start() {
        // Subscribe to events
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onCrash += onCrash;
        GameEvents.current.onOutOfFuel += onOutOfFuel;
        GameEvents.current.onLostInSpace += onLostInSpace;
        
        // Set proper end rocket sprite
        rocket.sprite = rockets[PlayerPrefs.GetInt("Default Rocket")];

        // Hide all child objects
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);
    }

    // Show game over panel
    void onGameOver() {
        // Set high score based on new score
        float score = float.Parse(HUDScore.text);
        currentScoreText.text = score.ToString();

        if (PlayerPrefs.GetInt("Difficulty") == 1) {
            // hardcore high score
            if (score > PlayerPrefs.GetFloat("Hard High Score")) {
                PlayerPrefs.SetFloat("Hard High Score", float.Parse(HUDScore.text));
                highScoreText.font = heavy;
            } else
                highScoreText.font = light;
            highScoreText.text = PlayerPrefs.GetFloat("Hard High Score").ToString();
        
        } else {

            // Normal high score
            if (score > PlayerPrefs.GetFloat("Normal High Score")) {
                PlayerPrefs.SetFloat("Normal High Score", float.Parse(HUDScore.text));
                highScoreText.font = heavy;
            } else
                highScoreText.font = light;
            highScoreText.text = PlayerPrefs.GetFloat("Normal High Score").ToString();
        }


        // Increment global values
        PlayerPrefs.SetFloat("Total Distance", PlayerPrefs.GetFloat("Total Distance") + score);

        int temp = (int)(PlayerPrefs.GetFloat("Total Distance") / RocketPanelController.main.distancePerRocket) + 1;
        if (temp >= 9)
            PlayerPrefs.SetInt("Rockets Unlocked", 9);
        else
            PlayerPrefs.SetInt("Rockets Unlocked", temp);

        PlayerPrefs.SetInt("Total Games", PlayerPrefs.GetInt("Total Games") + 1);
        

        // Show panel
        blur.SetActive(true);
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(true);
    }

    // Set end text for crash
    void onCrash() {
        whiteout.Play("Whiteout00", -1, 0f);
        centerText.text = "You crashed into a planet.";
        PlayerPrefs.SetInt("Times Crashed", PlayerPrefs.GetInt("Times Crashed") + 1);
    }

    // Set end text for fuel
    void onOutOfFuel() {
        whiteout.Play("Whiteout00", -1, 0f);
        centerText.text = "You ran out of fuel.";
        PlayerPrefs.SetInt("Times Fueled", PlayerPrefs.GetInt("Times Fueled") + 1);
    }

    // Set end text for lost
    void onLostInSpace() {
        whiteout.Play("Whiteout00", -1, 0f);
        centerText.text = "You got lost in space.";
        PlayerPrefs.SetInt("Times Lost", PlayerPrefs.GetInt("Times Lost") + 1);
    }
}
