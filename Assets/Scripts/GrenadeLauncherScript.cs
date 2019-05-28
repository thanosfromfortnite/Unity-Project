using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherScript : AttackScript
{
    private float nextTime;
    [SerializeField] public float AttackCooldown;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public int Damage = 25;
    [SerializeField] public Sprite sprite;
    [SerializeField] public Sprite explosionSprite;
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


    public override void Attack()
    {

        if (Time.time > nextTime)
        {
            nextTime = Time.time + AttackCooldown;


            projectile = new GameObject("Grenade");

            // projectile.transform.SetParent(gameObject.transform);

            projectile.layer = 11;

            projectile.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            CircleCollider2D collider = projectile.AddComponent<CircleCollider2D>();
            collider.radius = 0.1f;
            collider.isTrigger = true;


            Rigidbody2D rigidBody = projectile.AddComponent<Rigidbody2D>();

            Grenade grenade = projectile.AddComponent<Grenade>();

            grenade.sprite = explosionSprite;

            projectile.transform.position = gameObject.transform.position;
            if (!gameObject.GetComponent<CharacterController2D>().m_FacingRight)
            {
                grenade.xForce = -grenade.xForce;
            }
        }
    }
}
