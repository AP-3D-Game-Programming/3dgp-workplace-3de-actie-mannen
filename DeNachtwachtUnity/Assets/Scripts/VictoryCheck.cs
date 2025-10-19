using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    public GameObject prize;
    public GameObject start;
    [SerializeField] bool hasPrize = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(start) && hasPrize)
        {
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
}
