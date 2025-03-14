using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Android;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    float moveSpeed;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float jumpForce = 2;
    [SerializeField] float gravity = 10f;
    float rotationSpeed = 4.0f;
    Vector3 moveDirection;

    [Header("Stamina variables")]
    float minJumpStamina = 30;
    float minRunStamina = 10;
    public float stamina = 100f;
    public float staminaDrainSpeed = 40f;
    public Image staminaBar;
    bool isRunning = false;

    //public float health = 100f;
    //public Image healthBar;

    [Header("Components")]
    Animator anim;
    CharacterController controller;
    //AudioSource audioS;

    [Header("Booleans")]
    public bool gameOver = false;
    bool isCrouching = false;

    [Header("Audio clips")]
    public AudioClip walkSound;
    public AudioClip runSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        //audioS = GetComponent<AudioSource>();
        //playerCamera = Camera.main;

        Cursor.visible = true;

        moveSpeed = walkSpeed;

        //audioS.PlayOneShot(walkSound);
        //audioS.Pause();
    }

    void Update()
    {

        #region Movement
        if (controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0 && !isRunning)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                //audioS.Play();
            }
            else if (verticalInput == 0 && horizontalInput == 0)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                //audioS.Pause();
            }

            // preserve y velocity of the player, if player is grounded
            float movementDirectionY = moveDirection.y;

            moveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);

            // rotate player based on horizontal input
            transform.Rotate(0, horizontalInput * rotationSpeed, 0);
            //if(horizontalInput != 0) anim.SetBool("isTurning", true); 

            // set walking and running animation states while turning
            if (horizontalInput != 0 && !isRunning) anim.SetBool("isWalking", true);
            if (horizontalInput != 0 && isRunning) anim.SetBool("isRunning", true);

            if (Input.GetButtonDown("Jump") && stamina >= minJumpStamina)
            // hardcoding jumping rather than relying on rigidbodies 
            {
                moveDirection.y = jumpForce;
                stamina -= minJumpStamina;
                anim.SetTrigger("isJumping");
            }
            else
            {
                // sharper fall by setting moveDirection to the last preserved y velocity
                // faster than waiting for gravity to reduce moveDirection.y
                moveDirection.y = movementDirectionY;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // sprint mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina >= minRunStamina)
        {
            moveSpeed = runSpeed;
            isRunning = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
            //audioS.PlayOneShot(runSound);
            //audioS.Play();
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina >= minRunStamina)
        {
            // reduce stamina while key is pressed
            stamina -= Time.deltaTime * staminaDrainSpeed;
            StaminaBarUpdate();
        }
        else
        {
            // recover stamina over time if sprint button is not pressed
            if (stamina < 100)
            {
                stamina += Time.deltaTime * staminaDrainSpeed;
                StaminaBarUpdate();
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina < minRunStamina)
        {
            // change so setting a fixed speed
            moveSpeed = walkSpeed;
            anim.SetBool("isRunning", false);
            isRunning = false;
            //audioS.Pause();
        }

        // crouch or uncrouch when left control is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            anim.SetBool("isCrouching", isCrouching);
            if (isCrouching)
            {
                anim.SetTrigger("crouchDown");
                moveSpeed = crouchSpeed;
            }
            if (!isCrouching) moveSpeed = walkSpeed;
        }


        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        #endregion

    }

    void StaminaBarUpdate()
    {
        staminaBar.fillAmount = stamina / 100f;
    }

    // public void TakeDamage()
    // {
    //     health -= 10f;
    //     Debug.Log("health = " + health);
    //     healthBar.fillAmount = health / 100f;
    //     if(health <= 0)
    //     {
    //         gameOver = true;
    //         Debug.Log("game over");
    //     }
    // }

    // IEnumerator TurnAnimTrigger()
    // {
    //     isTurning = true;
    //     anim.SetBool("isTurning", true);
    //     yield return new WaitForSeconds(0.5f);
    //     isTurning = false;
    //     anim.SetBool("isTurning", false);
    // }

    public void FnUpdateStamina(float fltStaminaChange)
    {
        stamina += fltStaminaChange;
        if (stamina < 0)
        {
            stamina = 0;
        }

        if (stamina > 100)
        {
            stamina = 100;
        }

        StaminaBarUpdate();


    }
}
