using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  

public class ItemCollector : MonoBehaviour
{
    public int totalItems = 3;
    private int itemsCollected = 0;
    public Transform mapaTransform;
    public TMP_Text itemsCollectedText;
    Quaternion nextRotation;
    public float velocidadRotacion;
    float lastRotAngle;

    public GameObject finalItem;

    void Start()
    {
        UpdateUI();
        if (finalItem != null)
        {
            finalItem.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemsCollected++;
            Destroy(collision.gameObject);
            UpdateUI();
            Invoke("RotateMap", 0.2f);
            //RotateMap();

            if (itemsCollected == totalItems)
            {
                ActivateFinalItem(); 
            }
        }
        else if (collision.CompareTag("FinalItem"))  
        {
            Debug.Log("Final Item Collected!"); 
            LoadNextScene();  
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

    public void RotateMap()
    {
        int randomDirection = Random.Range(0, 100);
        float rotationAngle = (randomDirection > 50) ? -90f : 90f;
        lastRotAngle += rotationAngle;

        nextRotation = Quaternion.Euler(0, 0, lastRotAngle);
    }

    void ActivateFinalItem()
    {
        if (finalItem != null)
        {
            finalItem.SetActive(true);
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
        /*
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Loading next scene: " + nextSceneIndex);  
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No hay más escenas disponibles en Build Settings.");
        }
        */
    }
}