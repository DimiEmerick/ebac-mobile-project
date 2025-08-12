using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float duration = .5f;
    public Color startColor = Color.HSVToRGB(0, 0, 78);
    private Color _correctColor;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_correctColor, duration).SetDelay(.25f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            LerpColor();
        }
    }
}
