using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float walkSpeed = 4.0f;
    private float crouchSpeed = 2.0f;
    private float sprintSpeed = 6.0f;
    [SerializeField] float currentSpeed;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float turnSpeed = 200;

    private Rigidbody playerRb;
    private GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //Player movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                currentSpeed = crouchSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }

            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);

            //Player turning following mouse
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * turnSpeed);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            playerRb.AddTorque(new Vector3(-1, 0, -1), ForceMode.Impulse);
        }
    }
}
