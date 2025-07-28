using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt satellites;
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextSatellites;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        satellites.value = 0;
        UpdateUI();
    }

    public void AddCoins(int amountC = 1)
    {
        coins.value += amountC;
        UpdateUI();
    }
    public void AddSatellites(int amountS = 1)
    {
        satellites.value += amountS;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIInGameManager.UpdateTextCoins(coins.ToString());
        UIInGameManager.UpdateTextSatellites(satellites.ToString());
    }
}
