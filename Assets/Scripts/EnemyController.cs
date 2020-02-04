using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D Enemy;
    public bool isActive;
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerController player;
    [SerializeField] private float speed;
    [SerializeField] private Transform areaCheck;
    private RaycastHit2D groundInfo;
    private bool isFacingRight;
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

}
