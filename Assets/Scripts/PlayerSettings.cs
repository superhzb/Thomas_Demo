using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [SerializeField]
    [Range(0f, 20f)]
    private float moveSpeed = 5.0f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    [Range(10f, 50f)]
    private float maxMoveSpeed = 20f;
    public float MaxMoveSpeed { get { return maxMoveSpeed; } }

    [SerializeField]
    [Range(1f, 10f)]
    private float deceleration = 5f;
    public float Deceleration { get { return deceleration; } }

    [SerializeField]
    [Tooltip("in seconds")]
    private float pickUpTime = 20f;
    public float PickUpTime { get { return pickUpTime; } }

    [SerializeField]
    [Tooltip("in seconds")]
    private float dropOffTime = 20f;
    public float DropOffTime { get { return dropOffTime; } }

    [SerializeField]
    [Tooltip("in seconds")]
    private float superSpeedUpTime = 10f;
    public float SuperSpeedUpTime { get { return superSpeedUpTime; } }

    [SerializeField]
    [Tooltip("The path position of Dolly track.")]
    private float pickupParkingPos = 185;
    public float PickupParkingPos { get { return pickupParkingPos; } }

    [SerializeField]
    [Tooltip("The path position of Dolly track.")]
    private float dropoffParkingPos = 565;
    public float DropoffParkingPos { get { return dropoffParkingPos; } }


}
