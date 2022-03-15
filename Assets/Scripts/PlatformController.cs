using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private SliderJoint2D _slider;
    private JointMotor2D _motor;
    [SerializeField, Range(0,10)] private float _speedLimit=1;

    void Start()
    {
        _slider = GetComponent<SliderJoint2D>();
        _slider.useMotor = true;
        _motor = _slider.motor;

    }

    private void FixedUpdate()
    {
        _slider.angle += _speedLimit;
        float _angleToRad = (_slider.angle * (Mathf.PI)) / 180;
        _motor.motorSpeed = Mathf.Cos(_angleToRad)/(1/_speedLimit);
        _slider.motor = _motor;
    }

}
