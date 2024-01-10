using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    
    [SerializeField]
    private float baseSpeed = 100;
    private float _speed = 100;
    [SerializeField]
    private float _distanceLimit = 5;

    private bool limitImpulseAllowed = true;
    private Vector3 _centerPoint = Vector3.zero;
    private Vector3 _followDirection = Vector3.zero;

    private float timeForNewDirection = 0;

    protected Rigidbody fishRb;
    protected Animator fishAnimator;
    private GameObject GFXObject;

    private SwimType _swimType = SwimType.Normal;

    // ENCAPSULATION
    public float Speed
    {
        get{ return _speed; }
        set {
            if (value < 0) { _speed = 0; }
            else { _speed = value; }
        }
    }

    // ENCAPSULATION
    protected Vector3 FollowDirection
    {
        get { return _followDirection; }
        set { 
            _followDirection = value;
            if (value.magnitude == 0)
            { 
                //in case there is no direction, we make sure there is one
                _followDirection.x = 1;
            } 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fishRb = GetComponent<Rigidbody>();
        GFXObject = transform.Find("GFX").gameObject;
        fishAnimator = GFXObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSwimType();
        Swim();
    }

    protected virtual void CheckForSwimType()
    {
        // Override this function to make a different checkup to change the swim type
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _swimType = SwimType.Stop;
        }
    }

    // ABSTRACTION
    private void Swim()
    {
        if (FishIsInsideLimit())
        {
            limitImpulseAllowed = true;
            switch (_swimType)
            {
                case SwimType.Normal: DoBasicSwimming(); break;
                case SwimType.Stop: StopSwimming(); break;
                case SwimType.Behavior: DoBehaviourSwimming(); break;
            }
        }
        else
        {
            SetTowardsCenter();
        }
        SetFishFaceDirection();
        fishRb.AddForce(FollowDirection * _speed * Time.deltaTime, ForceMode.Force);
    }

    // ABSTRACTION
    private void SetFishFaceDirection()
    {
        if (fishRb.velocity.x >= 0)
        {
            GFXObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            GFXObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // ABSTRACTION
    private void DoBasicSwimming()
    {
        fishAnimator.speed = 1;
        Speed = baseSpeed;
        SetTowardsRandomDirection();   
    }

    // ABSTRACTION
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

    // ABSTRACTION
    private void SetTowardsCenter()
    {
        if (limitImpulseAllowed) 
        {
            limitImpulseAllowed = false;
            AddBreakForce(); 
        }
        FollowDirection = (_centerPoint - transform.position).normalized;
    }

    // ABSTRACTION
    private void SetTowardsRandomDirection()
    {
        if (NeedNewDirection())
        {
            //AddBreakForce();
            FollowDirection = SelectRandomDirection();

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

    // ABSTRACTION
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


    // ABSTRACTION
    private void StopSwimming()
    {
        Speed = 0;
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

    protected Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    protected float GetCurrentDistanceToMouseCursor()
    {
        Vector3 worldPos = GetMouseWorldPosition();
        Debug.Log($"Current Distance: {(worldPos - transform.position).magnitude}");
        return (worldPos - transform.position).magnitude;
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
