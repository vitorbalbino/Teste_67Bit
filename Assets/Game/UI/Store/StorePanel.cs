using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorePanel : Singleton<StorePanel>
{
    float _precoPowerUp = 3.0f;

    public Button _buyButton;

    [SerializeField] GameObject _PanelBox;

    [SerializeField] TMP_Text text_PlayerCoins;
    [SerializeField] TMP_Text text_PowerUpPrice;
    [SerializeField] TMP_Text text_PowerUpPurchased;



    private void UpdateUI()
    {
        text_PlayerCoins.text = "Coins = " + Player._Sgt.GetCoinsCount().ToString("F2");
        text_PowerUpPrice.text = "Preço Power Up = " + _precoPowerUp.ToString("F2");
        text_PowerUpPurchased.text = "Power Ups acumulados = " + Player._Sgt.GetPowerUpCount();

        _buyButton.interactable = (Player._Sgt.GetCoinsCount() >= _precoPowerUp);
    }

    
    
    public void OnBuy()
    {
        Player._Sgt.BuyPowerUp(_precoPowerUp);
        UpdateUI();
    }



    public void OnOpenPanel()
    {
        GameManager._Sgt.Pause(true);
        _PanelBox.gameObject.SetActive(true);
        UpdateUI();
    }



    public void OnClosePanel()
    {
        GameManager._Sgt.Pause(false);
        _PanelBox.gameObject.SetActive(false);
    }



    private void Awake()
    {
        SetSingleton(this);
        _PanelBox.gameObject.SetActive(false);
    }
}
