using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagnifyingGlassUpgrade : MonoBehaviour
{
    public MagnifyingGlass magnifyingGlass; // Referencia al script de la lupa
    public int upgradeCost = 50; // El costo para mejorar la lupa
    public float zoomIncrease = 2f; // Cuánto aumentará el zoom máximo cada vez que mejoremos la lupa
    public TextMeshProUGUI coinsText; // Referencia al texto que muestra la cantidad de monedas
    public TextMeshProUGUI upgradeLevelText; // Referencia al texto que muestra el nivel de mejora de la lupa
    public Button upgradeButton; // El botón de mejora

    private int coins = 0; // Monedas iniciales
    private int upgradeLevel = 1; // Nivel inicial de la lupa

    void Start()
    {
        // Actualizar el texto de monedas y nivel de mejora
        UpdateCoinsText();
        UpdateUpgradeLevelText();

        // Añadir la función al botón de mejora
        upgradeButton.onClick.AddListener(UpgradeMagnifyingGlass);
    }

    // Método para mejorar la lupa
    public void UpgradeMagnifyingGlass()
    {
        // Verificar si tenemos suficientes monedas para mejorar
        if (coins >= upgradeCost)
        {
            coins -= upgradeCost; // Restar el costo de la mejora de las monedas
            magnifyingGlass.maxZoomOut += zoomIncrease; // Aumentar el máximo de zoom de la lupa
            upgradeLevel++; // Incrementar el nivel de mejora

            // Actualizar el texto de monedas y nivel de mejora
            UpdateCoinsText();
            UpdateUpgradeLevelText();

            Debug.Log("Lupa mejorada. Nuevo zoom máximo: " + magnifyingGlass.maxZoomOut);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para mejorar.");
        }
    }

    // Método para añadir monedas (llámalo cuando el jugador gane monedas)
    public void AddCoins(int amount)
    {
        coins += amount;

        // Actualizar el texto de monedas
        UpdateCoinsText();
    }

    // Actualiza el texto de monedas en la UI
    private void UpdateCoinsText()
    {
        coinsText.text = "Monedas: " + coins;
    }

    // Actualiza el texto del nivel de mejora en la UI
    private void UpdateUpgradeLevelText()
    {
        upgradeLevelText.text = "Nivel de Lupa: " + upgradeLevel;
    }
}

