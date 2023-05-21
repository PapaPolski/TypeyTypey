using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    private static WordBank _instance;

    public static WordBank Instance
    {
        get { return _instance; }
    }

    public List<string> attackWordsEasy = new List<string>();
    public List<string> attackWordsMedium = new List<string>();
    public List<string> attackWordsMax = new List<string>();

    public List<string> defendWordsEasy = new List<string>();
    public List<string> defendWordsMedium = new List<string>();
    public List<string> defendWordsMax = new List<string>();

    public List<string> shortStorySegments = new List<string>();

    public string[] individualLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    private void Awake()
    {
        _instance = this;
    }
}
