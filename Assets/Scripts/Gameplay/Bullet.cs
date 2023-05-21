using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void Action(int damage);
    public static event Action OnBulletHit;
    public int damage;
    public float speed = 20f;

    private void OnEnable()
    {
        Destroy(gameObject,1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Если пуля столкнулась с игроком
        {
            OnBulletHit?.Invoke(damage); // Вызов события попадания пули
        }
    }

    public void Update()
    {
        transform.position += transform.right * (speed * Time.deltaTime);
    }
}

