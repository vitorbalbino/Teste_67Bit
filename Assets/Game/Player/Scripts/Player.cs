using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMove _playerMove;

    [SerializeField] ItemsPile _itemsPile;

    [SerializeField] PlayerSkin _playerSkin;

    [SerializeField] TMP_Text text_PlayerCoins, text_PileMax;
    [SerializeField] float _coins = 0;
    [SerializeField] int _PowerUpCount = 0;



    private void Awake()
    {
        SetSingleton(this);
    }



    private void Start()
    {
        UpdateTextInUI();
    }



    public PlayerMove GetPlayerMove()
    {
        return _playerMove;
    }



    public void CollisionWithNPC(CharNpc npc)
    {
        _playerMove.PunchNPC(npc);

        npc.OnKill();
    }



    public bool TryReceiveItem(GameObject itemNpc)
    {
        return _itemsPile.TryAddItemToPile(itemNpc);
    }


    public float GetCoinsCount()
    {
        return _coins;
    }



    public void UpdateTextInUI()
    {
        text_PlayerCoins.text = "Coins = " + GetCoinsCount().ToString("F2");
        text_PileMax.text = "Limite da pilha = " + _itemsPile.GetPileLimit();
    }



    public void AddvalueToCoins(float value)
    {
        _coins += value;
        UpdateTextInUI();
    }



    public bool TrySellItem(float payment, out GameObject npc)
    {
        if (_itemsPile.TakeItemFromPile(out npc))
        {
            AddvalueToCoins(payment);
        }
        
        return npc != null;
    }



    public void BuyPowerUp(float payment)
    {
        _PowerUpCount++;
        AddvalueToCoins(-payment);

        _itemsPile.PoweUpUpdate(_PowerUpCount);
        _playerSkin.UpdateMatColor(_PowerUpCount);

        UpdateTextInUI();
    }



    public int GetPowerUpCount()
    {
        return _PowerUpCount;
    }
}
