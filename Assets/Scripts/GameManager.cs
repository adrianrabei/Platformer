using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Main;
    [SerializeField] private GameObject Game;
    [SerializeField] private GameObject Death;
    [SerializeField] private GameObject Fail;
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController enemy;

    void Start()
    {
        Time.timeScale = 0;    
    }

    void FixedUpdate()
    {
        
    }

    
    public void Play()
    {
        Main.SetActive(false);
        Game.SetActive(true);
        player.isActive = true;
        enemy.isActive = true;
        Time.timeScale = 1;
    }

    public void Dead()
    {
        Game.SetActive(false);
        Death.SetActive(true);
        player.isActive = false;
    }

    public void Failed()
    {
        Game.SetActive(false);
        Fail.SetActive(true);
        player.isActive = false;
        enemy.isActive = false;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
