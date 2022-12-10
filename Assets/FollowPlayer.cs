using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    public Vector3 offset;
    void Update () {
        offset = new Vector3(-2f, 2f, 0);
        transform.position = player.transform.position + offset;
    }
}