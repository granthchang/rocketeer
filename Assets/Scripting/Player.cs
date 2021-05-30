using UnityEngine;

public class Player : MonoBehaviour {
	public static Player main;

	[Header("Object References")]
		[SerializeField] private GameObject startingNode;
		[SerializeField] private GameObject flame;

	[Header("Settings")]
		[SerializeField] private float speed;
		[SerializeField] private float expansionRate;
		[SerializeField, Tooltip("Percent of fuel consumed in 1 second")] private float fuelDepletion;
		[SerializeField, Tooltip("Percent of fuel regenerated in 1 second")] private float fuelRegeneration;
		[SerializeField, Tooltip("How fast the player comes to halt in drift")] private float driftStop;

	// Movement controls
	private GameObject latch;
	public enum MoveStage {linear, searching, angular, drift}; private MoveStage stage;
	private Vector3 velocity;
	private bool clockwise;
	private float radius, angle;
	private float fuel;

	// Set singleton object to Player.main
	private void Awake() {
		main = this;	
	}

	// Start scene with searching movement on the starting node
	private void Start() {
		GameEvents.current.onGameOver += onGameOver;
		GameEvents.current.onTap += tap;

		if (PlayerPrefs.GetInt("Difficulty") == 1)
			this.transform.position = new Vector2(-1, -5);
		else
			this.transform.position = new Vector2(-1.8f, -5);

		// Start movement
		latch = startingNode;
		radius = findDistanceFromObject(latch);
		startSearching();
		fuel = 1.0f;
		flame.SetActive(false);
	}

	// Called at tap event (when main button is pressed)
	private void tap() {
		switch (stage) {
			case MoveStage.angular:
				startLinear();
				break;
			case MoveStage.searching:
				latch.GetComponent<Collider2D>().enabled = false;
				startLinear();
				break;
			default:
				latch.GetComponent<Collider2D>().enabled = false;
				latch = null;
				radius = float.MaxValue;
				stage = MoveStage.linear;
				break;
		}
	}

	// Move every physics tick, handle fuel
	private void FixedUpdate() {
		if (RuntimeController.current.phase != RuntimeController.Phase.paused) {
			// Move and handle fuel
			switch (stage) {
				case MoveStage.angular:
					angularMovement();
					fuel = 1.0f;
					break;
				case MoveStage.searching:
					searchMovement();
					fuel = 1.0f;
					break;
				case MoveStage.linear:
					linearMovement();
					fuel -= fuelDepletion * .02f;
					break;
				default:
					driftMovement();
					break;
			}
			// Check if out of fuel of at max fuel
			if (fuel <= 0) {
				stage = MoveStage.drift;
				flame.SetActive(false);
				if (velocity.magnitude <= 0.002f && velocity.magnitude != 0) {
					this.velocity = Vector3.zero;
					GameEvents.current.outOfFuel();
				} else
					AudioController.main.fadeRocketBooster();
			}
		}
	}





	// Angular movement around given node
	private void angularMovement() {
		// Rotate around node
		if (!clockwise) {
			angle += speed / radius * Mathf.Deg2Rad;
			this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
		} else {
			angle -= speed / radius * Mathf.Deg2Rad;
			this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 180);
		}
		angle = normalizeAngle(angle);
		this.transform.position = latch.transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
		AudioController.main.fadeRocketBooster();
	}

	// Rotate around latched node and increase radius and spiral outward
	private void searchMovement() {
		angularMovement();
		radius += expansionRate * 0.01f;
		AudioController.main.fadeRocketBooster();
	}

	// Move in a straight line
	private void linearMovement() {
		transform.position += velocity;
	}

	// Drift to a stop
	private void driftMovement() {
		velocity -= velocity * driftStop * 0.02f;
		transform.position += velocity;
	}





	// Register settings to rotate around node
	private void startAngular() {
		angle = findAngleFromObject(latch);
		radius = findDistanceFromObject(latch);
		stage = MoveStage.angular;
		flame.SetActive(false);
	}

	// Register settings to spiral around node; detects wheter or not to move clockwise
	private void startSearching() {
		angle = findAngleFromObject(latch);

		if (this.transform.position.x > latch.transform.position.x)
			clockwise = false;
		else
			clockwise = true;
		stage = MoveStage.searching;
		flame.SetActive(false);
	}

	// Release from latch and switch to linear movement
	private void startLinear() {
		// Set linear velocity
		angle = findAngleFromObject(latch);
		if (!clockwise)
			angle += Mathf.PI * 0.5f;
		else
			angle -= Mathf.PI * 0.5f;
		angle = normalizeAngle(angle);

		velocity.x = Mathf.Cos(angle);
		velocity.y = Mathf.Sin(angle);
		velocity = velocity.normalized * speed * 0.02f;

		latch = null;
		radius = float.MaxValue;
		stage = MoveStage.linear;
		flame.SetActive(true);

		AudioController.main.playRocketBooster();
	}





	// Set the latch when entering Node trigger
	private void OnTriggerEnter2D(Collider2D collision) {
		GameObject obj = collision.gameObject;
		if (obj.tag.Equals("Node"))
			latch = obj;
	}

	// Check when to latch onto node
	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject == latch && (stage == MoveStage.linear || stage == MoveStage.drift)) {
			// Waits until the player is as close as it can be to the node
			float temp = findDistanceFromObject(latch);
			if (temp < radius)
				radius = temp;
			else {
				radius = temp;
				startSearching();
			}
		}
	}

	// Start angular movement when Player passes the edge of the Node's collider
	private void OnTriggerExit2D(Collider2D collision) {
		GameObject obj = collision.gameObject;
		if (obj == latch)
			startAngular();
	}



	

	// Returns angle from given object to Player in radians
	private float findAngleFromObject(GameObject obj) {
		float deltaX = transform.position.x - obj.transform.position.x;
		float deltaY = transform.position.y - obj.transform.position.y;
		float result = Mathf.Atan2(deltaY, deltaX);
		if (result < 0)
			return result + 2 * Mathf.PI;
		return result;
	}

	// Returns distance from given object to Player (regardless of Z axis)
	private float findDistanceFromObject(GameObject obj) {
		return Vector2.Distance(this.transform.position, obj.transform.position);
	}

	// Returns the input (in radians) as the same angle from 0 <= x < 2PI
	private float normalizeAngle(float input) {
		if (input >= 0 && input < 2 * Mathf.PI)
			return input;
		input %= 2 * Mathf.PI;
		if (input < 0)
			input += 2 * Mathf.PI;
		return input;
	}





	// Returns linear velocity of Player
	public Vector3 getVelocity() {
		switch (stage) {
			case MoveStage.linear:
			case MoveStage.drift:
				return velocity;
			default:
				return Vector3.zero;
		}
	}
	
	// Returns latched Node; null if there is none
	public GameObject getLatch() {
		return latch;
	}

	// Returns the percentage of fuel left (float)
	public float getFuel() {
		return fuel;
	}

	// Returns move stage of player
	public MoveStage getStage() {
		return stage;
	}

	// Game events
	private void onGameOver() {
		this.enabled = false;
		this.GetComponent<SpriteRenderer>().enabled = false;
		AudioController.main.playRocketCrash();

	}
}
