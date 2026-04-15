using UnityEngine;

public class TraceEnemyAi : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public float raycastDistance = 10.0f;
    public float traceRange = 5.0f;

    private Transform player;


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



    }
}
