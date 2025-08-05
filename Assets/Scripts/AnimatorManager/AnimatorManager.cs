using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public List<AnimatorSetup> animatorSetups;
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEATH
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
}
