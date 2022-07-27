using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameOverScreen gameOverScreen;
    private WinScreen winScreen;

    public TextToSprite[] textToSprites;
    public string playerName;
    public string[] playerArr;
    public string firstName;
    static bool hasSpawned = false;
    private bool playerDead = false;


    [System.Serializable]
    public struct TextToSprite
    {
        public Sprite textSprite;
        public char text;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        gameOverScreen = GameObject.FindWithTag("GameOverScreen").GetComponent<GameOverScreen>();
        winScreen = GameObject.FindWithTag("WinScreen").GetComponent<WinScreen>();
        if (!hasSpawned)
        {
            hasSpawned = true;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Health playerHealth = player.GetComponent<Health>();
                if (Mathf.Approximately(playerHealth.GetCurrentHitpoint(), 0f))
                {
                    gameOverScreen.Setup();
                }
                else
                {
                    gameOverScreen.HideGameObject();
                    winScreen.HideGameObject();
                }
            }
        }
        else
        {
            gameOverScreen.HideGameObject();
            winScreen.HideGameObject();
        }
    }

    public void UpdatePlayerName(string value)
    {
        playerName = value;
        playerArr = playerName.Split(' ');
        firstName = playerArr[0];
    }
}
