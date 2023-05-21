using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{

    private static LevelUpManager _instance;
    public static LevelUpManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("Level Up menu opened!");
    }

    private void OnDisable()
    {
        Debug.Log("Level Up menu closed!");
        GameManager.Instance.SetGameState(GameState.Playing);
    }

    public void CloseLevelUpMenu()
    {
        this.gameObject.SetActive(false);
    }    
}
