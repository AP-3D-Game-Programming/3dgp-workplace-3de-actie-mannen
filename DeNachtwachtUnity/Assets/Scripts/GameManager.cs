using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] TextMeshProUGUI victoryText;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI interact;
    public bool gameIsActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interact.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsActive && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameIsActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Victory()
    {
        Debug.Log("Victory");
        gameIsActive = false;
        victoryText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        startText.gameObject.SetActive(false);
        gameIsActive = true;
    }

    public void Interactable()
    {
        interact.gameObject.SetActive(true);
    }
    public void Uninteractable()
    {
        interact.gameObject.SetActive(false);
    }
}
