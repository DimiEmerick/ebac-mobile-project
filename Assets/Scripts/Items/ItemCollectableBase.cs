using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide = 3f;
    public ParticleSystem itemParticleSystem;
    public GameObject graphicItem;
    public Collider2D colliderItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        colliderItem.enabled = false;
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (itemParticleSystem != null) itemParticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}