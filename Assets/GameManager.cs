using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum GameState
{
    Playing,
    Paused,
    InMenu
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    public GameState CurrentGameState { get; private set; }


    public List<WordManager> wordManagers = new List<WordManager>();
    WordManager currentWordManager;
    int counter = 0;

    int currentScore;

    public TMP_Text scoreText, comboNumber;

    float currentCombo;
    private float maxComboTime = 1f;
    private float comboTimer = 0f;
    public bool isComboRunning;

    int currentXP, XPToLevelUp, currentPlayerLevel;
    public Slider xpSlider;

    public GameObject levelUpMenu;


    private void Awake()
    {
        _instance = this;  
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.Playing);

        currentWordManager = wordManagers[0];
        wordManagers[0].amIActive = true;
        currentScore = 0;

        UpdateScore(0);

        currentCombo = 1;
        isComboRunning = false;
        XPToLevelUp = 100;
        SetUpNextXPLevel();
        currentPlayerLevel = 1;
        levelUpMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGameState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                counter = (counter + 1) % wordManagers.Count;
                currentWordManager = wordManagers[counter];

                //Debug.Log(currentWordManager);

                for (int i = 0; i < wordManagers.Count; i++)
                {
                    if (wordManagers[i] == currentWordManager)
                    {
                        wordManagers[i].amIActive = true;
                    }
                    else
                    {
                        wordManagers[i].amIActive = false;
                    }
                }
            }

            if (isComboRunning)
            {
                comboTimer += Time.deltaTime;
                if (comboTimer > maxComboTime)
                {
                    ResetCombo();
                }
            }
            else if (!isComboRunning)
            {
                ResetCombo();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (CurrentGameState)
            {
                case GameState.Playing:
                    SetGameState(GameState.Paused);
                    break;
                case GameState.Paused:
                    SetGameState(GameState.Playing);
                    break;
                case GameState.InMenu:
                    break;
                default:
                    break;
            }
        }    
    }

    public void SetGameState(GameState newGameState)
    {
        if(newGameState != CurrentGameState) 
        {
            CurrentGameState = newGameState;

        }
    }



    public void UpdateScore(int score)
    {
        currentScore += (int)(score * currentCombo);
        scoreText.text = currentScore.ToString("00000");
    }

    public void SetCombo()
    {
        if (!isComboRunning)
            isComboRunning = true;
        else
        {
            currentCombo += 0.1f;
            comboTimer = 0f;
        }

        DisplayCombo();
    }

    public void ResetCombo()
    {
        comboTimer = 0f;
        isComboRunning = false;
        currentCombo = 1;
        DisplayCombo();
    }

    void DisplayCombo()
    {
        comboNumber.text = currentCombo.ToString("0.0");
    }
    
    void SetUpNextXPLevel()
    {
        xpSlider.maxValue = XPToLevelUp;
        xpSlider.value = 0;
    }

    public void UpdateXP(int xpGained)
    {
        currentXP += xpGained;
        xpSlider.value = currentXP;

        if(currentXP >= XPToLevelUp)
        {
            LevelUp();
            XPToLevelUp = (int)(XPToLevelUp * 1.2);
            SetUpNextXPLevel();
            currentXP = 0;
        }
    }

    void LevelUp()
    {
        SetGameState(GameState.InMenu);
        Debug.Log("Levelled up!");
        currentPlayerLevel++;
        levelUpMenu.SetActive(true);
    }

    public float TakeDamage(float amount)
    {
        return amount;
    }
}
