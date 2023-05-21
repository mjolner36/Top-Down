using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    public GameObject playerRef;
    public Canvas canvas;
    public Transform startPoint;
    public InventoryHolder Inventory = new InventoryHolder();
    public static GameManager Instance;
    public CinemachineVirtualCamera VirtualCamera;

    private void Awake()
    {
        Instance = this;
        playerRef = Instantiate(playerPref, startPoint.position, Quaternion.identity);
        playerRef.GetComponent<PlayerController>().Init();
        Inventory.Init();
        canvas.GetComponent<UIController>().Init();
        VirtualCamera.Follow = playerRef.transform;
    }
}