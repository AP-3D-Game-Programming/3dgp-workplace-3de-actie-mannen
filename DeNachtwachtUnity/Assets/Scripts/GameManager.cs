using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas pauseScreen;
    public bool gameIsActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        } 
    }

    public void PauseToggle()
    {
        gameIsActive = false;

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameIsActive = false;
        //gameOverText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Victory()
    {
        Debug.Log("Victory");
        gameIsActive = false;
        //victoryText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startScreen.gameObject.SetActive(false);
        gameIsActive = true;
    }

    public void Interactable()
    {
        //interact.gameObject.SetActive(true);
    }
    public void Uninteractable()
    {
        //interact.gameObject.SetActive(false);
    }
}
