using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotate : MonoBehaviour
{
    private Quaternion rot = Quaternion.Euler(Vector3.right*30f);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(rot.eulerAngles * Time.deltaTime, Space.Self);
    }
    //TODO: Rotate around y so it faces forward
    //TODO: Calculate Correct rotation
}
