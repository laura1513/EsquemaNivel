using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {

    [SerializeField] GameObject cartelInteractuar; //Se activa si estamos frente a un objeto interactuable
    private List<Interactable> interactablesCerca = new List<Interactable>(); // Lista de objetos interactuables en el rango del trigger
    public Interactable interactableMasCercano = null; // Lo pongo público para ver lo que detectamos en el inspector


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && interactableMasCercano != null) //Hay algún objeto interactable cerca?
        {
            interactableMasCercano.Interact(); // Interactuamos con el objeto más cercano
        }
    }
    //Detectamos un objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>(); //miramos si el objeto que ha entrado es Interactable
        if (interactable != null)
        {
            interactablesCerca.Add(interactable); // Añadimos el objeto interactuable a la lista
            DetectClosestInteractable();//Actualizamos el objeto más cercano
        }
    }
    //Un objeto sale de nuestro rango de detección
    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();//miramos si el objeto que ha salido es Interactable
        if (interactable != null)
        {
            interactablesCerca.Remove(interactable); // Eliminamos el objeto interactuable de la lista
            DetectClosestInteractable(); //Actualizamos el objeto más cercano
        }
    }
    private void Update()
    {
        if (interactableMasCercano != null) { cartelInteractuar.SetActive(true); } else { cartelInteractuar.SetActive(false); } // Si hay algun objeto cercano activamos el cartel
    }

    // Método para detectar el objeto interactuable más cercano
    private void DetectClosestInteractable()
    {
        float closestDistance = Mathf.Infinity;
        interactableMasCercano = null;

        foreach (Interactable interactable in interactablesCerca)
        {
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, interactable.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    interactableMasCercano = interactable;
                }
            }
        }
    }
}
