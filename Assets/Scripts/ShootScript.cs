using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : AttackScript
{
    private float nextTime;
    [SerializeField] public float AttackCooldown;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public Sprite sprite;
    [SerializeField] public int Damage;
    private GameObject projectile;
    // Start is called before the first frame update
    void Awake()
    {
        nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public override void Attack() {

        if (Time.time > nextTime)
        {
            nextTime = Time.time + AttackCooldown;


            projectile = new GameObject("Projectile");

            // projectile.transform.SetParent(gameObject.transform);

            projectile.layer = 11;

            projectile.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            CircleCollider2D collider = projectile.AddComponent<CircleCollider2D>();
            collider.radius = 0.12f;
            collider.isTrigger = true;


            Rigidbody2D rigidBody = projectile.AddComponent<Rigidbody2D>();

            Projectile projectileScript = projectile.AddComponent<Projectile>();
            projectileScript.Damage = Damage;

            projectile.transform.position = gameObject.transform.position;
            if (!gameObject.GetComponent<CharacterController2D>().m_FacingRight)
            {
                projectileScript.xForce = -projectileScript.xForce;
            }
        }
    }
    
}
