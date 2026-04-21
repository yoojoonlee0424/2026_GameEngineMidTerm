using UnityEngine;

public class TraceEnemyAi : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public float raycastDistance = 10.0f;
    public float traceRange = 5.0f;


    public float attackCooldown = 0.1f;
    public float range;
    public float colliderDistance;

    public float damage;
    private float cooldownTimer = Mathf.Infinity;

    public BoxCollider2D BoxCollider;

    private Transform player;
    public LayerMask playerLayer;

    private Health_Controll PlayerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector2 direction = player.position - transform.position;


        if(direction.magnitude > traceRange)
        {
            return;
        }


        Vector2 directionNormalized = direction.normalized;

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        foreach(RaycastHit2D h in hit)
        {
            if(h.collider != null && h.collider.CompareTag("Player"))
            {
                Vector3 altDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(altDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }

        cooldownTimer += Time.deltaTime;

        if (PlayerSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                PlayerDamage();
            }
        }

    }


    public bool PlayerSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

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
