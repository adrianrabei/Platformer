using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject view;
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerController player;
    private bool caught;
    private bool canSee;
    void Start()
    {
        caught = false;
        canSee = true;
    }

    void FixedUpdate()
    {
        PlayerFinding();
    }    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player")
        {
            canSee = false;
        }
        else caught = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canSee = true;
        caught = false;
    }

    private void PlayerFinding()
    {
        if(canSee && caught && !player.isInvisible)
        {
            manager.Failed();
        }
    }
}
