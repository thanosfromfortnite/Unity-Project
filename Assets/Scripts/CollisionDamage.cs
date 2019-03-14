using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public CircleCollider2D objectCollider;
    public Collider2D anotherCollider;
    [SerializeField] public LayerMask PlayerLayer;

    
    void Start()
    {
        objectCollider = transform.GetComponent<CircleCollider2D>();
        anotherCollider = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(objectCollider.transform.position, objectCollider.radius, PlayerLayer);
        for (int i = 0; i < colliders.Length; i ++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                anotherCollider.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
    }

    
    /*void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.tag == "Player") {
	        other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
	    }
    }*/
}
