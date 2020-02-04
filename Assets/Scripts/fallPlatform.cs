using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(player.transform.position.x, -20);
    }
}
