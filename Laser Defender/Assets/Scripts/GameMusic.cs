using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        if (FindObjectsOfType<GameMusic>().Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
