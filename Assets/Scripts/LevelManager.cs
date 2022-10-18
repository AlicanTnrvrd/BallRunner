using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levels;
  

    private void Awake()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        var currentLevel = PlayerPrefs.GetInt("Level", 0);
        var index = currentLevel % levels.Count;
        Instantiate (levels[index]);
    }
}
