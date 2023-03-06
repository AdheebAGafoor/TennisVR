using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimation;

    public Animator animate;

    void Start()
    {
        
    }

    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        animate.SetFloat("Trigger", triggerValue);
        
        float gripValue = gripAnimation.action.ReadValue<float>();
        animate.SetFloat("Grip", gripValue);

    }
}
