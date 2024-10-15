using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemDirectionManager itemManager; // Referencia al gestor de �tems

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador recoge el �tem
            itemManager.CollectItem(transform);

            // Destruir o desactivar el �tem
            Destroy(gameObject);
        }
    }
}
