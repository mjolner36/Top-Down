using UnityEngine;

public class EnemySO : ScriptableObject
{
   public int hp;
   public int moveSpeed;
   public int damage;
   public GameObject prefab;
   public int rangeToSee = 20;
   public int attackRange = 5;
   public Color healthBarColor = new Color(0.8f, 0.2f, 0.1f, 1f);
}
