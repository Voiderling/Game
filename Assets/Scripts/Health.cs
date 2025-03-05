using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private HeroKnight knight;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    [Header("Components")]

    [SerializeField] private Behaviour[] components; 
     
    private void Awake()
    {
        knight = GetComponent<HeroKnight>(); 
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage, Vector3 attackerPosition)
    {
        HeroKnight heroKnight = GetComponent<HeroKnight>();
        if(heroKnight != null && heroKnight.IsInvulnerable())
    {
            return;
        }
        if (heroKnight != null && heroKnight.IsBlocking()) {
            bool isAttackerInFront =
             (heroKnight.GetFacingDirection() == 1 && attackerPosition.x > transform.position.x) ||
             (heroKnight.GetFacingDirection() == -1 && attackerPosition.x < transform.position.x);
            if (isAttackerInFront) {
                _damage = 0;
            }
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            if (heroKnight.IsBlocking())
            {
                anim.SetTrigger("Block");
            }
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Death");
                GetComponent<HeroKnight>().enabled = false;
                GetComponent<HeroKnight>().disableMovement();
                dead = true;
            }
        }
    }
    private void Update()
    {
      
    }
    public void AddHealt(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7,10,true);
        Physics2D.IgnoreLayerCollision(7, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes / 3 ));
        }
    }
}