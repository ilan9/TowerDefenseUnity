using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int health = 2;
    [SerializeField] private int damageee = 2;
    private Rigidbody2D rb;
    private Transform checkpoint;
    private int index = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        checkpoint = EnemyManager.main.checkpoints[index];
    }

    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * speed;
    }
    void Update()
    {
        checkpoint = EnemyManager.main.checkpoints[index];

        if (Vector2.Distance(checkpoint.transform.position,transform.position) <= 0.1f)
        {
            index ++;
            if (index >= EnemyManager.main.checkpoints.Length)
            {
                print("enlever pv a tour");
                Player.main.health -= damageee;
                Destroy(gameObject);

            }
        }
    }
    
    public void damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
