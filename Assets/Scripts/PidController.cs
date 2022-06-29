using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidController : MonoBehaviour
{
    public float target;
    public float current;
    public float proportionalCoefficient = 1;
    public float integralCoefficient = 1;
    public float derivativeCoefficient = 1;

    public float output { get; private set; }

    private float errorDerivative;
    private float errorIntegral;
    private float previousError;

    private void FixedUpdate()
    {
        var error = target - current;
        
        errorIntegral += error * Time.fixedDeltaTime;
        errorDerivative = (error - previousError) / Time.fixedDeltaTime;

        output = proportionalCoefficient * error + integralCoefficient * errorIntegral + derivativeCoefficient * errorDerivative;

        previousError = error;
    }

    public override string ToString()
    {
        return $"Error: {previousError}, Integral: {errorIntegral}, Derivative: {errorDerivative}";
    }
}
