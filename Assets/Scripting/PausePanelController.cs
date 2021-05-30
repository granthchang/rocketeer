using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] private GameObject blur;
    private Animator animator;
    
    void Start()
    {
        animator = this.GetComponent<Animator>();

        GameEvents.current.onPause += onPause;
        GameEvents.current.onResume += onResume;
        GameEvents.current.onGameOver += onGameOver;
    }

    void onPause() {
        blur.SetActive(true);
        animator.Play("In Left", -1, 0f);
    }

    void onResume() {
        blur.SetActive(false);
        animator.Play("Out Right", -1, 0f);
    }

    private void onGameOver() {
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);
    }
}
