using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameMaster gm;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (gameObject.GetComponent<Unit>().hp <= 0)
        {
            gm.bossDefeated = true;
        }
    }
}
