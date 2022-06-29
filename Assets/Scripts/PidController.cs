using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidController : MonoBehaviour
{
    public Vector2 target;
    public Vector2 current;
    public float proportionalCoefficient = 1;
    public float integralCoefficient = 1;
    public float derivativeCoefficient = 1;

    public Vector2 output { get; private set; }

    private Vector2 errorDerivative;
    private Vector2 errorIntegral;
    private Vector2 previousError;

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
