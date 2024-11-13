using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Taca");
        this.transform.parent = collision.gameObject.transform; //Linea mágica para que las flechas se claven en el enemigo
        _rb.angularVelocity = 0;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;

        //Si golpeamos a un enemigo le quitamos vida y destruimos la flecha
       /* if (collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<IA_Enemigo>().Golpear();
            Destroy(this.gameObject);
        }*/
    }
}
