using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedPanelScript : Singleton<LevelCompletedPanelScript>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float delay;
    [SerializeField] private float openDuration= 0.5f;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Transform contents;

    private void Start()
    {
        contents.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DOTween.Kill("tween");
    }
    public void Open()
    {
        contents.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        nextLevelButton.enabled = false;
        canvasGroup.DOFade(0.9f , openDuration)
            .SetId("tween")
            .SetDelay(delay)
            .OnComplete(() => 
            { 
             nextLevelButton.enabled=true;
            });

    }

    public void Close()
    {
        nextLevelButton.enabled = false;
        canvasGroup.DOFade(0f, openDuration)
            .SetId("tween")
            .OnComplete(() =>
            {
                contents.gameObject.SetActive(false);
            });
    }

    public void OnNextButtonClick()
    {
        Close();
        var currentLevel = PlayerPrefs.GetInt("Level", 0);
        currentLevel++;
        PlayerPrefs.SetInt("Level",currentLevel);
        SceneManager.LoadScene("GamePlayScene");
    }
}
