using UnityEngine;

public class Enhancedskeleton : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private Health playerHealth;
    private HeroKnight knight;
    private EnemyPatrool enemyPatrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponent<Animator>();
        knight = GetComponent<HeroKnight>();
        enemyPatrol = GetComponentInParent<EnemyPatrool>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                anim.SetTrigger("attack");
                cooldownTimer = 0;
            }
        }
        if (enemyPatrol != null) {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
               new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,boxCollider.bounds.size.z),
               0,Vector2.left, 0, playerLayer);
        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage,transform.position);
        }
    }
}
