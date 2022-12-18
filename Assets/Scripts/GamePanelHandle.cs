using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelHandle : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject theEndPanel;
    [SerializeField] TMP_Text gameSpeed;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text levelText;

    public void UpdateScore()
    {
        score.text = "SCORE : " + GameManager.instance.score.ToString();
    }

    #region Panels

    public void WinPanel(bool activate)
    {
        if (activate)
            winPanel.SetActive(true);
        else
            winPanel.SetActive(false);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdateLevelPanel(string level)
    {
        levelText.text = "LEVEL " + level;
    }

    public void TheEndPanel()
    {
        theEndPanel.SetActive(true);
    }

    public void UpdateGameSpeedPanel(float speed)
    {
        gameSpeed.text = "x" + speed.ToString();
    }

    public void NextLevelButton()
    {
        GameManager.instance.IncreaseLevel();
        GameManager.instance.LoadNextLevel();
        WinPanel(false);
    }

    public void PlayAgainButton()
    {
        PlayerPrefs.SetInt("Level", 0);
        ReloadSceneButton();
    }

    public void ReloadSceneButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    #endregion

}
