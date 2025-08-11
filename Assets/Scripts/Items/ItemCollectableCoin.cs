using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Coin Setup")]
    public float lerp = 5f;
    public float minDistance = 1f;
    private bool _collect = false;
    public Collider colliderCoin;

    private void Start()
    {
        CoinsAnimatorManager.Instance.RegisterCoin(this);
    }

    private void Update()
    {
        if(_collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);

            if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                // HideItems();

                Destroy(gameObject);
            }
        }
    }

    protected override void Collect()
    {
        OnCollect();
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        colliderCoin.enabled = false;
        _collect = true;
        ItemManager.Instance.AddCoins();
        Debug.Log("Coletou um coin!");

        // PlayerController.Instance.Bounce();
    }
}
