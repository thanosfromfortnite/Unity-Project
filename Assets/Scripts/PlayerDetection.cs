using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float DetectionRadius = 5f;
    [SerializeField] private int CooldownTimer = 2;
    private Transform ownPosition;
    private Transform playerPosition;
    public FrogJump frogJump;
    private float relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        ownPosition = gameObject.transform;
        InvokeRepeating("Detect", 1.0f, 1.0f);
        relativePosition = (playerPosition.position.x + playerPosition.position.y) - (ownPosition.position.x + ownPosition.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Detect()
    {
        if (Mathf.Abs(relativePosition) <= DetectionRadius)
        {
            frogJump.JumpYouIdiot(relativePosition >= 0);
            Debug.Log("gdfsdf");
        }
    }
}
