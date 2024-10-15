using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnifyingGlass : MonoBehaviour
{
    public Camera mainCamera;       // La c�mara principal que se alejar�
    public Button magnifyButton;    // El bot�n de la lupa
    public Image chargeImage;       // Imagen que mostrar� el progreso de la carga de la lupa

    // Las tres im�genes que representan los distintos estados de la lupa
    public Sprite normalSprite;     // Imagen normal (mientras se carga)
    public Sprite readySprite;      // Imagen cuando est� lista
    public Sprite zoomedSprite;     // Imagen cuando el zoom est� activado

    public float maxZoomOut = 10f;  // Cu�nto se aleja la c�mara
    public float normalZoom = 5f;   // Zoom normal de la c�mara
    public float movementToCharge = 50f; // Cantidad de movimiento del jugador necesario para cargar la lupa
    public float zoomSpeed = 1f;    // Velocidad del zoom

    private float playerMovement = 0f;   // Movimiento acumulado del jugador
    private bool isZoomedOut = false;    // Controla si la c�mara est� en el modo alejado
    private bool isCharged = false;      // Controla si la lupa est� completamente cargada

    private Image magnifyButtonImage;    // Referencia al componente Image del bot�n de lupa

    void Start()
    {
        // Desactivar el bot�n de la lupa al inicio
        magnifyButton.interactable = false;

        // Ocultar la imagen de carga
        chargeImage.fillAmount = 0;

        // A�adir la funci�n al bot�n de lupa
        magnifyButton.onClick.AddListener(ToggleZoom);

        // Obtener la imagen del bot�n para cambiar su sprite
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

    // M�todo para actualizar la carga basada en el movimiento del jugador
    public void UpdateCharge(float movementAmount)
    {
        playerMovement += movementAmount;

        // Actualizar la imagen de carga
        chargeImage.fillAmount = Mathf.Clamp01(playerMovement / movementToCharge);

        // Si la lupa est� completamente cargada, activar el bot�n
        if (playerMovement >= movementToCharge && !isCharged)
        {
            isCharged = true;
            magnifyButton.interactable = true;

            // Cambiar la imagen de la lupa a la imagen lista
            magnifyButtonImage.sprite = readySprite;
        }
    }

    // M�todo que se llama al presionar el bot�n de la lupa
    void ToggleZoom()
    {
        if (!isCharged) return;  // No permitir el zoom si no est� cargada

        if (!isZoomedOut)
        {
            // Alejar la c�mara
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

        // Desactivar la lupa y resetear la carga despu�s de usarla
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

    // Coroutine para hacer un zoom suave en la c�mara
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
