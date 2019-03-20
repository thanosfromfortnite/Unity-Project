using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;
    public LayerMask layerMaskThatThisHits;
    public LayerMask ground;
    private Rigidbody2D rigidBody;
    public float xForce = 0.5f;
    public float yForce = 0f;
    public int Damage = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        layerMaskThatThisHits = LayerMask.GetMask("Entity");
        ground = LayerMask.GetMask("Default") | LayerMask.GetMask("TransparentFX") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("Water") | LayerMask.GetMask("Obstacle");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(new Vector2(xForce, yForce));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collider.radius, layerMaskThatThisHits);
        for (int i = 0; i < colliders.Length; i ++)
        {
            if (colliders[i].gameObject.tag == "Entity")
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0, Damage);
                Destroy(gameObject);
            }
        }
        Collider2D[] obstacleColliders = Physics2D.OverlapCircleAll(transform.position, collider.radius, ground);
        for (int i = 0; i < obstacleColliders.Length; i ++)
        {
            if (obstacleColliders[i])
            {
                Destroy(gameObject);
            }
        }
    }
}
