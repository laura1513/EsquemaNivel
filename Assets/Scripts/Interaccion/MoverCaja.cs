using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoverCaja : MonoBehaviour
{
    [SerializeField] private ParticleSystem particulas;
    public bool activa; //la pongo p�blica para verla desde el inspector
    private bool _particulasOn;

    private void Start()
    {

        activa = true;
        _particulasOn = false;
        particulas.Stop();
    }
    public void SwitchParticulas()
    {
        if (activa)
        {

            if (_particulasOn) { particulas.Stop(); _particulasOn = false; } else { particulas.Play(); _particulasOn = true; }
        }

    }
    public void ActivarPalanca()
    {
        activa = true;
    }
    public void DesactivarPalanca()
    {
        activa = false;
    }
}
