
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().m_Follow = GameManager.Instance.playerRef.transform;
    }
}
