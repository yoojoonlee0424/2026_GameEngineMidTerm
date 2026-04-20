using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackTranform;
    public float attackRange = 1.5f;
    public LayerMask attackableLayer;

    public float damage;

    private RaycastHit2D[] hits;





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }



    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTranform.position, attackRange, transform.right, 0f, attackableLayer);

        for(int i = 0; i < hits.Length; i++)
        {
            Health_Controll health_Controll = hits[i].collider.gameObject.GetComponent<Health_Controll>();
            if(health_Controll != null )
            {
                health_Controll.TakeDamage(damage);
            }
            
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTranform.position, attackRange);
    }

}
