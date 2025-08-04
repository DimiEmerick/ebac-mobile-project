using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpJump : PowerUpBase
{
    [Header("Power-Up Jump")]
    public float amountJump = 2f;
    public float animationDuration = 1f;
    public Ease ease = Ease.OutBounce;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountJump, animationDuration);
        PlayerController.Instance.SetPowerUpText("Jump!");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight(animationDuration, ease);
        PlayerController.Instance.SetPowerUpText("");
    }
}
