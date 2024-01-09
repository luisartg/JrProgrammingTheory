using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _distanceLimit = 5;

    private bool limitImpulseAllowed = true;
    private Vector3 _followDirection = Vector3.zero;
    private Vector3 _centerPoint = Vector3.zero;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _swimType = SwimType.Stop;
        }
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
        if (FishIsInsideLimit())
        {
            limitImpulseAllowed = true;
            SetTowardsRandomDirection();
        }
        else
        {
            SetTowardsCenter();
        }
        fishRb.AddForce(_followDirection * _speed * Time.deltaTime, ForceMode.Force);
    }

    private bool FishIsInsideLimit()
    {
        if ((_centerPoint - transform.position).magnitude <= _distanceLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SetTowardsCenter()
    {
        if (limitImpulseAllowed) 
        {
            limitImpulseAllowed = false;
            AddBreakForce(); 
        }
        _followDirection = (_centerPoint - transform.position).normalized;
    }

    private void SetTowardsRandomDirection()
    {
        if (NeedNewDirection())
        {
            //AddBreakForce();
            _followDirection = SelectRandomDirection();

        }
    }

    private void AddBreakForce()
    {
        // This is to apply a little break to the force without making a full stop
        //fishRb.AddForce(_followDirection * (-1) * _speed * Time.deltaTime, ForceMode.Force);
        fishRb.velocity = Vector3.zero;
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
        DoBasicSwimming();
    }

    

    private void StopSwimming()
    {
        if (FishIsInsideLimit())
        {
            if (fishRb.velocity.magnitude > 0)
            {
                fishRb.velocity -= fishRb.velocity * 0.1f * Time.deltaTime;
            }
        }
        else
        {
            AddBreakForce();
        }
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
