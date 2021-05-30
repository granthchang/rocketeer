using UnityEngine;

public class Node : MonoBehaviour
{
	[SerializeField] private Sprite[] spriteList;

	private int[] lastSprites;

	private void Awake() {
		lastSprites = new int[2];
		lastSprites[0] = 0;
		lastSprites[1] = 1;
	}

	private void Start() {
		GameEvents.current.onGameOver += onGameOver;
		if (PlayerPrefs.GetInt("Difficulty") == 1)
			this.GetComponent<CircleCollider2D>().radius = 1.5f;
		else
			this.GetComponent<CircleCollider2D>().radius = 2;
	}

	// Disables object when off screen (bottom)
	private void FixedUpdate() {
		if (this.transform.position.y - Camera.main.transform.position.y < -12) {
			Destroy(this.gameObject);
			GameEvents.current.onGameOver -= onGameOver;
		}
	}

	// Sets the position of the node to (x,y)
	public void SetPosition(float x, float y) {
		this.transform.position = new Vector3(x, y, 0);
	}

	// Destroy nodes when game ends
	private void onGameOver() {
		this.gameObject.SetActive(false);
	}

	// Sets sprite to a random sprite from list
	public void setSprite(int index) {
		this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteList[index];
	}
}
