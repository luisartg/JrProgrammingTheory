using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    private Vector3 _followDirection;
    private float _distanceLimit;
    private Vector3 _centerPoint;

    private float timeForNewDirection = 0;

    private Rigidbody fishRb;

    private SwimType _swimType = SwimType.Normal;

    public float Speed
    {
        get{ return _speed; }
        set {
            if (value < 0) { _speed = 0; }
            else { _speed = value; }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fishRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Swim();
    }

    private void Swim()
    {
        switch (_swimType)
        {
            case SwimType.Normal: DoBasicSwimming(); break;
            case SwimType.Stop: StopSwimming(); break;
            case SwimType.Behavior: DoBehaviourSwimming(); break;
        }
    }

    private void DoBasicSwimming()
    {
        if (NeedNewDirection())
        {
            _followDirection = SelectRandomDirection();
            fishRb.AddForce(_followDirection * (-1) * _speed, ForceMode.Impulse);
        }

        fishRb.AddForce(_followDirection * _speed, ForceMode.Force);
    }

    private bool NeedNewDirection()
    {
        timeForNewDirection -= Time.deltaTime;
        if (timeForNewDirection <= 0)
        {
            timeForNewDirection = UnityEngine.Random.Range(1, 5);
            return true;
        }
        return false;
    }


    private Vector3 SelectRandomDirection()
    {
        int angle = UnityEngine.Random.Range(0, 360);
        float y = Mathf.Sin(angle);
        float x = Mathf.Cos(angle);

        return new Vector3(x, y, 0);
    }

    protected virtual void DoBehaviourSwimming()
    {
        
    }

    

    private void StopSwimming()
    {
    }

    public void SetSwimType(SwimType swimType = SwimType.Normal)
    {
        _swimType = swimType;
    }

    public enum SwimType
    {
        Stop,
        Normal,
        Behavior
    }
}
