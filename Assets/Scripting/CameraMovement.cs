using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Settings")]
        [SerializeField, Tooltip("Delay in seconds following movement of Player")] private float delay;

    private bool moving;
    private Vector3 velocity;
    private Vector3 drift;

    // Called at scene initialization
    void Start() {
        GameEvents.current.onGameOver += onGameOver;
        GameEvents.current.onTap += tap;
        GameEvents.current.onPause += onPause;
        GameEvents.current.onResume += onResume;

        moving = false;
    }

    // Moves up at the same rate as the player in linear movement, stops moving when the player has latched
    void FixedUpdate() {
        if (moving) {
            // Match velocity of player if drifting
            if (Player.main.getStage() == Player.MoveStage.drift) {
                drift = new Vector3(0, Player.main.getVelocity().y);
                this.transform.position += drift;
            }
            // Otherwise move up at normal velocity
            else
                this.transform.position += velocity;

            // Stop camera when it gets to latch
            if (this.transform.position.y >= Player.main.getLatch()?.transform.position.y + 5) {
                moving = false;
                velocity = Vector3.zero;
            } 
        }
    }

    // Move camera up when tapped
    private void tap() {
        StartCoroutine(delayMove());
    }

    // Wait to start animation
    private IEnumerator delayMove() {
        yield return new WaitForSeconds(delay);
        velocity = new Vector3(0, Player.main.getVelocity().y);
        moving = true;

        // But only move forward, not backward
        if (velocity.y <= 0) {
            moving = false;
        }
    }

    // Game over event
    private void onGameOver() {
        moving = false;
        this.enabled = false;
    }

    // Pause event
    private void onPause() {
        this.moving = false;
    }
    
    // Resume event
    void onResume() {
        this.moving = true;
    }
}
