using UnityEngine;
using System.Collections;
using _Game.Controllers;

public class CFX_AutoRotate : MonoBehaviour
{
    // Rotation speed & axis
    public Vector3 rotation;

    // Rotation space
    public Space space = Space.Self;

    // Rotation speed when the engine is on
    public float fastRotationSpeed = 180.0f; // Adjust as needed

    // Rotation speed when the engine is off
    public float slowRotationSpeed = 45.0f; // Adjust as needed

    private bool engineRunning = false;
    

    void Update()
    {
        // Rotate based on the engine status
        float currentRotationSpeed = engineRunning ? fastRotationSpeed : slowRotationSpeed;
        this.transform.Rotate(rotation * currentRotationSpeed * Time.deltaTime, space);
    }
    
}
