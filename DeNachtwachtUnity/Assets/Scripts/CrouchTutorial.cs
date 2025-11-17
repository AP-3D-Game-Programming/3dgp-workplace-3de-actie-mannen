using UnityEngine;

public class CrouchTutorial : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            gameManager.Interactable(2, "C", "sneak");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            gameManager.Uninteractable(3);
    }
}
