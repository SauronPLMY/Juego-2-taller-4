using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemDirectionManager itemManager; // Referencia al gestor de ítems

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador recoge el ítem
            itemManager.CollectItem(transform);

            // Destruir o desactivar el ítem
            Destroy(gameObject);
        }
    }
}
