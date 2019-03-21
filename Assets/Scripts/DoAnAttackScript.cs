using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAnAttackScript : MonoBehaviour
{
    public AttackScript[] attacks = new AttackScript[2];
    public Sprite[] attackSprites = new Sprite[2];
    public int currentAttack = 0;
    [SerializeField] private Sprite meleeSprite;
    [SerializeField] private Sprite rangedSprite;
    // Start is called before the first frame update
    void Start()
    {
        attacks[0] = gameObject.GetComponent<MeleeAttackScript>();
        attacks[1] = gameObject.GetComponent<ShootScript>();
        attackSprites[0] = meleeSprite;
        attackSprites[1] = rangedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch") && Input.GetAxisRaw("Switch") > 0)
        {
            currentAttack++;
            if (currentAttack > attacks.Length - 1)
            {
                currentAttack = 0;
            }
        }
        else if (Input.GetButtonDown("Switch") && Input.GetAxisRaw("Switch") < 0)
        {
            currentAttack--;
            if (currentAttack < 0)
            {
                currentAttack = attacks.Length - 1;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            attacks[currentAttack].Attack();
        }
    }
    
}
