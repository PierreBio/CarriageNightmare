using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccueilMenu : MonoBehaviour
{
    public GameObject InfoUi;
   
    public void playGame()
    {
        SoundManager.GetInstance().Play("Wolf_howling", this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void enableInfo()
    {
        InfoUi.SetActive(true);
    }

    public void disableInfo()
    {
        InfoUi.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
