using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BlueFish : ColorFish
{
    // POLYMORPHISM
    protected override void DoBehaviourSwimming()
    {
        Speed = 10;
        fishAnimator.speed = 0;
        FollowDirection = Vector3.up;
    }
}
