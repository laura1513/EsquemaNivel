using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

public class MoverCaja : MonoBehaviour
{
    //[SerializeField] private ParticleSystem particulas;
    
    public bool activa; //la pongo p�blica para verla desde el inspector
    //private bool _particulasOn;

    [SerializeField] Animator animCaja;

    private Animator _animator;
    public GameObject trigerPuerta;
    public GameObject puerta;
    public GameObject puertaAbierta;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        activa = true;

        trigerPuerta.SetActive(false);
        puerta.SetActive(true);
        puertaAbierta.SetActive(false);
        //_particulasOn = false;
        //particulas.Stop();
    }
    public void Caja()
    {
        if (activa)
        {
            _animator.SetTrigger("mover");
            animCaja.SetTrigger("mover");
            puerta.SetActive(false);
            trigerPuerta.SetActive(true);
            puertaAbierta.SetActive(true);
            //if (_particulasOn) { particulas.Stop(); _particulasOn = false; } else { particulas.Play(); _particulasOn = true; }
        }

    }
    public void ActivarCaja()
    {
        activa = true;
    }
    public void DesactivarCaja()
    {
        activa = false;
    }

}
