using UnityEngine;
public class NodeGenerator : MonoBehaviour
{
    [Header("Object References")]
        [SerializeField] private Transform participants;
        [SerializeField] private GameObject defaultNode;
        [SerializeField] private GameObject startingNode;
    [Header("Settings")]
        [SerializeField, Tooltip("Distance between Node levels")] private float interval;
        [SerializeField, Tooltip("Side margins for node position")] private float padding;

    // Other settings
    private float xMin, xMax;
    private float lastNodePos;
    private int[] lastSprites;

    // Called at scene initialization
    void Start() {
        xMin = -4.5f + padding;
        xMax = 4.5f - padding;

        lastSprites = new int[3];
        lastSprites[0] = 0;
        lastSprites[1] = 0;

        lastNodePos = startingNode.transform.position.y;
        int nodesToMake = (int)(12 / interval + 1);
        for (int i = 1; i <= nodesToMake; i++)
            createNode();
    }

    // Create new node just ahead of the screen when approaching
    void FixedUpdate() {
        if (lastNodePos - Camera.main.transform.position.y < 12)
            createNode();
    }

    // Create a new node ahead of the screen
    void createNode() {
        // Settings for the new node
        GameObject node = GameObject.Instantiate(defaultNode);
        node.transform.GetChild(0).GetComponent<Animator>().Play("Planet_bounce00", -1, UnityEngine.Random.value);
        node.name = "Node";
        node.transform.SetParent(participants);
        node.transform.position = new Vector3(getRandomX(), lastNodePos + interval);

        // Set sprite to a random sprite
        int rand = UnityEngine.Random.Range(0, 9);
        while (rand == lastSprites[0] || rand == lastSprites[1])
            rand = UnityEngine.Random.Range(0, 9);
        lastSprites[0] = lastSprites[1];
        lastSprites[1] = rand;
        node.GetComponent<Node>().setSprite(rand);

        defaultNode = node;
        lastNodePos += interval;
    }

    // Returns a random x coordinate between the xMin and xMax
    float getRandomX() {
        return UnityEngine.Random.Range(xMin, xMax);
    }
}
