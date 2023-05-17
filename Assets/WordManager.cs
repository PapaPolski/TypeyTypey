using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    public bool amIActive;
    public string wordType;
    string wordToSet;

    public string currentActiveWord;
    public TMP_Text text;
    private string remainingWord = string.Empty;

    private int storySegmentInt;

    private void Awake()
    {
        amIActive = false;
    }

    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();  
        SetCurrentWord();
        storySegmentInt = 0;
    }

    private void SetCurrentWord()
    {

        if(wordType == "attack")
        {
            wordToSet = WordBank.Instance.attackWordsEasy[(Random.Range(0, WordBank.Instance.attackWordsEasy.Count))];
        }

        else if(wordType == "defend")
        {
            wordToSet = WordBank.Instance.defendWordsEasy[(Random.Range(0, WordBank.Instance.defendWordsEasy.Count))];
        }

        else if (wordType == "long")
        {
            wordToSet = WordBank.Instance.shortStorySegments[storySegmentInt];
        }
        currentActiveWord = wordToSet;
        SetRemainingWord(wordToSet);
    }

    void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        text.text = remainingWord;
    }


    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            if (amIActive)
            {
                this.GetComponent<Image>().color = Color.black;
                CheckInput();
            }
            else
            {
                this.GetComponent<Image>().color = Color.white;
            }
        }
    }

    void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if(keysPressed.Length == 1 ) 
            {
                EnterLetter(keysPressed);
            }
        }
    }

    void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            GameManager.Instance.SetCombo();

            if(typedLetter == " ")
            {
                GameManager.Instance.UpdateScore(10);
                GameManager.Instance.UpdateXP(10);
            }
            if(IsWordComplete())
            {
                if (wordType == "long")
                    storySegmentInt++;
                else if (wordType == "attack")
                {
                    Attack();
                }
                else if(wordType == "defend")
                {
                    Defend();
                }
                GameManager.Instance.UpdateScore(10);
                SetCurrentWord();
            }
        }
        else
        {
            GameManager.Instance.ResetCombo();
        }
    }

    void Attack()
    {
        PlayerShip.Instance.Fire();
    }
    void Defend()
    {
        PlayerShip.Instance.ChargeShield();
    }


    private bool IsCorrectLetter(string letter) 
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
