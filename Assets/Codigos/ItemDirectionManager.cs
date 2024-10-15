using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ItemDirectionManager : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public GameObject directionalArrow; // La flecha direccional (imagen)
    public List<Transform> items; // Lista de ítems en la escena
    public Transform finalItem; // Referencia al ítem final
    public float detectionRadius = 20f; // Radio de detección para ítems cercanos

    private bool allItemsCollected = false; // Verifica si todos los ítems han sido recogidos

    void Start()
    {
        // Ocultar el ítem final hasta que todos los ítems sean recogidos
        finalItem.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!allItemsCollected)
        {
            // Buscar el ítem más cercano al jugador
            Transform closestItem = GetClosestItem();

            if (closestItem != null)
            {
                // Rotar la flecha hacia la dirección del ítem más cercano
                Vector3 direction = closestItem.position - player.position;
                direction.y = 0; // Ignorar la altura para que solo rote en el plano XZ
                directionalArrow.transform.rotation = Quaternion.LookRotation(direction);

                // Mostrar la flecha si hay un ítem cercano
                directionalArrow.SetActive(true);
            }
            else
            {
                // Ocultar la flecha si no hay ítems cercanos
                directionalArrow.SetActive(false);
            }
        }
        else
        {
            // Todos los ítems han sido recogidos, mostrar el ítem final
            directionalArrow.SetActive(false); // Podrías cambiar esto si quieres que apunte al ítem final
            finalItem.gameObject.SetActive(true);
        }
    }

    // Método para obtener el ítem más cercano al jugador
    private Transform GetClosestItem()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform item in items)
        {
            if (item != null) // Verificar si el ítem aún está en la escena
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

    // Método para recolectar un ítem (llamado cuando el jugador recoge uno)
    public void CollectItem(Transform item)
    {
        items.Remove(item); // Eliminar el ítem de la lista

        if (items.Count == 0)
        {
            // Todos los ítems han sido recogidos
            allItemsCollected = true;
        }
    }
}
