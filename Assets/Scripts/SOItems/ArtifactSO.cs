using UnityEngine;

public abstract class ArtifactSO : ScriptableObject
{
   [SerializeField] private string _id;
   [SerializeField] public GameObject prefab;
   public string id => this._id;

   public Sprite sprite;

   public  T GetTypeSO<T>() where T : class
   {
      return this as T;
   }
}


