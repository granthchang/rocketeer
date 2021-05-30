using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private bool clickable;

    // Start the scene as clickable
    void Start() {
        clickable = true;
    }

    // Register taps or clicks with each frame
    void Update()
    {
        // Detect tap
        if (Input.touchCount > 0) {
            if (clickable) {
                clickable = false;
                Vector3 inputPos3D = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 inputPos2D = new Vector2(inputPos3D.x, inputPos3D.y);
                RaycastHit2D hitInformation = Physics2D.Raycast(inputPos2D, Camera.main.transform.forward);
                tap(hitInformation.collider);
            } else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                clickable = true;
        }

        // Detect click
        if (Input.GetMouseButtonDown(0) && clickable) {
            clickable = false;
            Vector3 inputPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 inputPos2D = new Vector2(inputPos3D.x, inputPos3D.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(inputPos2D, Camera.main.transform.forward);
            tap(hitInformation.collider);
        } else if (Input.GetMouseButtonUp(0))
            clickable = true;
    }

    // Project raycast onto specific object from the tap/click
    void tap(Collider2D collider)
    {
        if (RuntimeController.current.phase != RuntimeController.Phase.paused)
            GameEvents.current.tap();
    }
}
