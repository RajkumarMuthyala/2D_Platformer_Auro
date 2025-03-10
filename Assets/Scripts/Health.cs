using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{
[Header ("Health")]
[SerializeField] private float startingHealth;
public float currentHealth {get; private set;}
private Animator anim;
private bool dead;


[Header("iFrames")]
[SerializeField] private float iFramesDuration;
[SerializeField] private int inumberOfFlashes;
private SpriteRenderer spriteRend;

    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth-_damage,0, startingHealth);
        if(currentHealth>0){
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead){
           anim.SetTrigger("die");
           GetComponent<PlayerMovement>().enabled = false;
           dead = true;
            }
           
        }
    }
}
