using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    private Transform aimBot;
    public GameObject gun;
    public GameObject bullet;
    public float bulletDelay = 1f;

    void Awake()
    {
        target = GameObject.Find("PLAYER");
        aimBot = transform.Find("Bot");
        Invoke("Shoot", bulletDelay);

        Debug.Log(gun.name);
    }

    // Update is called once per frame
    void Update()
    {
        aimBot.LookAt(target.transform.position, Vector3.up);

    }

    void Shoot()
    {
        Instantiate(bullet, gun.transform.position, aimBot.transform.rotation);
        if (target.transform.position.z < transform.position.z)
        {
            Invoke("Shoot", bulletDelay);
        }
    }
}
