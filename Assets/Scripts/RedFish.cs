using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFish : Fish
{
    [SerializeField]
    private float behaviorSpeed = 200;
    private float cursorDistanceLimit = 2;
    private float currentCursorDistance;

    protected override void CheckForSwimType()
    {
        currentCursorDistance = GetCurrentDistanceToMouseCursor();
        if (currentCursorDistance <= cursorDistanceLimit)
        {
            SetSwimType(SwimType.Behavior);
        }
        else
        {
            SetSwimType(SwimType.Normal);
        }
    }

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
