using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BallRotate : MonoBehaviour
{
    public bool rotate = false;
    
    /// <summary>
    /// Set by Controller depending on movement speed
    /// </summary>
    [FormerlySerializedAs("rotInAnglePerSecond")] public Quaternion angularVelocityInDegree;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (!rotate) return;
        transform.Rotate(angularVelocityInDegree.eulerAngles * Time.deltaTime, Space.Self);
    }
}
