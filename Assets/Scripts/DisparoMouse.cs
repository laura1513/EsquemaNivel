using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoMouse : MonoBehaviour

{

    [SerializeField] private float cooldownTime; //El tiempo que tarda en volver a disparar.
    [SerializeField] private float nextFireTime;
    public GameObject flechaPrefab;  // Prefab de la flecha a disparar
    public Transform firePoint;     // Punto desde el cual se dispararán las flechas
    public float flechaSpeed = 10f;  // Velocidad de la flecha

    private Camera mainCamera;
    private PlayerInput _playerInput;  // Referencia al PlayerInput para acceder a las acciones

    private void Awake()
    {
        mainCamera = Camera.main;
        _playerInput = GetComponent<PlayerInput>(); // Obtener referencia al componente PlayerInput
    }

    // Detecto el disparo
    public void Disparar(InputAction.CallbackContext context)
    {
        //Para que el cooldown funcione
        if (Time.time > nextFireTime)
        {

            if (context.performed)
            {

                ShootArrow();
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }

    private void ShootArrow()
    {
        // Leo la posición actual del mouse al momento de disparar
        Vector2 mousePosition = _playerInput.actions["MousePos"].ReadValue<Vector2>();
        Debug.Log(mousePosition);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0;
        Debug.Log(mouseWorldPosition);

        // Calcula el ángulo de rotación de la flecha
        Vector2 direccion = (mouseWorldPosition - firePoint.position).normalized;
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg; //Atan2 es la función que se suele usar para obtener el ángulo de rotacion entre dos vectores.

        // Instancia la flecha con la rotación hacia el objetivo
        GameObject flecha = Instantiate(flechaPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        // Aplica fuerza a la flecha
        Rigidbody2D rb = flecha.GetComponent<Rigidbody2D>();
        rb.AddForce(direccion * flechaSpeed, ForceMode2D.Impulse);
    }
}
