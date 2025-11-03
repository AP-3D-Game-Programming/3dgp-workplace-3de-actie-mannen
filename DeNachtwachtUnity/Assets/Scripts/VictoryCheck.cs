using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    [SerializeField] GameObject prize;
    [SerializeField] GameObject start;
    [SerializeField] bool hasPrize = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(prize))
        {
            gameManager.Interactable();
        }
        if (other.gameObject.Equals(start) && hasPrize)
        {
            hasPrize = false;
            prize.SetActive(true);
            gameManager.Victory();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(prize) && Input.GetKeyDown(KeyCode.E))
        {
            hasPrize = true;
            prize.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(prize))
        {
            gameManager.Uninteractable();
        }
    }
}
