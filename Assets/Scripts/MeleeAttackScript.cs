using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackScript : AttackScript
{
    [SerializeField] public float AttackCooldown;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public int Damage = 25;
    [SerializeField] public LayerMask HittableLayer;
    [SerializeField] public CapsuleCollider2D attackHitbox;
    [SerializeField] public SpriteRenderer HitSprite;
    private float nextTime;

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox = transform.GetComponent<CapsuleCollider2D>();
        HitSprite.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public override void Attack()
    {
        if (Time.time > nextTime)
        {
            StartCoroutine(AnimationCooldown());
            
            nextTime = Time.time + AttackCooldown;
        }
    }

    IEnumerator AnimationCooldown()
    {
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("IsAttacking", true);
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(attackHitbox.transform.position, attackHitbox.size * 2, attackHitbox.direction, HittableLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Entity")
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0, Damage, 350, 175);
                break;
            }
        }
        yield return new WaitForSecondsRealtime(0.2f);
        
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("IsAttacking", false);
        
    }
}
