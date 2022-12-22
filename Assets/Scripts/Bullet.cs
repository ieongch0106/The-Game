using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // TODO: update for player
        // Debug.Log(other.name);
        if (other.name != "Gun")
        {
            Destroy(gameObject);
        }

    }
}
