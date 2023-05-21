using System;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    [SerializeField] public ArtifactSO _artifactSo;
    [SerializeField] public int amount = 1;
    public delegate void ActionArtifact(string itemId,int amount);
    public static event ActionArtifact PickUpItem;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpItem?.Invoke(_artifactSo.name,amount);
            Destroy(gameObject);
        }
    }
}
