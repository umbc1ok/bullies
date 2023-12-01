using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class InputSetup : MonoBehaviour
{
    // TODO: this parameter should be in more appropiate place, a more general player script
    [SerializeField] private int playerID = 1;
    private string leftAnalogHorizontal;
    private string leftAnalogVertical;
    private string rightAnalogHorizontal;
    private string rightAnalogVertical;
    private string rightTrigger;
    private string a;

    public event Action<Vector2> OnLeftAnalogInput;
    public event Action<Vector2> OnRightAnalogInput;
    public event Action<float, int> OnRightTriggerInput;
    public event Action<int> OnRightTriggerPressed;
    public event Action<int> OnRightTriggerReleased;
    public event Action OnAButtonPressed;

    private float deadZone = 0.15f;
    private float triggerPressBias = 0.35f;
    private bool rightTriggerPressFlag;
    
    public int GetPlayerID()
    {
        return playerID;
    }

    private void SetControls()
    {
        leftAnalogHorizontal = "J" + playerID + "_LeftStickX";
        leftAnalogVertical = "J" + playerID + "_LeftStickY";
        rightAnalogHorizontal = "J" + playerID + "_RightStickX";
        rightAnalogVertical = "J" + playerID + "_RightStickY";
        rightTrigger = "J" + playerID + "_RightTrigger";
        a = "J" + playerID + "_A";
    }

    // Set controls based on player ID
    void Start()
    {
        SetControls();
    }

    // Checkers for any input (events will be sent based on this) //

    void LeftAnalog()
    {
        float x = Input.GetAxisRaw(leftAnalogHorizontal);
        float y = Input.GetAxisRaw(leftAnalogVertical);
        Vector2 v = new Vector2(x, y);
        if (v.magnitude <= deadZone)
            v = Vector2.zero;

        OnLeftAnalogInput?.Invoke(v);
    }

    void RightAnalog()
    {
        float x = Input.GetAxis(rightAnalogHorizontal);
        float y = Input.GetAxis(rightAnalogVertical);
        Vector2 v = new Vector2(x, y);
        if (v.magnitude < deadZone)
            v = Vector2.zero;

        OnRightAnalogInput?.Invoke(v);
    }

    void RightTrigger()
    {
        float f = Input.GetAxis(rightTrigger);
        OnRightTriggerInput?.Invoke(f, playerID);
    }

    void RightTriggerPressed()
    {
        if (Input.GetAxis(rightTrigger) >= triggerPressBias && !rightTriggerPressFlag)
        {
            OnRightTriggerPressed?.Invoke(playerID);
            rightTriggerPressFlag = true;
        }
    }
    
    void RightTriggerReleased()
    {
        if (Input.GetAxis(rightTrigger) < triggerPressBias && rightTriggerPressFlag)
        {
            OnRightTriggerReleased?.Invoke(playerID);
            rightTriggerPressFlag = false;
        }
    }

    void A()
    {
        if(Input.GetButton(a))
            OnAButtonPressed?.Invoke();
    }

    // Update and check for any inputs. Only one loop per player, the rest is handled on events
    void Update()
    {
        LeftAnalog();
        RightAnalog();
        RightTrigger();
        RightTriggerPressed();
        RightTriggerReleased();
        A();
    }
}