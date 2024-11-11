using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoTopDown : MonoBehaviour
{
    [SerializeField] private float moveVel;

    private PlayerInput _playerinput;
    private Vector2 _pos;
    private Transform _transform;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _left;
    
    void Start()
    {
        _playerinput = GetComponent<PlayerInput>();
        _transform = GetComponent<Transform>();
        _left = true;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _pos = _playerinput.actions["Mover"].ReadValue<Vector2>();

        //Para mover el personaje
        _transform.position += new Vector3(_pos.x * moveVel * Time.deltaTime, _pos.y * moveVel * Time.deltaTime, 0);

        //Para rotar el personaje
        if (_pos.x > 0 && !_left) {
            _sprite.flipX = false;
            _left = true;
        }
        if (_pos.x < 0 && _left) {
            _sprite.flipX = true;
            _left = false;
        }
    }
}
