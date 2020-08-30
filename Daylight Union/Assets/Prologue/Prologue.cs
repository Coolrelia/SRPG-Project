using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public GameObject roland;
    public GameObject simon;
    public GameObject edgar;
    public GameObject leah;

    GameMaster gm;

    void Start()
    {       
        gm = FindObjectOfType<GameMaster>();
    }

    void Update()
    { 
        WinCondition();

        if(gm.turnNumber == 2)
        {
            leah.SetActive(true);
        }
    }

    void WinCondition()
    {
        if(gm.turnNumber == 5)
        {
            Debug.Log("You Win");
            //sets boss to undefeated
            //takes you to victory screen
        }
        else if(gm.bossDefeated == true)
        {
            Debug.Log("You Win");
            gm.bossDefeated = false;
            //sets boss to undefeated
            //takes you to victory screen
        }
    }
}
