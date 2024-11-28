using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PosFlecha : MonoBehaviour
{
    public GameObject Meele;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimeer = 0f;
    private PlayerInput _playerInput;
    private void Awake() 
    { 
        _playerInput = GetComponent<PlayerInput>();
    }
    
    private void Update()
    {
    
    }
}
