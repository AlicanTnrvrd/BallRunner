using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private new Camera camera;
    public void StartTheGame()
    {
        InGamePanelController.Instance.Open();
        PlayerController.Instance.StartToRun();
        
    }

    public void GameOver()
    {
        InGamePanelController.Instance.Close();
        GameOverPanelScript.Instance.Open();
    }

    public void LevelCompleted()
    {
        InGamePanelController.Instance.Close();
        LevelCompletedPanelScript.Instance.Open();
    }

    public Camera GetCamera()
    {
        return camera;
    }
}
