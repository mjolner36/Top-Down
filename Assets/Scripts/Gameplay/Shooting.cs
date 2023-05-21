using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private string currencyWeapon = "AK-47"; // здесь как пример оружия, сменить на текущий момент нельзя
    private int currencyWeaponIndex;

    private void OnEnable()
    {
        UIController.PlayerShootEvent += Shoot;
        ChangeWeapon();
    }

    private void Shoot()
    {
        if (GameManager.Instance.Inventory.InventoryData.inventoryList[currencyWeaponIndex].amount > 0)
        {
            var bulletRef = Instantiate(bullet, shootPos.position,Quaternion.identity);
            var damage = gameObject.GetComponent<Artifact>()._artifactSo.GetTypeSO<WeaponSO>().damage;
            bulletRef.GetComponent<Bullet>().damage = damage;
            GameManager.Instance.Inventory.InventoryData.inventoryList[currencyWeaponIndex].amount--;
        }
        else
        {
            Debug.Log("not enough cartridge");
        }
    }

    public void ChangeWeapon()
    {
        for (int i = 0; i < GameManager.Instance.Inventory.InventoryData.inventoryList.Count; i++)
        {
            var item = GameManager.Instance.Inventory.InventoryData.inventoryList[i];
            if (item.id == currencyWeapon)
            {
                currencyWeaponIndex = i;
            }
        }
    }
    private void OnDisable()
    {
        UIController.PlayerShootEvent -= Shoot;
    }
}
