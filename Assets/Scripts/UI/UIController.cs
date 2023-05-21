using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemsGroup;
    [SerializeField] private GameObject cellPref;
    [SerializeField] private GameObject DiePanel;
    
    public delegate void PlayerAction();
    public static event PlayerAction PlayerShootEvent;
    public void Init()
    {
        GameManager.Instance.Inventory.ChangerEvent += LoadInventory;
        PlayerController.PlayerDieEvent += PlayerDeath;
    }

    private void LoadInventory()
    {
        for (var index = 0; index < itemsGroup.transform.childCount; index++)
        {
            Destroy(itemsGroup.transform.GetChild(index).gameObject);
        }
        foreach (var item in GameManager.Instance.Inventory.InventoryData.inventoryList)
        {
            foreach (var artifactSO in GameManager.Instance.Inventory.AllArtifactSOs)
            {
                if (item.id != artifactSO.id) continue;
                var tempCell = Instantiate(cellPref, itemsGroup.transform).GetComponent<CellHolder>();
            
                tempCell.imageItem.sprite = artifactSO.sprite;
                tempCell.itemName = artifactSO.id;
                tempCell.amountText.text = "x" + item.amount;
            }
        }
    }

    public void Shoot()
    {
        PlayerShootEvent?.Invoke();
    }
    
    public void OpenInventoryPanel()
    {
        inventoryPanel.transform.SetAsLastSibling();
        LoadInventory();
        inventoryPanel.SetActive(true);
    }
    
    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
    
    private void PlayerDeath()
    {
        DiePanel.transform.SetAsLastSibling();
        DiePanel.SetActive(true);
    }

    public void RestartGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        GameManager.Instance.Inventory.ChangerEvent -= LoadInventory;
        PlayerController.PlayerDieEvent -= PlayerDeath;
    }
}
