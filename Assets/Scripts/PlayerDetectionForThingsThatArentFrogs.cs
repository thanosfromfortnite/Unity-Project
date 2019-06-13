using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionForThingsThatArentFrogs : MonoBehaviour
{
    [SerializeField] private float DetectionRadius = 5f;
    [SerializeField] private float CooldownTimer = 3f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform WallCheckLeft;
    [SerializeField] private Transform WallCheckRight;
    [SerializeField] private Animator animator;
    
    const float k_GroundedRadius = .5f;
    private Transform ownPosition;
    private Transform playerPosition;
    public EnemyMove enemyMovement;
    private float relativePosition;
    [SerializeField] private bool DontMoveIfNotGrounded = false;
    private bool wasMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        ownPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void Detect()
    {
        bool obstacle = false;
        bool m_Grounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        if ((m_Grounded && DontMoveIfNotGrounded || !DontMoveIfNotGrounded) && Mathf.Abs(Mathf.Abs(playerPosition.position.x + playerPosition.position.y) - Mathf.Abs(ownPosition.position.x + ownPosition.position.y)) <= DetectionRadius)
        {
            Collider2D[] otherColliders = Physics2D.OverlapCircleAll(WallCheckLeft.position, k_GroundedRadius, m_WhatIsGround);
            if (!wasMoving)
            {
                animator.SetBool("moving", true);
                wasMoving = true;
            }
            for (int i = 0; i < otherColliders.Length; i ++)
            {
                if (otherColliders[i].gameObject != gameObject)
                {
                    obstacle = true;
                }
            }
            Collider2D[] rightColliders = Physics2D.OverlapCircleAll(WallCheckRight.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < rightColliders.Length; i ++)
            {
                if (rightColliders[i].gameObject != gameObject)
                {
                    obstacle = true;
                }
            }
            
            if (obstacle && m_Grounded)
            {
                enemyMovement.JumpYouIdiot(playerPosition.position.x - ownPosition.position.x >= 0);
            }
            else
            {
                enemyMovement.MoveYouIdiot(playerPosition.position.x - ownPosition.position.x >= 0);
            }
        }
        else if (wasMoving)
        {
            animator.SetBool("moving", false);
            wasMoving = false;
        }
    }
}
