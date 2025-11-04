using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] float openAngleInfront = 90f;
    [SerializeField] float openAngleBehind = -90f;
    [SerializeField] float openSpeed = 2f;
    [SerializeField] bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotationInfront;
    private Quaternion openRotationBehind;
    private Coroutine currentCoroutine;

    private GameManager gameManager;

    private bool isPlayerNearby = false;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.rotation;

        openRotationBehind = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngleBehind, 0));
        openRotationInfront = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngleInfront, 0));

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation;
        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.y = 0f;
        if (toPlayer.sqrMagnitude < 0.0001f) toPlayer = transform.forward;
        toPlayer.Normalize();

        if (isOpen)
        {
            targetRotation = closedRotation;
            isOpen = false;
        }
        else
        {
            Quaternion openRotationPos = closedRotation * Quaternion.Euler(0f, openAngleInfront, 0f);
            Quaternion openRotationNeg = closedRotation * Quaternion.Euler(0f, openAngleBehind, 0f);

            Vector3 forwardPos = (openRotationPos * -Vector3.forward);
            Vector3 forwardNeg = (openRotationNeg * -Vector3.forward);
            forwardPos.y = 0f; forwardNeg.y = 0f;
            forwardPos.Normalize(); forwardNeg.Normalize();

            float dotPos = Vector3.Dot(forwardPos, toPlayer);
            float dotNeg = Vector3.Dot(forwardNeg, toPlayer);

            if (dotPos < dotNeg)
            {
                targetRotation = openRotationPos;
            }
            else
            {
                targetRotation = openRotationNeg;
            }
            isOpen = true;
        }

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
                yield return null;
            }

        transform.rotation = targetRotation;
        currentCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPlayerNearby = true;
            gameManager.Interactable();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPlayerNearby = false;
            gameManager.Uninteractable();
        }
    }

}
