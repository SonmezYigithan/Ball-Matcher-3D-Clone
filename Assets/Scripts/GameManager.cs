using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> levelPrefabs = new List<GameObject>(); 

    [HideInInspector] public int score;
    [HideInInspector] public int currentLevel = 0;

    public static GameManager instance;

    private GamePanelHandle gamePanelHandler;

    public GameObject levelObjContainer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        gamePanelHandler = GameObject.FindGameObjectWithTag("PanelHandler").GetComponent<GamePanelHandle>();
        LoadNextLevel();
        gamePanelHandler.UpdateScore();
    }

    public void ScoreBall()
    {
        score += 10;
        PlayerPrefs.SetInt("Score", score);
        gamePanelHandler.UpdateScore();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gamePanelHandler.GameOverPanel();
        //Time.timeScale = 0;
    }

    public void LevelCleared()
    {
        gamePanelHandler.WinPanel(true);
    }

    public void LoadNextLevel()
    {
        if (currentLevel < levelPrefabs.Count)
        {
            if (levelObjContainer.transform.childCount > 0)
                Destroy(levelObjContainer.transform.GetChild(0).gameObject);

            PlayerPrefs.SetInt("Level", currentLevel);
            Debug.Log("loading level " + currentLevel);
            Instantiate(levelPrefabs[currentLevel], levelObjContainer.transform);
            gamePanelHandler.UpdateLevelPanel((currentLevel + 1).ToString());
        }
        else
        {
            // end game panel
            gamePanelHandler.TheEndPanel();
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }



}
