using UnityEngine;
using UnityEngine.UI;
public class ScorePanelController : MonoBehaviour {

	[SerializeField] private Text score;

	// Use this for initialization
	void Start() {
		GameEvents.current.onGameOver += onGameOver;
		GameEvents.current.onStart += onStart;
		GameEvents.current.onPause += onPause;
		GameEvents.current.onResume += onResume;

		for (int i = 0; i < this.transform.childCount; i++)
			this.transform.GetChild(i).gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (RuntimeController.current.phase == RuntimeController.Phase.running)
			score.text = (Camera.main.transform.position.y).ToString("F2");
	}

	void onGameOver() {
		for (int i = 0; i < this.transform.childCount; i++)
			this.transform.GetChild(i).gameObject.SetActive(false);
	}

	void onStart() {
		for (int i = 0; i < this.transform.childCount; i++)
			this.transform.GetChild(i).gameObject.SetActive(true);
	}

	private void onPause() {
		for (int i = 0; i < this.transform.childCount; i++)
			this.transform.GetChild(i).gameObject.SetActive(false);
	}

	private void onResume() {
		if (Camera.main.transform.position.y > 0) {
			for (int i = 0; i < this.transform.childCount; i++)
				this.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
}
