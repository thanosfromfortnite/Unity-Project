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
        for (int i = 0; i < healthSprites.Length; i++)
        {
            healthSprites[i] = new GameObject();
            healthSprites[i].transform.SetParent(GameObject.Find("HealthBar").transform);
            healthSprites[i].name = "Health";
            healthSprites[i].AddComponent<SpriteRenderer>();
            SpriteRenderer sr = healthSprites[i].GetComponent<SpriteRenderer>();
            sr.sprite = HealthSprite;
            sr.sortingOrder = 20;
            sr.transform.localScale = new Vector3(1.0f, 1.0f);
            healthSprites[i].transform.position = new Vector3(((healthSprites.Length - i) / (size / 5)) + (size / 12), transform.Find("Background").transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

        health = GameObject.Find("Player").GetComponent<PlayerHealth>().Health;
        if (health >= 0)
        {
            for (int i = health - 1; i >= 0; i--)
            //for (int i = 0; i < health; i++)
            {
                SpriteRenderer sr = healthSprites[i].GetComponent<SpriteRenderer>();
                sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            //for (int i = health; i < healthSprites.Length; i ++)
            for (int i = healthSprites.Length - 1; i >= health; i--)
            {
                SpriteRenderer sr = healthSprites[i].GetComponent<SpriteRenderer>();
                sr.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
            }

        }

    }

}
