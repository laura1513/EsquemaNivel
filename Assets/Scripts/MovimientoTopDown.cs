using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoTopDown : MonoBehaviour
{
    //[SerializeField] private float fuerzaSalto;
    [SerializeField] private float moveVel;
    //[SerializeField] private float maxVel;
    //[SerializeField] private Transform groundPos;
    //[SerializeField] private Vector2 tamCaja;
   
    [SerializeField] private float fuerzaFlecha;
    [SerializeField] private GameObject _flecha;
    [SerializeField] private Transform posFlecha;
    //[SerializeField] private LayerMask sueloLayer;
    
    private Vector2 pointerInput, movementInput;

    private PlayerInput _playerinput;
    private Rigidbody2D _rb;
    private Vector2 _pos;
    private Transform _transform;
    //private bool _gravedad;
    //private Vector2 _groundPos2D;
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
        //Debug.Log(_pos);

        _transform.position += new Vector3(_pos.x * moveVel * Time.deltaTime, _pos.y * moveVel * Time.deltaTime, 0);
        
        //_groundPos2D = groundPos.position;

        //_gravedad = Gravedad();

        //Para cambiar la animacion cuando se mueve
       // _animator.SetFloat("VelX", Mathf.Abs(_pos.x));
        //_animator.SetFloat("VelY", Mathf.Abs(_pos.y));
        //_animator.SetBool("Grounded", _gravedad);

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

    /*public void Saltar(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.started && _gravedad)
        {
            _rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            _animator.SetTrigger("Saltar");
        }
    }*/

    /*private void FixedUpdate()
    {
        if (_rb.velocity.magnitude <= maxVel)
        {
            //_rb.AddForce(new Vector2(_pos.x, 0) * moveVel);
        }
        _rb.velocity = new Vector2(_pos.x * moveVel, _rb.velocity.y);
    
    }*/

    /*public bool Gravedad()
    {
        if(Physics2D.BoxCast(_groundPos2D, tamCaja, 0f, Vector3.zero, 0f, sueloLayer))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void OnDrawGizmos() { 
        Gizmos.DrawWireCube(groundPos.position, tamCaja);
    }*/
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
