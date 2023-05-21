using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    ParticleSystem pS;
    // Start is called before the first frame update
    void Start()
    {
        pS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState == GameState.Playing && !pS.isPlaying)
            pS.Play(); 
        else
            pS.Pause();
    }
}
