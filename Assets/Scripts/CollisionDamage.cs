using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public CircleCollider2D objectCollider;
    [SerializeField] public LayerMask PlayerLayer;
    [SerializeField] public float knockbackX, knockbackY;
    [SerializeField] public int Damage = 1;

    
    void Start()
    {
        objectCollider = transform.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(objectCollider.transform.position, objectCollider.radius * 5, PlayerLayer);
        for (int i = 0; i < colliders.Length; i ++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                colliders[i].gameObject.GetComponent<PlayerHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0, Damage, knockbackX, knockbackY);
                break;
            }
        }
    }

    
    /*void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.tag == "Player") {
	        other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
	    }
    }*/
}
