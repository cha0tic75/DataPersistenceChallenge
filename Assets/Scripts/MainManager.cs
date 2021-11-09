using System;
using System.Collections.Generic;
using Project;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    #region Event/Delegate(s):
    #endregion

    #region Inspector Assigned Field(s):
    #endregion

    public TextMeshProUGUI ScoreText;
    public GameObject GameOverText;
    public GameObject SpaceBarText;
    
    private void Start() { }
    private void GetPlayerName()
    {
        // This should popup a UI to gather name;
    }

    private void WaitForSpacebarToRestart()
    {
        SpaceBarText.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        GameOverText.SetActive(true);
    }
}
