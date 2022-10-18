using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanelController : Singleton<InGamePanelController>
{
    [SerializeField] private Transform contents;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI bulletText;

    void Start()
    {
        UpdateCoinText();
        contents.gameObject.SetActive(false);
    }


    public void UpdateCoinText()
    {
        coinText.text = PlayerPrefs.GetInt("CoinAmount",0).ToString();
    }

    public void UpdateBulletText(int bulletAmount)
    {
        bulletText.text= bulletAmount.ToString();
    }

    public void Open()
    {
        contents.gameObject.SetActive(true);
    }

    public void Close()
    {
        contents.gameObject.SetActive(false);
    }
}
