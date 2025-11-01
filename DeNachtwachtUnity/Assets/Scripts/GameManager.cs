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
    [SerializeField] Canvas EndScreen;
    public bool gameIsActive;
    private bool gameStarted;
    private int currentLevel;

    LevelManager lvlManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = 0;
        gameIsActive = false;
        gameStarted = false;
        startScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        EndScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseToggle();
        }
    }    
    
    public void StartGame()
    {
        startScreen.gameObject.SetActive(false);
        gameIsActive = true;
        gameStarted = true;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void PauseToggle()
    {
        gameIsActive = !gameIsActive;
        pauseScreen.gameObject.SetActive(!pauseScreen.gameObject.activeSelf);
    }

    public void GameOver()
    {
        gameIsActive = false;
        EndScreen.gameObject.SetActive(true);
    }
    // can be replaced with LoadLevel(currentLevel)
    public void RestartLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevel + 1);
        SceneManager.LoadScene(currentLevel+1, LoadSceneMode.Additive);
        GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void LoadLevel(int toLoad)
    {
        // need control for what level can be loaded
        SceneManager.UnloadSceneAsync(currentLevel+1);
        currentLevel = toLoad + 1;
        SceneManager.LoadScene(toLoad+1, LoadSceneMode.Additive);
        GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void Victory()
    {
        Debug.Log("Victory");
        gameIsActive = false;
        EndScreen.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
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
