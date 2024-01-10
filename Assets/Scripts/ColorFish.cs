using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFish : Fish
{
    [SerializeField]
    protected float behaviorSpeed = 200;
    [SerializeField]
    protected float cursorDistanceLimit = 2;
    
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
}
