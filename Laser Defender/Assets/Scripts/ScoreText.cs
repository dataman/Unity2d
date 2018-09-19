using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    Text scoreText;
    GameSession session;

        // Use this for initialization
    void Start()
    {
        session = FindObjectOfType<GameSession>();
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = session.GetScore().ToString();
    }
}
