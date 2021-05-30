using UnityEngine;

public class RuntimeController : MonoBehaviour
{
    public static RuntimeController current;

    public enum Phase {pre, running, paused}; public Phase phase;

    private void Awake() {
        current = this;
    }

    void Start()
    {
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onStart += onStart;
        GameEvents.current.onPause += onPause;
        GameEvents.current.onResume += onResume;

        phase = Phase.pre;
    }

    void onGameOver() {
        phase = Phase.paused;
    }

    void onStart() {
        phase = Phase.running;
    }

    public void onPause() {
        phase = Phase.paused;
        AudioController.main.fadeRocketBooster();
    }

    public void onResume() {
        if (Camera.main.transform.position.y <= 0)
            phase = Phase.pre;
        else {
            phase = Phase.running;
        }
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
		}
	}
}
