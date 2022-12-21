using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SIDE { left, mid, right };
public class Player : MonoBehaviour
{
    public SIDE m_side = SIDE.mid;
    private int player_health;
    float NewPos;
    float midPos;
    float finalPosX;
    float finalPosY;
    bool isJump;
    bool isDead;
    public float speedDodge;
    private Animator animation_controller;
    private CharacterController character_controller;
    public float velocity;
    internal float PosValue = 4.5f;
    public float gravity = 9.8f;
    public AudioSource footsteps, sliding, jumping, gettingHit;

    // Start is called before the first frame update
    void Start()
    {
        NewPos = -1.5f;
        midPos = -1.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        speedDodge = 2f;
        velocity = 6f;
        player_health = 5;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("MoveLeft") || animation_controller.GetCurrentAnimatorStateInfo(0).IsName("MoveRight")) {
            footsteps.enabled = false;
            sliding.enabled = true;
            jumping.enabled = false;
        } else {
           if (!isJump) {
            footsteps.enabled = true;
            sliding.enabled = false;
            jumping.enabled = false;
            } else {
                footsteps.enabled = false;
                sliding.enabled = false;
                jumping.enabled = true;
            }
        }

        if (player_health <= 0 && !isDead) {
            isDead = true;
            velocity = 0;
            animation_controller.SetTrigger("death");
        }

        if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("GettingHit")) {
            velocity = Mathf.Lerp(-1f, 0, Time.deltaTime);
            footsteps.enabled = false;
            sliding.enabled = false;
            jumping.enabled = false;
            gettingHit.enabled = true;
        } else if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Run")) {
            velocity = 6f;
            footsteps.enabled = true;
            sliding.enabled = false;
            jumping.enabled = false;
            gettingHit.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && m_side != SIDE.left) {
            if (!isJump) {
                animation_controller.SetTrigger("isMoveLeft");
            }
            if (m_side == SIDE.mid) {
                NewPos = -PosValue;
                m_side = SIDE.left;
            }
            else if (m_side == SIDE.right) {
                NewPos = midPos;
                m_side = SIDE.mid;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && m_side != SIDE.right) {
            if (!isJump) {
                animation_controller.SetTrigger("isMoveRight");
            }
            if (m_side == SIDE.mid) {
                NewPos = PosValue * 0.4f;
                m_side = SIDE.right;
            }
            else if (m_side == SIDE.left) {
                NewPos = midPos;
                m_side = SIDE.mid;
            }
        }

        finalPosX = Mathf.Lerp(finalPosX, NewPos, Time.deltaTime * speedDodge);
        float finalPosZ = velocity * Time.deltaTime;
        Vector3 movement = new Vector3(finalPosX - transform.position.x, finalPosY * Time.deltaTime, finalPosZ);
        character_controller.Move(movement);
        Jump();
    }

    public void Jump() {
        if (character_controller.isGrounded) {
            isJump = false;
            if (Input.GetKeyDown(KeyCode.Space)) {
                animation_controller.SetTrigger("isJumping");
            }

            if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver")) {
                isJump = true;
                finalPosY = 6f;
            }
        } else {
            finalPosY -= gravity * Time.deltaTime;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.transform.name);
        if (hit.transform.name == "Obstacle" || hit.transform.name == "Cart") {
            player_health -= 1;
            animation_controller.Play("GettingHit");
        }
    }
}
