using UnityEngine;

public class Health : MonoBehaviour
{
[SerializeField] private float startingHealth;
public float currentHealth {get; private set;}

    public void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth-_damage,0, startingHealth);
        if(currentHealth>0){
            //player hurt
        }
        else
        {

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
    }

}
