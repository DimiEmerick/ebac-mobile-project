using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableSatellite : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddSatellites();
        Debug.Log("Coletou um satélite!");
    }
}