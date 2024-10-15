using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel; // El panel de ajustes que se activar�/desactivar�

    // M�todo que ser� llamado por el bot�n
    public void ToggleSettingsPanel()
    {
        // Alterna el estado activo del panel de ajustes
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}