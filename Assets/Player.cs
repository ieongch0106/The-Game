using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float velocity;
    public float gravity = 20.0f;

    // Start is called before the first frame update

    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(1.0f, 0.0f, 0.0f);
        velocity = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        movement_direction.y -= gravity * Time.deltaTime;
        character_controller.Move(movement_direction * velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A)) {
            transform.position += new Vector3(0, 0, 3f);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            transform.position += new Vector3(0, 0, -3f);
        }
    }
}
