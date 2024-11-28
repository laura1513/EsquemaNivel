using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtaqueMelee : MonoBehaviour
{
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    //[SerializeField] private GameObject particulas; El tiene particulas
    [SerializeField] private float fuerzaAtaque;
    //Sale en el animator
    private bool _atacando;
    // Input action
    /*private PLayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        puntoAtaque = GetComponent<Transform>();
        _atacando = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Atacar(InputAction.CallbackContext context)
    {
        if (context.starter && !_atacando)
        {
            _animator.SetBool("Melee", true);
            Atacando();
        }
    }
    public void Atacando()
    {
        _atacando = true;
        _animator.SetBool("atacar", true);
    }
    public void NoAtacando()
    {
        _atacando = true;
        _animator.SetBool("atacar", false);

       
    }
    public void EventoAnimAtaque()
    {
        //Necesarias para el circulo
        ContactFilter2D aux = new ContactFilter2D();
        List<ContactFilter2D> results = new List<ContactFilter2D>();

        //Casteo el circulo 
        Physics2D.CircleCast(puntoAtaque.position, radioAtaque, Vector2.zero,aux, results);

        //Comprobamos los TAGS
        foreach (RaycastHit2D hit in results)
        {
            if (hit.collider.gameObject.CompareTag("Eenmigo"))
            {
                //Accede al scrip del enemigo y le quita vida wow
                hit.collider.gameObject.GetComponent<IA_Enemigo>().Golpear;
            }
        }


    }*/
}
