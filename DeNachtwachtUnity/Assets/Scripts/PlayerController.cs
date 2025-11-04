using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] float crouchSpeed = 2.0f;
    [SerializeField] float sprintSpeed = 6.0f;
    [SerializeField] float currentSpeed;

    //Stamina
    [SerializeField] Image staminaBar;
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float staminaLoss = 20f;
    private Coroutine rechargeStamina;
    [SerializeField] float chargeRateStamina;
    [SerializeField] bool isExhausted = false;

    //Input
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    //Reference
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currentStamina = maxStamina;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsActive)
        {
            HandleMovement();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;

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
                
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                currentSpeed = crouchSpeed;
            }
            else
            {
                
                currentSpeed = walkSpeed;
            }

            if (currentStamina <= 0)
            {
                isExhausted = true;
                currentSpeed = 2f;
            }
        }
    }

    private void HandleMovement()
    {
        //Player Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(verticalInput, 0f, -horizontalInput).normalized;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15f);

            transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);
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
}
