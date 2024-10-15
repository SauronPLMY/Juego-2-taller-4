using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnifyingGlass : MonoBehaviour
{
    public Camera mainCamera;       // La cámara principal que se alejará
    public Button magnifyButton;    // El botón de la lupa
    public Image chargeImage;       // Imagen que mostrará el progreso de la carga de la lupa

    // Las tres imágenes que representan los distintos estados de la lupa
    public Sprite normalSprite;     // Imagen normal (mientras se carga)
    public Sprite readySprite;      // Imagen cuando está lista
    public Sprite zoomedSprite;     // Imagen cuando el zoom está activado

    public float maxZoomOut = 10f;  // Cuánto se aleja la cámara
    public float normalZoom = 5f;   // Zoom normal de la cámara
    public float movementToCharge = 50f; // Cantidad de movimiento del jugador necesario para cargar la lupa
    public float zoomSpeed = 1f;    // Velocidad del zoom

    private float playerMovement = 0f;   // Movimiento acumulado del jugador
    private bool isZoomedOut = false;    // Controla si la cámara está en el modo alejado
    private bool isCharged = false;      // Controla si la lupa está completamente cargada

    private Image magnifyButtonImage;    // Referencia al componente Image del botón de lupa

    void Start()
    {
        // Desactivar el botón de la lupa al inicio
        magnifyButton.interactable = false;

        // Ocultar la imagen de carga
        chargeImage.fillAmount = 0;

        // Añadir la función al botón de lupa
        magnifyButton.onClick.AddListener(ToggleZoom);

        // Obtener la imagen del botón para cambiar su sprite
        magnifyButtonImage = magnifyButton.GetComponent<Image>();

        // Establecer la imagen inicial de la lupa como "normal"
        magnifyButtonImage.sprite = normalSprite;
    }

    void Update()
    {
        // Comprobar si la tecla "P" es presionada para simular el movimiento del jugador
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Tecla P presionada");
            UpdateCharge(movementToCharge * 0.5f); // Simula la carga de la lupa
        }
    }

    // Método para actualizar la carga basada en el movimiento del jugador
    public void UpdateCharge(float movementAmount)
    {
        playerMovement += movementAmount;

        // Actualizar la imagen de carga
        chargeImage.fillAmount = Mathf.Clamp01(playerMovement / movementToCharge);

        // Si la lupa está completamente cargada, activar el botón
        if (playerMovement >= movementToCharge && !isCharged)
        {
            isCharged = true;
            magnifyButton.interactable = true;

            // Cambiar la imagen de la lupa a la imagen lista
            magnifyButtonImage.sprite = readySprite;
        }
    }

    // Método que se llama al presionar el botón de la lupa
    void ToggleZoom()
    {
        if (!isCharged) return;  // No permitir el zoom si no está cargada

        if (!isZoomedOut)
        {
            // Alejar la cámara
            StartCoroutine(ZoomCamera(mainCamera.orthographicSize, maxZoomOut));

            // Cambiar la imagen de la lupa al sprite de zoom activado
            magnifyButtonImage.sprite = zoomedSprite;
        }
        else
        {
            // Volver al zoom normal
            StartCoroutine(ZoomCamera(mainCamera.orthographicSize, normalZoom));

            // Cambiar la imagen de la lupa a la imagen lista (para que pueda ser presionada de nuevo)
            magnifyButtonImage.sprite = readySprite;
        }

        // Alternar el estado de zoom
        isZoomedOut = !isZoomedOut;

        // Desactivar la lupa y resetear la carga después de usarla
        if (isZoomedOut) // Solo desactiva cuando se aleja
        {
            magnifyButton.interactable = false;
            isCharged = false;
            playerMovement = 0;
            chargeImage.fillAmount = 0;

            // Volver la imagen de la lupa al sprite "normal" cuando se desactive
            magnifyButtonImage.sprite = normalSprite;
        }
    }

    // Coroutine para hacer un zoom suave en la cámara
    IEnumerator ZoomCamera(float fromZoom, float toZoom)
    {
        float elapsedTime = 0f;
        while (elapsedTime < zoomSpeed)
        {
            mainCamera.orthographicSize = Mathf.Lerp(fromZoom, toZoom, elapsedTime / zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = toZoom;
    }
}
