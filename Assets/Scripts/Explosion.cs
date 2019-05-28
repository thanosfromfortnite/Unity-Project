using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float nextTime;
    public CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        LayerMask layerMaskThatThisHits = layerMaskThatThisHits = LayerMask.GetMask("Entity");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collider.radius, layerMaskThatThisHits);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Entity")
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0, 35);
            }
            if (colliders[i].gameObject.tag == "Player")
            {
                colliders[i].gameObject.GetComponent<PlayerHealth>().TakeDamage((gameObject.transform.position.x - colliders[i].gameObject.transform.position.x) > 0);
            }
        }
        nextTime = Time.time + 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTime > Time.time)
        {
            Destroy(gameObject);
        }
    }
}
