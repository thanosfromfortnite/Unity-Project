using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;
    public LayerMask layerMaskThatThisHits = LayerMask.GetMask("Entity");
    public float xForce;
    public float yForce;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = (Sprite) Resources.Load("rock.PNG");
        collider.radius = 0.12f;
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
