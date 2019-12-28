using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Main;
    [SerializeField] private GameObject Game;
    [SerializeField] private GameObject Death;
    private PlayerController player;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void Play()
    {
        Main.SetActive(false);
        Game.SetActive(true);
        player.isActive = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
