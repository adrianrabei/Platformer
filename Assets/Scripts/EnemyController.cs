using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D Enemy;
    public bool isActive;
    [SerializeField] private GameManager manager;
    [SerializeField] private float speed;
    [SerializeField] private Transform areaCheck;
    [SerializeField] private Transform playerCheck1;
    [SerializeField] private Transform playerCheck2;
    private RaycastHit2D groundInfo;
    private RaycastHit2D playerInfo1;
    private RaycastHit2D playerInfo2;
    [SerializeField] private float distance = 5;
    [SerializeField] private float viewDistance = 10;
    private bool isFacingRight;
    private Ray2D ray;
    void Start()
    {
        Enemy = GetComponent<Rigidbody2D>();
        isActive = false;
        isFacingRight = false;
    }

    void FixedUpdate()
    {
        if(isActive)
        {
            Enemy.transform.Translate(Vector2.left * speed * Time.deltaTime);
            Patroling();
            PlayerFinding();
        }
    }

    private void Patroling()
    {
        groundInfo = Physics2D.Raycast(areaCheck.position, Vector2.down);

        if(groundInfo.transform.gameObject.tag == "Ground")
        {
            if(isFacingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isFacingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isFacingRight = true;
            }
        }
    }

    private void PlayerFinding()
    {
        if(isFacingRight)
        {
            playerInfo1 = Physics2D.Raycast(playerCheck1.position, Vector2.right);
            playerInfo2 = Physics2D.Raycast(playerCheck2.position, Vector2.right);
        }
        else
        {
            playerInfo1 = Physics2D.Raycast(playerCheck1.position, Vector2.left);
            playerInfo2 = Physics2D.Raycast(playerCheck2.position, Vector2.left);
        }

        /*if ((playerInfo1 == true && playerInfo1.transform.gameObject.tag == "Player") || (playerInfo2 == true && playerInfo2.transform.gameObject.tag == "Player"))
        {
            manager.Failed();
        }*/
    }
}
