using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFish : ColorFish
{
    protected override void DoBehaviourSwimming()
    {
        Speed = behaviorSpeed;
        fishAnimator.speed = 2;
        FollowDirection = GetDirectionAwayFromMouseCursor();
    }

    private Vector3 GetDirectionAwayFromMouseCursor()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        return (transform.position - mousePos).normalized;
    }
}
