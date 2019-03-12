using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeHealth : MonoBehaviour
{
    private int health;
    private int maxHealth;
    private GameObject[] healthSprites;
    [SerializeField] public Sprite HealthSprite;
    private float size;
    

    // Start is called before the first frame update


    void Start()
    {
        size = transform.Find("Background").transform.position.x * 2;
        maxHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().MaxHealth;
        healthSprites = new GameObject[maxHealth];
        
    }

    // Update is called once per frame
    void Update()
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>().Health;
        for (int i = 0; i < healthSprites.Length; i ++)
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer();
            spriteRenderer.sprite = HealthSprite;
            healthSprites[i] = new GameObject("Health " + i);
            spriteRenderer = healthSprites[i].AddComponent<SpriteRenderer>();
            healthSprites[i].transform.position = new Vector3(i / size, transform.Find("Background").transform.position.y);
        }
    }

    void Positioning()
    {
    }
}
