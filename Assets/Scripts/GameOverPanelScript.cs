using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelScript : Singleton<GameOverPanelScript>
{

    [SerializeField] private float delay;
    [SerializeField] private float openDuration = 0.5f;
    [SerializeField] private Button retry;
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
        contents.localScale = Vector3.zero;
        retry.enabled = false;
        contents.DOScale(Vector3.one,openDuration)
            .SetDelay(delay)
            .SetId("tween")
            .OnComplete(() =>
            {
                retry.enabled = true;

            });

    }
    public void Close()
    {
        retry.enabled = false;
        contents.DOScale(Vector3.zero, openDuration)
            .SetId("tween")
            .OnComplete(() =>
            {
                contents.gameObject.SetActive(false);
                SceneManager.LoadScene("GamePlayScene");
            });
    }

    
}

