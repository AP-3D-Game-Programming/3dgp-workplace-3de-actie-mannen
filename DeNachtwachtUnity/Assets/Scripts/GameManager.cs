using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Canvas menuScreen;
    [SerializeField] Canvas gameplayScreen;
    public bool gameIsActive;
    private bool gameStarted;
    private int currentLevel;

    LevelManager lvlManager;

    private string[] levelnames = { "Hub", "Level1", "Level2" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = 0;
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
        if (gameIsActive)
            menuScreen.gameObject.SetActive(false);
        else
            PauseScreen();
    }

    public void GameOver()
    {
        gameIsActive = false;
        GameOverScreen();
    }

    public async void LoadLevel(int toLoad)
    {
        menuScreen.gameObject.SetActive(false);
        gameIsActive = false;
        // need control for what level can be loaded
        await SceneManager.UnloadSceneAsync(levelnames[currentLevel]);
        currentLevel = toLoad;
        SceneManager.LoadScene(levelnames[toLoad], LoadSceneMode.Additive);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().prepareLevel();
        gameIsActive = true;
    }

    public void Victory()
    {
        Debug.Log("Victory");
        gameIsActive = false;
        menuScreen.gameObject.SetActive(true);
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

    private void StartScreen()
    {
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
            {
                button.onClick.AddListener(StartGame);
            } 
            else if (button.gameObject.name == "o2")
            {
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
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
            {
                button.onClick.AddListener(PauseToggle);
            }
            else if (button.gameObject.name == "o2")
            {
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
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
                button.onClick.AddListener(delegate { LoadLevel(currentLevel); });
            else if (button.gameObject.name == "o2")
            {
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
        foreach (var button in menuScreen.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "o1")
                button.onClick.AddListener(delegate { LoadLevel(0); });
            else if (button.gameObject.name == "o2")
            {
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
                    text.text = "To hub";
                    break;
                case "o2Text":
                    text.text = "Retry level";
                    break;
            }
        }
    }
}
