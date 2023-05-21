using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemySO> enemySOList;

    [SerializeField] private int enemyOnLvl = 3;
    [SerializeField] private List<BoxCollider2D> spawnAreas;
    
    public void Awake()
    {
        enemySOList = new List<EnemySO>(Resources.LoadAll<EnemySO>(""));

        for (int count = 0; count < enemyOnLvl; count++)
        {
            var tempPref = Instantiate(enemySOList[0].prefab,SpawnEnemy(),enemySOList[0].prefab.transform.rotation).GetComponent<EnemyController>();
            tempPref.Init();
        }
    }

    private Vector2 SpawnEnemy()
    {
        Bounds bounds = spawnAreas[0].bounds;
        
        // Случайная позиция внутри области спавна
        float xPos = Random.Range(bounds.min.x, bounds.max.x);
        float yPos = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        return spawnPos;
    }
}
