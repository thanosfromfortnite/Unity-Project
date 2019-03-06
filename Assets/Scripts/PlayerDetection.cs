using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float DetectionRadius = 5f;
    private Transform ownPosition;
    private Transform playerPosition;
    public FrogJump frogJump;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        ownPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((playerPosition.position.x + playerPosition.position.y) - (ownPosition.position.x + ownPosition.position.y)) <= DetectionRadius)
        {
            frogJump.JumpYouIdiot();
            Debug.Log("gdfsdf");
        }
    }
}
