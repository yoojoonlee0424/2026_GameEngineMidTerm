using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Health_Controll : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    private Animator anim;
    private PlayerMovement Player;
    private bool dead = false;

    float score;

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
            anim.SetTrigger("hurt");
        }
        else
        {
            if (dead == false)
            {
                anim.SetTrigger("die");

                if(Player != null)
                {
                    Player.enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                    
                if(GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponentInParent<EnemyMelee>() != null)
                {
                    GetComponentInParent<EnemyMelee>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;

                    score += 1f;
                }


                if(GetComponent<TraceEnemyAi>() != null)
                {
                    GetComponentInParent<TraceEnemyAi>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Invoke(nameof(DestroyObj), 2f);

                    score += 1f;
                }

                Debug.Log("hit");

                dead = true;

                

            }
        }
    }


    public void Invincible()
    {
        currentHealth = 1000000;
    }

    public void InvincibleOff()
    {
        currentHealth = startingHealth;
    }




    void DestroyObj()
    {
        Destroy(this.gameObject);
    }

}
