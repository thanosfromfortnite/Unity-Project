using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeEnemyHealth : MonoBehaviour
{

    Transform spriteTransform;
    private int maxHealth;
    private int currentHealth;
    private float scaleBefore;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform.Find("HealthBar").transform.Find("HealthHealthBar").transform;
        maxHealth = transform.gameObject.GetComponent<EnemyHealth>().MaxHealth;
        currentHealth = transform.gameObject.GetComponent<EnemyHealth>().Health;
        scaleBefore = spriteTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = transform.gameObject.GetComponent<EnemyHealth>().Health;
        spriteTransform.localScale = new Vector3(scaleBefore *  ((float) currentHealth / (float) maxHealth), spriteTransform.localScale.y);
    }
}
