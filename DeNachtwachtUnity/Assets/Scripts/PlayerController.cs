using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Speeds
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float crouchMultiplier = 0.5f;
    [SerializeField] float sprintMultiplier = 2.0f;
    [SerializeField] float currentSpeed;
    private float crouchSpeed
    {
        get
        {
            return walkSpeed * crouchMultiplier;
        }
    }
    private float sprintSpeed
    {
        get
        {
            return walkSpeed * sprintMultiplier;
        }
    }

    //Stamina
    [SerializeField] Image staminaBar;
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float staminaLoss = 20f;
    private Coroutine rechargeStamina;
    [SerializeField] float chargeRateStamina;
    [SerializeField] bool isExhausted = false;

    //Crouch options
    [SerializeField] GameObject standObject;
    [SerializeField] GameObject crouchObject;

    //Input
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    //Reference
    private GameManager gameManager;
    private Rigidbody playerRb;
    private BoxCollider playerCollider;
    private Vector3 standSize;
    private Vector3 crouchSize;
    public bool isCrouching = false;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
        standSize = standObject.GetComponent<BoxCollider>().size;
        crouchSize = crouchObject.GetComponent<BoxCollider>().size;
        standObject.SetActive(false);
        crouchObject.SetActive(false);

        currentStamina = maxStamina;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.gameIsActive)
        {
            HandleMovement();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
                playerCollider.size = standSize;

                currentStamina -= staminaLoss * Time.deltaTime;
                if (currentStamina < 0)
                {
                    currentStamina = 0;
                }
                staminaBar.fillAmount = currentStamina / maxStamina;

                if (rechargeStamina != null)
                {
                    StopCoroutine(rechargeStamina);
                }
                rechargeStamina = StartCoroutine(RechargeStamina());
                isCrouching = false;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                currentSpeed = crouchSpeed;
                playerCollider.size = crouchSize;
                isCrouching = true;
            }
            else
            {
                currentSpeed = walkSpeed;
                playerCollider.size = standSize;
                isCrouching = false;
            }

            if (currentStamina <= 0)
            {
                isExhausted = true;
                currentSpeed = crouchSpeed;
            }
        }
    }

    private void HandleMovement()
    {
        //Player Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(verticalInput, 0f, -horizontalInput).normalized;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15f);

            playerRb.linearVelocity = (move * currentSpeed * Time.deltaTime);
        }
        else
        {
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (currentStamina < maxStamina)
        {
            currentStamina += chargeRateStamina / 10f;
            if (isExhausted && currentStamina >= maxStamina / 2f)
            {
                isExhausted = false;
            }

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            staminaBar.fillAmount = currentStamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
    }

    public void ResetA()
    {
        currentStamina = maxStamina;
        playerCollider.size = standSize;
        isExhausted = false;
        isCrouching = false;
    }
}
