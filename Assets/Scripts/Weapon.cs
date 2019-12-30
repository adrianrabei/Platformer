using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void FixedUpdate()
    {
        WeaponHandle();
    }

    private void WeaponHandle()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x + (-0.68f), player.transform.position.y + (-1.47f), 0);
    }
}
