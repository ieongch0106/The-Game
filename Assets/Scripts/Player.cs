using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SIDE { left, mid, right };
public class Player : MonoBehaviour
{
    public SIDE m_side = SIDE.mid;
    private float max_health = 1;
    private float currentHealth;
    float NewPos;
    float midPos;
    float finalPosX;
    float finalPosY;
    bool isJump;
    bool isHit;
    bool isDead;
    public float speedDodge;
    private Animator animation_controller;
    private CharacterController character_controller;
    public float velocity;
    internal float PosValue = 4.5f;
    public float gravity = 9.8f;
    public AudioSource footsteps, sliding, jumping, gettingHit;
    [SerializeField] private HealthBar _healthBar;  

    // Start is called before the first frame update
    void Start()
    {
        NewPos = -1.5f;
        midPos = -1.5f;
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        speedDodge = 3f;
        velocity = 6f;
        isDead = false;
        isJump = false;
        isHit = false;
        currentHealth = max_health;
        _healthBar.UpdateHealthBar(max_health, currentHealth);
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
            }
        }

        if (currentHealth <= 0 && !isDead) {
            isDead = true;
            animation_controller.Play("Death");
        }

        if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("GotHit")) {
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
            isHit = false;
        } else if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver")) {
            velocity = 5.5f;
            finalPosY = 1.5f;
            if (animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.80) {
                isJump = false;
            }
        } else if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
            character_controller.enabled = false;
            footsteps.enabled = false;
            sliding.enabled = false;
            jumping.enabled = false;
            gettingHit.enabled = false;
            if (animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        } else if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            velocity = 0;
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
        
        if (!character_controller.isGrounded) {
            finalPosY -= gravity * Time.deltaTime; 
        }

        finalPosX = Mathf.Lerp(finalPosX, NewPos, Time.deltaTime * speedDodge);
        float finalPosZ = velocity * Time.deltaTime;
        Vector3 movement = new Vector3(finalPosX - transform.position.x, finalPosY * Time.deltaTime, finalPosZ);
        character_controller.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {   
        if (hit.transform.name != "Road-Left" && hit.transform.name != "Road-Mid" && hit.transform.name != "Road-Right" && hit.transform.name != "Land" && hit.transform.name != "SM_Env_TreeLog_01" && hit.transform.name != "Land Left" && hit.transform.name != "Land Right" && !isHit) {
            Debug.Log(hit.transform.name);
            hit.transform.GetComponent<Collider>().isTrigger = true;
            isHit = true;
            currentHealth -= 1;
            animation_controller.Play("GotHit");
            Destroy(hit.gameObject);
        }

        else if ((hit.transform.name == "Land" || hit.transform.name == "Land Left" || hit.transform.name == "Land Right" || hit.transform.name == "Water") && !isJump) {
            isJump = true;
            footsteps.enabled = false;
            sliding.enabled = false;
            gettingHit.enabled = false;
            // jumping.enabled = true;
            character_controller.enabled = false;
            animation_controller.Play("Idle");
        }
            
            // animation_controller.SetTrigger("isJumping");
    // {
    //     Debug.Log(hit.transform.name);
    //     if (hit.transform.name == "Obstacle" || hit.transform.name == "Cart" 
    //     || hit.transform.name == "SM_Bld_Wall_01" || hit.transform.name == "SM_Env_Rock_012" 
    //     || hit.transform.name == "SM_Bld_Stall_04" || hit.transform.name == "SM_Env_Rock_011" 
    //     || hit.transform.name == "SM_Env_Hedge_01" || hit.transform.name == "SM_Bld_Wall_01 (1)") {
    //         currentHealth = currentHealth-1;
    //         _healthBar.UpdateHealthBar(max_health, currentHealth);
    //         animation_controller.Play("GettingHit");
    //     }
    // }
    }

    
}
