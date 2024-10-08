using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // el comentario
    public int totalItems = 3;
    private int itemsCollected = 0;

    public Text itemsCollectedText;

    void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
            itemsCollected++;
            Destroy(collision.gameObject);
            UpdateUI();
            RotateMap();

            if (itemsCollected == totalItems)
            {
                SpawnFinalItem();
            }
        
    }

    void UpdateUI()
    {
        itemsCollectedText.text = "Items: " + itemsCollected.ToString() + "/" + totalItems.ToString();
    }

    void RotateMap()
    {
        int randomDirection = Random.Range(0, 2);
        float rotationAngle = (randomDirection == 0) ? -90f : 90f; 

        transform.parent.Rotate(0f, 0f, rotationAngle);
    }

    void SpawnFinalItem()
    {
        Vector3 centerPosition = new Vector3(0, 0, 0);
        GameObject finalItem = Instantiate(Resources.Load("FinalItem") as GameObject, centerPosition, Quaternion.identity);
    }
}