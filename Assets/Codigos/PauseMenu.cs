using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;  // El panel que se mostrar� al pausar
    public Image pauseImage;       // La imagen que animar� su aparici�n
    public Button[] menuButtons;   // Lista de botones que se activar�n
    public float imageAnimDuration = 1f;  // Duraci�n de la animaci�n de la imagen
    public float buttonDelay = 0.3f;      // Retraso entre la aparici�n de cada bot�n
    public Button pauseButton;     // El bot�n que se usar� para pausar el juego

    private Vector3 imageInitialScale;    // Tama�o inicial de la imagen
    private Vector3 imageTargetScale;     // Tama�o final de la imagen (para cubrir la pantalla)
    private Vector3 imageInitialPosition; // Posici�n inicial de la imagen (esquina superior izquierda)
    private Vector3 imageTargetPosition;  // Posici�n final de la imagen (centrada)
    private bool isPaused = false;        // Controla si el juego est� pausado

    void Start()
    {
        // Configurar el tama�o inicial (peque�o) y el tama�o final (pantalla completa)
        imageInitialScale = new Vector3(0, 0, 1); // Inicia desde cero
        imageTargetScale = new Vector3(1, 1, 1);  // Tama�o completo

        // Configurar la posici�n inicial (esquina superior izquierda) y la final (centrada)
        RectTransform rectTransform = pauseImage.GetComponent<RectTransform>();
        imageInitialPosition = new Vector3(-rectTransform.rect.width / 2, rectTransform.rect.height / 2, 0);
        imageTargetPosition = Vector3.zero; // Centrado

        // Asegurarse de que el panel y los botones est�n desactivados al inicio
        pausePanel.SetActive(false);
        pauseImage.transform.localScale = imageInitialScale;
        pauseImage.transform.localPosition = imageInitialPosition;
        foreach (Button btn in menuButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Vincular el bot�n de pausa al m�todo que abre el men�
        pauseButton.onClick.AddListener(TogglePauseMenu);
    }

    // M�todo que se llama al presionar el bot�n de pausa
    public void TogglePauseMenu()
    {
        if (!isPaused)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    // M�todo para abrir el men� de pausa
    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);  // Activar el panel
        StartCoroutine(AnimatePauseMenu());  // Iniciar la animaci�n del men� de pausa
        isPaused = true;
    }

    // M�todo para cerrar el men� de pausa
    public void ClosePauseMenu()
    {
        StartCoroutine(ClosePauseCoroutine());
        isPaused = false;
    }

    IEnumerator AnimatePauseMenu()
    {
        // Animar la imagen desde la esquina superior izquierda hacia el centro
        float elapsedTime = 0f;
        while (elapsedTime < imageAnimDuration)
        {
            float progress = elapsedTime / imageAnimDuration;
            pauseImage.transform.localScale = Vector3.Lerp(imageInitialScale, imageTargetScale, progress);
            pauseImage.transform.localPosition = Vector3.Lerp(imageInitialPosition, imageTargetPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pauseImage.transform.localScale = imageTargetScale;
        pauseImage.transform.localPosition = imageTargetPosition;

        // Mostrar los botones con animaci�n sutil de escala y retraso entre ellos
        foreach (Button btn in menuButtons)
        {
            btn.gameObject.SetActive(true);
            StartCoroutine(AnimateButton(btn));
            yield return new WaitForSeconds(buttonDelay);
        }
    }

    IEnumerator AnimateButton(Button btn)
    {
        RectTransform btnTransform = btn.GetComponent<RectTransform>();
        Vector3 initialScale = new Vector3(0.8f, 0.8f, 1f);
        Vector3 targetScale = Vector3.one;
        float elapsedTime = 0f;
        float animDuration = 0.3f;

        // Animar el bot�n desde una escala m�s peque�a hasta su tama�o original
        btnTransform.localScale = initialScale;
        while (elapsedTime < animDuration)
        {
            float progress = elapsedTime / animDuration;
            btnTransform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        btnTransform.localScale = targetScale;
    }

    IEnumerator ClosePauseCoroutine()
    {
        // Ocultar los botones
        foreach (Button btn in menuButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Animar la imagen de regreso a la esquina superior izquierda
        float elapsedTime = 0f;
        while (elapsedTime < imageAnimDuration)
        {
            float progress = elapsedTime / imageAnimDuration;
            pauseImage.transform.localScale = Vector3.Lerp(imageTargetScale, imageInitialScale, progress);
            pauseImage.transform.localPosition = Vector3.Lerp(imageTargetPosition, imageInitialPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pauseImage.transform.localScale = imageInitialScale;
        pauseImage.transform.localPosition = imageInitialPosition;

        // Ocultar el panel despu�s de la animaci�n
        pausePanel.SetActive(false);
    }
}
