using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollision : MonoBehaviour
{
    // End game when crashing into planet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            GameEvents.current.crash();
        }
    }
}
