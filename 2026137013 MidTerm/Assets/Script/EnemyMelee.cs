using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float attackCooldown = 0.1f;
    public float range;
    public float colliderDistance;

    public int damage;
    private float cooldownTimer = Mathf.Infinity;

    public BoxCollider2D BoxCollider;
    public LayerMask playerLayer;

    private Health_Controll PlayerHealth;

    private Animator anime;
    
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        
        anime = GetComponent<Animator>();

        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                //anime.SetTrigger("meleeAttack");
                PlayerDamage();
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerSight();
        }

    }


    public bool PlayerSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y,BoxCollider.bounds.size.z),
            0,Vector2.left,0,playerLayer);

        if (hit.collider != null)
        {
            PlayerHealth = hit.transform.GetComponent<Health_Controll>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z));
    }


    private void PlayerDamage()
    {
        if (PlayerSight())
        {
            PlayerHealth.TakeDamage(damage);
        }
    }

}
