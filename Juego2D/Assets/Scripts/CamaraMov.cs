using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamaraMov : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private CamaraMov dirContraria;
    [SerializeField] private bool activo;
    [SerializeField] private UnityEvent evento;
    private bool _isMoving = false;
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (_isMoving)
        {
            //Ole
            //Mover la camara
            cam.GetComponent<Transform>().position = Vector3.Lerp(cam.GetComponent<Transform>().position, target.position, speed * Time.deltaTime);

            //Al llegar se oara
            if (Vector3.Distance(cam.GetComponent<Transform>().position, target.position) < 0.1f)
            {
                _isMoving = false;
            }
        }
    }
    public void StartMoving()
    {
        dirContraria.Moving(false);
        _isMoving = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && activo)
        {
            StartMoving();
            Desactivar();
            dirContraria.Activar();
            evento.Invoke();
        }
    }
    public void Activar()
    {
        activo = true;
    }
    public void Desactivar()
    {
        activo = false;
    }
    public void Moving(bool moving)
    {
        _isMoving = moving;
    }

}