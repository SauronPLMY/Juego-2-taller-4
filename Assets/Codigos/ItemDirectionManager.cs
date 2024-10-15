using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ItemDirectionManager : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public GameObject directionalArrow; // La flecha direccional (imagen)
    public List<Transform> items; // Lista de �tems en la escena
    public Transform finalItem; // Referencia al �tem final
    public float detectionRadius = 20f; // Radio de detecci�n para �tems cercanos

    private bool allItemsCollected = false; // Verifica si todos los �tems han sido recogidos

    void Start()
    {
        // Ocultar el �tem final hasta que todos los �tems sean recogidos
        finalItem.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!allItemsCollected)
        {
            // Buscar el �tem m�s cercano al jugador
            Transform closestItem = GetClosestItem();

            if (closestItem != null)
            {
                // Rotar la flecha hacia la direcci�n del �tem m�s cercano
                Vector3 direction = closestItem.position - player.position;
                direction.y = 0; // Ignorar la altura para que solo rote en el plano XZ
                directionalArrow.transform.rotation = Quaternion.LookRotation(direction);

                // Mostrar la flecha si hay un �tem cercano
                directionalArrow.SetActive(true);
            }
            else
            {
                // Ocultar la flecha si no hay �tems cercanos
                directionalArrow.SetActive(false);
            }
        }
        else
        {
            // Todos los �tems han sido recogidos, mostrar el �tem final
            directionalArrow.SetActive(false); // Podr�as cambiar esto si quieres que apunte al �tem final
            finalItem.gameObject.SetActive(true);
        }
    }

    // M�todo para obtener el �tem m�s cercano al jugador
    private Transform GetClosestItem()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform item in items)
        {
            if (item != null) // Verificar si el �tem a�n est� en la escena
            {
                float distance = Vector3.Distance(player.position, item.position);
                if (distance < minDistance && distance <= detectionRadius)
                {
                    minDistance = distance;
                    closest = item;
                }
            }
        }

        return closest;
    }

    // M�todo para recolectar un �tem (llamado cuando el jugador recoge uno)
    public void CollectItem(Transform item)
    {
        items.Remove(item); // Eliminar el �tem de la lista

        if (items.Count == 0)
        {
            // Todos los �tems han sido recogidos
            allItemsCollected = true;
        }
    }
}
