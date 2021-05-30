using UnityEngine;

public class Bounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision) {
        GameObject obj = collision.gameObject;
        if (obj.tag.Equals("Player")) {
            GameEvents.current.lostInSpace();
        }
    }
}
