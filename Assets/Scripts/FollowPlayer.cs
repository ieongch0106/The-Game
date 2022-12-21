using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    public Vector3 offset;

    void Start() {
        offset = new Vector3(0f, 2f, -5f);
    }
    void Update () {
        transform.position = player.transform.position + offset;
    }
}