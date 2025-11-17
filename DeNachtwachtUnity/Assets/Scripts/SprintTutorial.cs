using UnityEngine;

public class SprintTutorial : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            gameManager.Interactable(0, "LSHIFT", "sprint");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            gameManager.Uninteractable(1);
    }
}
