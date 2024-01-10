using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFish : ColorFish
{
    protected override void DoBehaviourSwimming()
    {
        Speed = behaviorSpeed;
        fishAnimator.speed = 2;
        FollowDirection = GetDirectionTowardsMouseCursor();
    }

    private Vector3 GetDirectionTowardsMouseCursor()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        return (mousePos - transform.position).normalized;
    }
}