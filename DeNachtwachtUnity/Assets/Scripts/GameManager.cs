using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject start;

    [SerializeField] Canvas menuScreen;
    [SerializeField] Canvas gameplayScreen;
    public bool gameIsActive;
    private bool gameStarted;
    [SerializeField] int currentLevel;

    LevelManager lvlManager;

    private string[] levelnames = { "Assets/Scenes/Levels/Hub.unity", "Assets/Scenes/Levels/Level1.unity", "Assets/Scenes/Levels/Level2.unity" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = -1;
        gameIsActive = false;
        gameStarted = false;
        StartScreen();
        gameplayScreen.gameObject.SetActive(false);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        gameplayScreen.gameObject.SetActive(gameIsActive);
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseToggle();
        }
    }    
    
    public void StartGame()
    {
        menuScreen.gameObject.SetActive(false);
        gameplayScreen.gameObject.SetActive(true);
        gameIsActive = true;
        gameStarted = true;
        LoadLevel(0);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void PauseToggle()
    {
        gameIsActive = !gameIsActive;
        if (gameIsActive)
            menuScreen.gameObject.SetActive(false);
        else
            PauseScreen();
    }

    public void GameOver()
    {
        gameIsActive = false;
        gameplayScreen.gameObject.SetActive(false);
        player.GetComponent<VictoryCheck>().LevelReset();
        GameOverScreen();
    }

    public async void LoadLevel(int toLoad)
    {
        menuScreen.gameObject.SetActive(false);
        gameIsActive = false;
        // need control for what level can be loaded
        if (currentLevel != -1)
            await SceneManager.UnloadSceneAsync(levelnames[currentLevel]);
        currentLevel = toLoad;
        await SceneManager.LoadSceneAsync(levelnames[toLoad], LoadSceneMode.Additive);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().prepareLevel(player, cube, start);
        gameIsActive = true;
        gameplayScreen.gameObject.SetActive(true);
    }

    public void Victory()
    {
        gameIsActive = false;
        VictoryScreen();
    }

    public void Interactable()
    {
        foreach (var text in gameplayScreen.GetComponents<TextMeshProUGUI>()) {
            if (text.gameObject.name == "interact")
                text.gameObject.SetActive(true);
        }
    }
    public void Uninteractable()
    {
        foreach (var text in gameplayScreen.GetComponents<TextMeshProUGUI>())
        {
            if (text.gameObject.name == "interact")
                text.gameObject.SetActive(false);
        }
    }

    private void StartScreen()
    {
        menuScreen.gameObject.SetActive(true);
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1") { 
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(StartGame);
            } 
            else if (button.gameObject.name == "o2")
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }
        }
        foreach (var text in menuScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            switch (text.gameObject.name)
            {
                case "Title":
                    text.text = "De Nachtwacht";
                    break;
                case "o1Text":
                    text.text = "Start";
                    break;
            }
        }
    }
    private void PauseScreen()
    {
        menuScreen.gameObject.SetActive(true);
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(PauseToggle);
            }
            else if (button.gameObject.name == "o2")
            {
                button.onClick.RemoveAllListeners();
                if (currentLevel != 0)
                {
                    button.gameObject.SetActive(true);
                    button.onClick.AddListener(delegate { LoadLevel(0); });
                }
                else
                    button.gameObject.SetActive(false);
            }
        }
        foreach (var text in menuScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            switch (text.gameObject.name)
            {
                case "Title":
                    text.text = "Paused";
                    break;
                case "o1Text":
                    text.text = "Continue";
                    break;
                case "o2Text":
                    text.text = "Back to hub";
                    break;
            }
        }
    }

    private void GameOverScreen()
    {
        menuScreen.gameObject.SetActive(true);
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate { LoadLevel(currentLevel); });
            }
            else if (button.gameObject.name == "o2")
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(true);
                button.onClick.AddListener(delegate { LoadLevel(0); });
            }
        }
        foreach (var text in menuScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            switch (text.gameObject.name)
            {
                case "Title":
                    text.text = "You died!";
                    break;
                case "o1Text":
                    text.text = "Retry";
                    break;
                case "o2Text":
                    text.text = "Exit level";
                    break;
            }
        }
    }

    private void VictoryScreen()
    {
        menuScreen.gameObject.SetActive(true);
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate { LoadLevel(currentLevel + 1); });
            }
            else if (button.gameObject.name == "o2")
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(true);
                button.onClick.AddListener(delegate { LoadLevel(currentLevel); });
            }
        }
        foreach (var text in menuScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            switch (text.gameObject.name)
            {
                case "Title":
                    text.text = "Victory!";
                    break;
                case "o1Text":
                    text.text = "Next Level";
                    break;
                case "o2Text":
                    text.text = "Retry level";
                    break;
            }
        }
    }
}
