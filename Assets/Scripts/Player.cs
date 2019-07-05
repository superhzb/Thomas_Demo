using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private float currentSpeed = 0f;

    private CinemachineDollyCart cinemachineDollyCart;
    private ParticleSystem particle_superSpeedup;
    private float superSpeedupTimer = 0f;
    private bool isTransited = false;
    private FSMSystem fsm;

    public Carriage carriage;
    public BoxCollider playerCollider;
    public StateID currentState;

    private void Awake()
    {
        cinemachineDollyCart = GetComponentInParent<CinemachineDollyCart>();
        carriage = transform.Find("CarriageZone").GetComponent<Carriage>();
        particle_superSpeedup = transform.Find("Particle_Speedup").GetComponent<ParticleSystem>();
        playerCollider = GetComponent<BoxCollider>();

        //Register button events
        GameController.OnSpeedup += Speedup;
        GameController.OnSuperSpeedup += SuperSpeedup;
        currentState = StateID.Normal;
    }

    public void Start()
    {
        MakeFSM();
    }

    private void MakeFSM()
    {
        NormalState normalState = new NormalState(this);
        normalState.AddTransition(Transition.InSpecialMoveZone, StateID.SpecialMove);
        normalState.AddTransition(Transition.InPickUpZone, StateID.PickingUp);
        normalState.AddTransition(Transition.InDropOffZone, StateID.DroppingOff);

        SpecialMoveState specialMoveState = new SpecialMoveState(this);
        specialMoveState.AddTransition(Transition.ReturnToNormal, StateID.Normal);

        PickingUpState pickingUpState = new PickingUpState(this, playerSettings.PickUpTime, playerSettings.PickupParkingPos);
        pickingUpState.AddTransition(Transition.ReturnToNormal, StateID.Normal);

        DroppingOffState droppingOffState = new DroppingOffState(this, playerSettings.DropOffTime,playerSettings.DropoffParkingPos);
        droppingOffState.AddTransition(Transition.ReturnToNormal, StateID.Normal);

        fsm = new FSMSystem();
        fsm.AddState(normalState);
        fsm.AddState(specialMoveState);
        fsm.AddState(pickingUpState);
        fsm.AddState(droppingOffState);
    }

    public void SetTransition(Transition t)
    {
        isTransited = true;
        fsm.PerformTransition(t);
        currentState = fsm.CurrentState.ID;
        GameController.Instance.StateChange(fsm.CurrentState.ID);
    }

    //Transitions to all other States.
    private void OnTriggerEnter(Collider other)
    {
        ZoneType zoneType= other.gameObject.GetComponent<ZoneType>();
        if (zoneType != null && !isTransited)
        {
            SetTransition(zoneType.transition);
        }
    }

    private void Speedup()
    {
        currentSpeed += 2.0f;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, playerSettings.MaxMoveSpeed);
        SetCurrentSpeed(currentSpeed);
    }

    public void SuperSpeedup()
    {
        currentSpeed = playerSettings.MaxMoveSpeed;
        SetCurrentSpeed(currentSpeed);
        superSpeedupTimer = 0f;
        //FX
        particle_superSpeedup.Play();
    }

    public void SetCurrentSpeed(float speed)
    {
        cinemachineDollyCart.m_Speed = speed;
        currentSpeed = speed;
    }

    private void Update()
    {
        if (GameController.Instance.isPause || GameController.Instance.isGameover)
            return;

        fsm.CurrentState.Act();
    }

    public void NormalStateInit()
    {
        GameController.Instance.SetActiveSpeedupBtn(true);
        GameController.Instance.SetActiveSuperSpeedupBtn(false);
        GameController.Instance.NormalCameraOnLive();
        superSpeedupTimer = 0f;
        carriage.canDropOff = false;
        isTransited = false;
    }

    public void NormalStateOnUpdate()
    {
        DecelerateOnUpdate();

        //player can perform super speed up every 10s. 
        if (superSpeedupTimer >= playerSettings.SuperSpeedUpTime)
        {
            GameController.Instance.SetActiveSuperSpeedupBtn(true);
        }
        else
        {
            GameController.Instance.SetActiveSuperSpeedupBtn(false);
            superSpeedupTimer += Time.deltaTime;
        }
    }

    //For normal state.
    private void DecelerateOnUpdate()
    {
        currentSpeed -= playerSettings.Deceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, playerSettings.MaxMoveSpeed);
        SetCurrentSpeed(currentSpeed);
    }

    //For Pickup State and DropOff State
    public void StopAtPlatform(float pathPosition)
    {
        if (Mathf.Abs(cinemachineDollyCart.m_Position - pathPosition) > 0.1f)
        {
            cinemachineDollyCart.m_Position = Mathf.Lerp(cinemachineDollyCart.m_Position, pathPosition, Time.deltaTime * 2);
        }

        if (cinemachineDollyCart.m_Position > pathPosition)
        {
            cinemachineDollyCart.m_Position = pathPosition;
        }
    }

    //For pick up State ending
    public void HoldBallsInCarriage()
    {
        carriage.HoldBalls();
    }

    public void BeginDropOff()
    {
        carriage.canDropOff = true;
        carriage.ReleaseBalls();
    }

    public void EndDropOff()
    {
        carriage.canDropOff = false;
    }

    private void OnMouseDown()
    {
        carriage.DropABall();
    }
}
