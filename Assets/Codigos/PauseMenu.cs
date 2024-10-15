using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;  // El panel que se mostrará al pausar
    public Image pauseImage;       // La imagen que animará su aparición
    public Button[] menuButtons;   // Lista de botones que se activarán
    public float imageAnimDuration = 1f;  // Duración de la animación de la imagen
    public float buttonDelay = 0.3f;      // Retraso entre la aparición de cada botón
    public Button pauseButton;     // El botón que se usará para pausar el juego

    private Vector3 imageInitialScale;    // Tamaño inicial de la imagen
    private Vector3 imageTargetScale;     // Tamaño final de la imagen (para cubrir la pantalla)
    private Vector3 imageInitialPosition; // Posición inicial de la imagen (esquina superior izquierda)
    private Vector3 imageTargetPosition;  // Posición final de la imagen (centrada)
    private bool isPaused = false;        // Controla si el juego está pausado

    void Start()
    {
        // Configurar el tamaño inicial (pequeño) y el tamaño final (pantalla completa)
        imageInitialScale = new Vector3(0, 0, 1); // Inicia desde cero
        imageTargetScale = new Vector3(1, 1, 1);  // Tamaño completo

        // Configurar la posición inicial (esquina superior izquierda) y la final (centrada)
        RectTransform rectTransform = pauseImage.GetComponent<RectTransform>();
        imageInitialPosition = new Vector3(-rectTransform.rect.width / 2, rectTransform.rect.height / 2, 0);
        imageTargetPosition = Vector3.zero; // Centrado

        // Asegurarse de que el panel y los botones estén desactivados al inicio
        pausePanel.SetActive(false);
        pauseImage.transform.localScale = imageInitialScale;
        pauseImage.transform.localPosition = imageInitialPosition;
        foreach (Button btn in menuButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Vincular el botón de pausa al método que abre el menú
        pauseButton.onClick.AddListener(TogglePauseMenu);
    }

    // Método que se llama al presionar el botón de pausa
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

    // Método para abrir el menú de pausa
    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);  // Activar el panel
        StartCoroutine(AnimatePauseMenu());  // Iniciar la animación del menú de pausa
        isPaused = true;
    }

    // Método para cerrar el menú de pausa
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

        // Mostrar los botones con animación sutil de escala y retraso entre ellos
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

        // Animar el botón desde una escala más pequeña hasta su tamaño original
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

        // Ocultar el panel después de la animación
        pausePanel.SetActive(false);
    }
}
