using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName; // El nombre de la escena que deseas cargar

    // M�todo que ser� llamado por el bot�n
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
