using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Singleton<MainMenu>
{
    [SerializeField] private Transform contents;
   public void OnTapButtonClick()
    {
        GameManager.Instance.StartTheGame();
        contents.gameObject.SetActive(false);
    }
    
}
