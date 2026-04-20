using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public Transform leftEdge;
    public Transform rightEdge;

    public Transform enemy;

    public float speed;
    private Vector3 initScale;
    private bool movingLeft;


    public float idleDuration;
    private float idleTimer;

    public Animator anime;


    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anime.SetBool("moving", false);
    }


    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }


        


    }

    private void DirectionChange()
    {
        anime.SetBool("moving", false);
        idleTimer += Time.deltaTime;


        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
   
        
           
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anime.SetBool("moving", true);


        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }
}