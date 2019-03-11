using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float DetectionRadius = 5f;
    [SerializeField] private float CooldownTimer = 3f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;
    const float k_GroundedRadius = .5f;
    private Transform ownPosition;
    private Transform playerPosition;
    public FrogJump frogJump;
    private float relativePosition;
    [SerializeField] private bool DontMoveIfNotGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        ownPosition = gameObject.transform;
        InvokeRepeating("Detect", 0f, CooldownTimer);
        relativePosition = (playerPosition.position.x + playerPosition.position.y) - (ownPosition.position.x + ownPosition.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Detect()
    {
        bool m_Grounded = false;
        if (DontMoveIfNotGrounded)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }
        }
        if ((m_Grounded && DontMoveIfNotGrounded || !DontMoveIfNotGrounded) && Mathf.Abs(Mathf.Abs(playerPosition.position.x + playerPosition.position.y) - Mathf.Abs(ownPosition.position.x + ownPosition.position.y)) <= DetectionRadius)
        {
            frogJump.JumpYouIdiot(playerPosition.position.x - ownPosition.position.x >= 0);
        }
    }
}
