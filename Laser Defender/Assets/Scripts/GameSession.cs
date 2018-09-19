using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int score = 0;
    GameSession session;
        
    void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddToScore(int points)
    {
        score += points;
    }

}
