using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextToSprite[] textToSprites;
    public string playerName;
    public string[] playerArr;
    public string firstName;

    [System.Serializable]
    public struct TextToSprite
    {
        public Sprite textSprite;
        public char text;
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdatePlayerName(string value)
    {
        playerName = value;
        playerArr = playerName.Split(' ');
        firstName = playerArr[0];
    }
}
