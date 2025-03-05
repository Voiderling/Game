using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private Animator anim;
    private bool IsAlive = true;
    private bool IsUnderAttack;
    EnemyPatrool patrol;
    private void Awake()
    {
        patrol = GetComponent<EnemyPatrool>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void EnemyTakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            anim.SetTrigger("hurt");
            Debug.Log(currentHealth);
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Enemy died");
        anim.SetTrigger("death");
        Destroy(gameObject, 1.1f);
    }
    // Update is called once per frame
    void Update()
    {
    }
    public float GetEnenemyHealth()
    {
        return currentHealth;
    }
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}