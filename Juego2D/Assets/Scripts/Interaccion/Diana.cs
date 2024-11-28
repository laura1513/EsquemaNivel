using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public GameObject diana;
    public GameObject puerta;
    public GameObject puertaAbierta;
    public GameObject flecha;
    // Start is called before the first frame update
    void Start()
    {
        puerta.SetActive(true);
        puertaAbierta.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Debug.Log("Diana");
            puerta.SetActive(false);
            puertaAbierta.SetActive(true);
        }
    }

}
