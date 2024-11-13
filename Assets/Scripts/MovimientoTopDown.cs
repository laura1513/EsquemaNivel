using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoTopDown : MonoBehaviour
{
    [SerializeField] private float moveVel;
    [SerializeField] private float fuerzaFlecha;
    [SerializeField] private GameObject _flecha;
    [SerializeField] private Transform posFlecha;
    private Vector2 pointerInput, movementInput;

    private PlayerInput _playerinput;
    private Rigidbody2D _rb;
    private Vector2 _pos;
    private Transform _transform;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _left;
    private bool _atacando;
    private bool _disparando;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerinput = GetComponent<PlayerInput>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _left = true;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        
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
    public void Ataque(InputAction.CallbackContext context)
    {
        
        if (context.started && !_atacando)
        {
            _rb.velocity = Vector2.zero;
            _animator.SetTrigger("Ataque");
        }
    }
    
    public void Atacando() {
        _atacando = true;
    }
    public void NoAtacando() {
        _atacando = false;
    }
    public void Flecha(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {
            Vector2 dirFlecha =  (pointerInput - (Vector2)transform.position).normalized;
            GameObject flecha = Instantiate(_flecha, posFlecha.position, posFlecha.rotation);
            flecha.GetComponent<Rigidbody2D>().AddForce(dirFlecha * fuerzaFlecha, ForceMode2D.Impulse);
            Destroy(flecha, 3f);
        }

    }

    public void Disparando()
    {
        
        
        _disparando = true;
    }
    public void NoDisparando()
    {
        _disparando = false;
    }
    // Video camara aim https://youtu.be/DPqc7qYDtzM?si=ZONzmozuQsopG8gZ

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = _playerinput.actions["PointerPosition"].ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        Debug.Log(mousePos);
        return Camera.main.ScreenToWorldPoint(mousePos);
        
    }
}
