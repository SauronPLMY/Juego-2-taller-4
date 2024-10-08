using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCollector : MonoBehaviour
{
    public int totalItems = 3;
    public int itemsCollected = 0;
    public Transform mapaTransform;
    public TextMeshProUGUI itemsCollectedText;
    Quaternion nextRotation;
    public float velocidadRotacion;
    float lastRotAngle;

    public GameObject finalItemPrefab;

    void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemsCollected++;
            Destroy(collision.gameObject);
            UpdateUI();
            RotateMap();

            if (itemsCollected == totalItems)
            {
                SpawnFinalItem();
            }
        }
        else if (collision.CompareTag("FinalItem"))
        {
            Destroy(collision.gameObject);
            GoToNextLevel();
        }
    }

    private void Update()
    {
        mapaTransform.rotation = Quaternion.Lerp(mapaTransform.rotation, nextRotation, velocidadRotacion * Time.deltaTime);
    }

    void UpdateUI()
    {
        if (itemsCollectedText)
            itemsCollectedText.text = "Items: " + itemsCollected.ToString() + "/" + totalItems.ToString();
    }

    [ContextMenu("rotar")]
    public void RotateMap()
    {
        int randomDirection = Random.Range(0, 100);
        float rotationAngle = (randomDirection > 50) ? -90f : 90f;
        lastRotAngle += rotationAngle;

        nextRotation = Quaternion.Euler(0, 0, lastRotAngle);
    }

    void SpawnFinalItem()
    {
        if (finalItemPrefab != null)
        {
            Vector3 centerPosition = new Vector3(0, 0, 0);
            Instantiate(finalItemPrefab, centerPosition, Quaternion.identity);
            Debug.Log("Apareció el Item Final");
        }
        else
        {
            Debug.LogWarning("Final Item Prefab no asignado en el Inspector");
        }
    }

    void GoToNextLevel()
    {
        Debug.Log("Has recolectado el último item. Cambiando al siguiente nivel...");
    }
}