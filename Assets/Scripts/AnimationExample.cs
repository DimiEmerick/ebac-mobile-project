using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation animationPlayer;
    public AnimationClip run;
    public AnimationClip idle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(run);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            PlayAnimation(idle);
        }
    }

    private void PlayAnimation(AnimationClip c)
    {
        animationPlayer.CrossFade(c.name);
    }
}
