using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;
    public LayerMask layerMaskThatThisHits;
    public LayerMask ground;
    public Sprite sprite;
    private Rigidbody2D rigidBody;
    public float xForce = 25f;
    public float yForce = 25f;
    private bool left;
    public int Damage = 25;

    // Start is called before the first frame update
    void Start()
    {
        layerMaskThatThisHits = LayerMask.GetMask("Entity");
        ground = LayerMask.GetMask("Default") | LayerMask.GetMask("TransparentFX") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("Water") | LayerMask.GetMask("Obstacle");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<CircleCollider2D>();
        left = (xForce < 0);
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(new Vector2(xForce, yForce));
        if (left)
        {
            xForce++;
            if (xForce >= 0)
            {
                xForce = 0;
            }
        }
        else
        {
            xForce--;
            if (xForce <= 0)
            {
                xForce = 0;
            }
        }

        yForce--;
        if (yForce <= 0)
        {
            yForce = 0;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collider.radius, layerMaskThatThisHits);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Entity")
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0, Damage);
                Explode();
            }
        }
        Collider2D[] obstacleColliders = Physics2D.OverlapCircleAll(transform.position, collider.radius, ground);
        for (int i = 0; i < obstacleColliders.Length; i++)
        {
            if (obstacleColliders[i])
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        GameObject explosion = new GameObject("Explosion");

        // projectile.transform.SetParent(gameObject.transform);

        explosion.layer = 11;

        explosion.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = explosion.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        CircleCollider2D collider = explosion.AddComponent<CircleCollider2D>();
        collider.radius = 2f;
        collider.isTrigger = true;

        Rigidbody2D rigidBody = explosion.AddComponent<Rigidbody2D>();

        Explosion explodeScript = explosion.AddComponent<Explosion>();
        explodeScript.collider = collider;

        explosion.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
