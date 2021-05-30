using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] rockets;
    [SerializeField] private Sprite[] flames;
    [SerializeField] private GameObject child;

    void Start()
    {
        // Set rocket sprite
        this.GetComponent<SpriteRenderer>().sprite = rockets[PlayerPrefs.GetInt("Default Rocket")];

        // Set flame offset
        switch (PlayerPrefs.GetInt("Default Rocket")) {
            // Orange fire
            case 0: case 1: case 2:
                child.GetComponent<SpriteRenderer>().sprite = flames[0];
                child.transform.localScale = new Vector3(1.2f, 1, 1);
                child.transform.localPosition = new Vector3(0.005f, -0.4f, 0);
                break;
            // Fin
            case 4:
                child.GetComponent<SpriteRenderer>().sprite = flames[1];
                child.transform.localScale = new Vector3(1, 1, 1);
                child.transform.localPosition = new Vector3(-0.06f, -0.33f, 0);
                break;
            // Orange fire (NASA offset)
            case 5:
                child.GetComponent<SpriteRenderer>().sprite = flames[0];
                child.transform.localScale = new Vector3(1, 1, 1);
                child.transform.localPosition = new Vector3(0, -0.65f, 0);
                break;
            // Blue fire
            case 6:
                child.GetComponent<SpriteRenderer>().sprite = flames[2];
                child.transform.localScale = new Vector3(1, 1, 1);
                child.transform.localPosition = new Vector3(0, -0.4f, 0);
                break;
            // Blue beam
            case 7:
                child.GetComponent<SpriteRenderer>().sprite = flames[3];
                child.transform.localScale = new Vector3(0.95f, 1, 1);
                child.transform.localPosition = new Vector3(-0.02f, -0.45f, 0);
                break;
            // None otherwise
            default:
                child.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }
}
