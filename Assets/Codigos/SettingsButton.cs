using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel; // El panel de ajustes que se activará/desactivará

    // Método que será llamado por el botón
    public void ToggleSettingsPanel()
    {
        // Alterna el estado activo del panel de ajustes
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}