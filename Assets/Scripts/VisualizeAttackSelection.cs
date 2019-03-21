using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeAttackSelection : MonoBehaviour
{
    [SerializeField] public DoAnAttackScript DoAnAttackScript;
    private SpriteRenderer sprite;
    private AttackScript[] attack;
    private int currentAttack;

    // Start is called before the first frame update
    void Start()
    {
        attack = DoAnAttackScript.attacks;
        currentAttack = DoAnAttackScript.currentAttack;
        sprite = transform.Find("CurrentAttackSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAttack = DoAnAttackScript.currentAttack;
        sprite.sprite = DoAnAttackScript.attackSprites[currentAttack];
    }
}
