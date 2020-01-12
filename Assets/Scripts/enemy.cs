using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D Enemy;
    [SerializeField] private float speed;
    [SerializeField] private Transform areaCheck;
    private RaycastHit2D groundInfo;
    [SerializeField] private float distance = 5;
    private bool isFacingRight;
    void Start()
    {
        Enemy = GetComponent<Rigidbody2D>();
        isFacingRight = false;
    }

    void FixedUpdate()
    {
        Enemy.transform.Translate(Vector2.left * speed * Time.deltaTime);
        Patroling();
    }

    private void Patroling()
    {
        groundInfo = Physics2D.Raycast(areaCheck.position, Vector2.down, distance);

        if(groundInfo.collider.tag == "Ground")
        {
            Debug.Log("bum");
            /*if(isFacingRight)
            {
                isFacingRight = !isFacingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }*/
        }
    }
}
