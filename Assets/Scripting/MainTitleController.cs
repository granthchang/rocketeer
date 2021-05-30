using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleController : MonoBehaviour
{
    // Show in the beginning
    void Start()
    {
        GameEvents.current.onStart += onStart;
    }

    void onStart() {
        this.GetComponent<Animator>().Play("Title Out");
    }
}
