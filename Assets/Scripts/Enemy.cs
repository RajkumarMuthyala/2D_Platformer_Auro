using Unity.VisualScripting;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private float damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        collision.GetComponent<Health>().TakeDamage(damage);
    }
}
