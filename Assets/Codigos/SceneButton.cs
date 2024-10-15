using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName; // El nombre de la escena que deseas cargar

    // Método que será llamado por el botón
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
