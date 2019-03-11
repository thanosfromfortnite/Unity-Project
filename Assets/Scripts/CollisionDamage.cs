using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public Collider2D objectCollider;
    public Collider2D anotherCollider;

    
    void Start()
    {
        objectCollider = transform.GetComponent<CircleCollider2D>();
        anotherCollider = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }


    private void Touch()
    {
        if (objectCollider.IsTouching(anotherCollider))
        {
            Debug.Log("hey");
        }
    }
}
