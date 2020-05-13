using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{
    public float RotationAngle = 0.1f;
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.up, RotationAngle);
        transform.LookAt(Player);
    }
}
