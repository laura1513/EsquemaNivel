using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Events;

public class IA_enemigo : MonoBehaviour
{
    public enum EnemyState { Patrullando, Persiguiendo, Atacando, Esperando, Muerto }
    private EnemyState currentState;

    [Header("Puntos de Patrulla")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Variables del Enemigo")]
    [SerializeField] private float distanciaVision = 10f;
    [SerializeField] private float distanciaAtaque = 2f;
    [SerializeField] private float tiempoEspera = 3f;
    [SerializeField] private UnityEvent eventoAtaque;
    [SerializeField] private Transform player;


    [Header("Variables Extra")]
    [SerializeField] private int puntosVida;


    private NavMeshAgent agent;
    private Transform currentPatrolPoint;
    private float esperaActual;
    private bool isMoving;


    //Animator
    private Animator _animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;   //Estas dos lineas son 
        agent.updateUpAxis = false;     //para que el enemigo no rote y deje de verse

        currentPatrolPoint = pointA;

        currentState = EnemyState.Patrullando;

       
        //animator
        _animator = GetComponent<Animator>();

        isMoving = false;
    }

    void Update()
    {
        //agent.velocity.magnitude es la variable buena, no agent.speed 
        if (agent.velocity.magnitude > 0.5f && !isMoving) { _animator.SetBool("Run", true); isMoving = true; }
        else if (agent.velocity.magnitude < 0.5f && isMoving) { _animator.SetBool("Run", false); isMoving = false; }

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
            case EnemyState.Muerto:
                //Esperamos la muerte
                break;
        }
    }

    private void Patrullar()
    {
        agent.SetDestination(currentPatrolPoint.position);

        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 1.5f)
        {
            currentPatrolPoint = currentPatrolPoint == pointA ? pointB : pointA; // Esto puede ponerse como un if
        }

        if (Vector3.Distance(transform.position, player.position) <= distanciaVision)
        {
            currentState = EnemyState.Persiguiendo;
        }
    }

    private void Perseguir()
    {
        agent.SetDestination(player.position); //mi objetivo es el player

        //Si estoy lo bastante cerca ataco.
        if (Vector3.Distance(transform.position, player.position) <= distanciaAtaque)
        {
            currentState = EnemyState.Atacando;
            agent.ResetPath();
        }
        //si el player se aleja lo suficiente vuelvo a patrullar.
        else if (Vector3.Distance(transform.position, player.position) > distanciaVision)
        {
            currentState = EnemyState.Patrullando;
            agent.SetDestination(currentPatrolPoint.position);
        }
    }

    private void Atacar()
    {
        //Ataco al player
      
        eventoAtaque?.Invoke(); //este evento esta pensado para ser personalizable: Quitar vida, modificar un marcador...
        currentState = EnemyState.Esperando; //Espero
     
        esperaActual = tiempoEspera; // Asigno los segundos que voy a esperar.
    }

    private void Esperar() //Ejemplo de contador 
    {
        //Decremento el tiempo en Time.deltatime (al final de cada segundo Time.deltaTime = 1, asi que decremento 1 por segundo)
        esperaActual -= Time.deltaTime;

        if (esperaActual <= 0) //Cuando el contador llega a cero vuelvo a patrullar.
        {
            currentState = EnemyState.Patrullando;
       
            agent.SetDestination(currentPatrolPoint.position);
        }
    }

    public void Golpear()
    {

        if (currentState != EnemyState.Esperando && currentState != EnemyState.Muerto)
        {

            puntosVida -= 1;

            if (puntosVida <= 0) // Si mi vida es > 0 muero.
            {
                Debug.Log("Enemigo muerto");
                GetComponent<BoxCollider2D>().enabled = false; //si muere ya no se le clavan flechas
                agent.ResetPath(); //se para
                currentState = EnemyState.Muerto; //cambiamos el estado
                _animator.SetTrigger("Death");
                Destroy(this.gameObject, 3);
            }
            else //Sino solo me quedo esperando y me quito vida.
            {
                Debug.Log("Enemigo herido");
                _animator.SetTrigger("Hit");
                agent.ResetPath(); //se para
                currentState = EnemyState.Esperando; //cambiamos el estado
                esperaActual = tiempoEspera;
            }
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
