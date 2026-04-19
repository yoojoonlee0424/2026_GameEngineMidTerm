using UnityEngine;
using System.Collections;
using UnityEditor;

public class Health_Controll : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    private Animator anim;
    private PlayerMovement Player;
    private bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        Player = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth = (currentHealth - damage);

        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
        }
        else
        {
            if (dead == false)
            {
                //anim.SetTrigger("die");

                if(Player != null)
                {
                    Player.enabled = false;
                }
                    
                if(GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponentInParent<EnemyMelee>() != null)
                {
                    GetComponentInParent<EnemyMelee>().enabled = false;
                }
                
                dead = true;

            }
        }
    }

}
