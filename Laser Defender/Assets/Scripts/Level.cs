using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    [SerializeField] float delayForSeconds = 2f;

	public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetScore();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        /*Destroy(FindObjectOfType<GameSession>());*/
    }

    public void LoadGameOver()
    {
        StartCoroutine(ProcessGameOver());
    }

    IEnumerator ProcessGameOver()
    {
        yield return new WaitForSeconds(delayForSeconds);
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
