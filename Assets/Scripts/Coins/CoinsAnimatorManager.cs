using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimatorManager : Singleton<CoinsAnimatorManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .25f;
    public float scaleTimeBetweenCoins = .1f;
    public Ease ease = Ease.OutBounce;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if(!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void ClearCoins()
    {
        itens.Clear();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScaleCoinsByTime());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }

    IEnumerator ScaleCoinsByTime()
    {
        foreach (var i in itens)
        {
            i.transform.localScale = Vector3.zero;
        }
        Sort();

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenCoins);
        }
    }
}
