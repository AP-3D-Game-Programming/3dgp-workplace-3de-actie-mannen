using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    [SerializeField] GameObject prize;
    [SerializeField] GameObject start;
    [SerializeField] bool hasPrize = false;
    private bool isNear = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameIsActive)
        {
            if (isNear && Input.GetKeyDown(KeyCode.E))
            {
                hasPrize = true;
                isNear = false;
                prize.SetActive(false);
                gameManager.Uninteractable();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(prize))
        {
            gameManager.Interactable(3, "E", "pick up");
            isNear = true;
        }
        if (other.gameObject.Equals(start) && hasPrize)
        {
            hasPrize = false;
            prize.SetActive(true);
            gameManager.Victory();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(prize))
        {
            gameManager.Uninteractable();
            isNear = false;
        }
    }

    public void LevelReset()
    {
        hasPrize = false;
        prize.SetActive(true);
    }
}
