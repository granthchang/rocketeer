using UnityEngine;
using UnityEngine.UI;

public class RocketPanelController : MonoBehaviour
{
    public static RocketPanelController main;

    [SerializeField] private Text totalDist;
    [SerializeField] private Text unit;
    [SerializeField] public float distancePerRocket;

    private int unlocked;
    private float remainder;

    private void Awake() {
        main = this;
    }

    // Prepares rocketPanel to be shown
    public void prepareRocketPanel() {
        // Set values
        unlocked = PlayerPrefs.GetInt("Rockets Unlocked");
        remainder = distancePerRocket - (PlayerPrefs.GetFloat("Total Distance") % distancePerRocket);

        // Set text
        totalDist.text = remainder.ToString("F2") + " / " + distancePerRocket + " AU";
        if (unlocked >= 9) {
            totalDist.text = "All rockets unlocked";
            unit.enabled = false;
        }

        // Unlock (or lock) rockets
        for (int i = 0; i < 9; i++) {
            if (i < unlocked)
                this.transform.GetChild(i).GetComponent<RocketSelect>().unlock();
            else
                this.transform.GetChild(i).GetComponent<RocketSelect>().lockRocket();
        }
    }

    public void setSprite(int index) {
        PlayerPrefs.SetInt("Default Rocket", index);
        for (int i = 0; i < 9; i++) {
            if (index == i)
                this.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            else
                this.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
}
