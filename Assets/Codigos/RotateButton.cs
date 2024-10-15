using UnityEngine;
using UnityEngine.UI;

public class RotateButton : MonoBehaviour
{
    public float normalSpeed = 100f;  // Velocidad normal de rotaci�n
    public float spasmSpeed = 300f;   // Velocidad durante los espasmos
    public float spasmDuration = 0.5f;  // Duraci�n de cada espasmo
    public float spasmCooldown = 2f;  // Tiempo entre espasmos

    private RectTransform rectTransform;
    private float currentSpeed;       // Velocidad actual de rotaci�n
    private bool isSpasming = false;  // Controla si est� en espasmo
    private float spasmTime;          // Temporizador de espasmo
    private float nextSpasmTime;      // Tiempo para el pr�ximo espasmo
    private int direction = 1;        // Direcci�n de rotaci�n (1 o -1)

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentSpeed = normalSpeed;
        nextSpasmTime = Time.time + Random.Range(1f, spasmCooldown);
    }

    void Update()
    {
        // Verificar si es tiempo de iniciar un espasmo
        if (!isSpasming && Time.time >= nextSpasmTime)
        {
            StartSpasm();
        }

        // Si est� en espasmo, mantener el temporizador activo
        if (isSpasming)
        {
            spasmTime -= Time.deltaTime;
            if (spasmTime <= 0)
            {
                EndSpasm();
            }
        }

        // Aplicar rotaci�n con la velocidad actual y la direcci�n
        rectTransform.Rotate(Vector3.forward * currentSpeed * direction * Time.deltaTime);
    }

    void StartSpasm()
    {
        isSpasming = true;
        currentSpeed = spasmSpeed;
        direction = -direction;  // Cambiar de direcci�n
        spasmTime = spasmDuration;
    }

    void EndSpasm()
    {
        isSpasming = false;
        currentSpeed = normalSpeed;
        nextSpasmTime = Time.time + Random.Range(1f, spasmCooldown);  // Nuevo tiempo aleatorio para el siguiente espasmo
    }
}