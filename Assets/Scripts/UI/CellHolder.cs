using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellHolder : MonoBehaviour,IPointerClickHandler
{
  public Image imageItem;
  [HideInInspector] public string itemName;
  public TextMeshProUGUI amountText;
  public Button deleteItemButton;
  public void OnPointerClick(PointerEventData eventData)
  {
    deleteItemButton.gameObject.SetActive(true);
  }

  public void DeleteItemFromInventory()
  {
      GameManager.Instance.Inventory.DeleteItem(itemName);
      Destroy(gameObject);
  }
}
