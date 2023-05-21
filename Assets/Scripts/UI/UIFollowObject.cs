using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    public Transform objectToFollow; // Объект, за которым следует UI элемент
    public Vector3 offset; // Смещение от позиции объекта

    private Camera mainCamera; // Камера для преобразования позиции
    private RectTransform rectTransform; // RectTransform UI элемента

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(objectToFollow.position + offset);
        rectTransform.position = screenPoint;
    }
}
