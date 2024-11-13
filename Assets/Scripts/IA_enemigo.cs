using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_enemigo : MonoBehaviour
{
    public enum EnemyState { Patrullando, Persiguiendo, Atacando, Esperando, Damaged }
    private EnemyState currentState;

    [Header("Puntos de Patrulla")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Variables del Enemigo")]
    [SerializeField] private float distanciaVision = 10f;
    [SerializeField] private float distanciaAtaque = 2f;
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private float tiempoEspera = 3f;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject cartelEspera;
    [SerializeField] private GameObject efectoAtaque;

    private Transform currentPatrolPoint;
    private float esperaActual;

     void Start()
    {
        currentPatrolPoint = pointA;
        currentState = EnemyState.Patrullando;
        cartelEspera.SetActive(false);
    }

    void Update()
{
    // Asegurar que el enemigo siempre mira hacia la cámara
    transform.rotation = Quaternion.Euler(0, 0, 0);

    // Continuar con la lógica de la IA
    switch (currentState)
    {
        case EnemyState.Patrullando:
            Patrullar();
            break;
        case EnemyState.Persiguiendo:
            Perseguir();
            break;
        case EnemyState.Atacando:
            Atacar();
            break;
        case EnemyState.Esperando:
            Esperar();
            break;
        case EnemyState.Damaged:
            Damaged();
            break;
    }
}
    private void Patrullar()
    {
        // Moverse hacia el punto actual
        MoverHacia(currentPatrolPoint.position);

        // Cambiar de punto si se ha llegado al actual
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.5f)
        {
            currentPatrolPoint = currentPatrolPoint == pointA ? pointB : pointA;
        }

        // Detectar al jugador
        if (Vector3.Distance(transform.position, player.position) <= distanciaVision)
        {
            currentState = EnemyState.Persiguiendo;
        }
    }

    private void Perseguir()
    {
        // Moverse hacia el jugador
        MoverHacia(player.position);

        // Atacar si está dentro del rango de ataque
        if (Vector3.Distance(transform.position, player.position) <= distanciaAtaque)
        {
            currentState = EnemyState.Atacando;
        }
        // Volver a patrullar si el jugador se aleja
        else if (Vector3.Distance(transform.position, player.position) > distanciaVision)
        {
            currentState = EnemyState.Patrullando;
        }
    }

    private void Atacar()
    {
        Instantiate(efectoAtaque, player.position, Quaternion.identity);
        currentState = EnemyState.Esperando;
        cartelEspera.SetActive(true);
        esperaActual = tiempoEspera;
    }

    private void Esperar()
    {
        esperaActual -= Time.deltaTime;

        if (esperaActual <= 0)
        {
            currentState = EnemyState.Patrullando;
            cartelEspera.SetActive(false);
        }
    }

    private void Damaged()
    {
        esperaActual -= Time.deltaTime;

        if (esperaActual <= 0)
        {
            currentState = EnemyState.Patrullando;
            cartelEspera.SetActive(false);
        }
    }

    private void MoverHacia(Vector3 objetivo)
{
    // Calcular la dirección hacia el objetivo en el plano 2D
    Vector3 direccion = (objetivo - transform.position).normalized;

    // Ignorar cualquier cambio en el eje Z (asegurar que Z permanezca igual)
    direccion.z = 0;

    // Mover al enemigo en el plano 2D
    transform.Translate(direccion * velocidadMovimiento * Time.deltaTime, Space.World);

    // Rotar hacia el objetivo en 2D (opcional si el enemigo necesita "mirar" al jugador)
    if (direccion != Vector3.zero)
    {
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
}


    // Visualización de las zonas de ataque y de visión en el editor con Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque); // Zona de ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaVision); // Zona de visión
    }
}
