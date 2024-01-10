using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : ColorFish
{
    protected override void DoBehaviourSwimming()
    {
        Speed = 10;
        fishAnimator.speed = 0;
        FollowDirection = Vector3.up;
    }
}
