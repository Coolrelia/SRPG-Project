using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool bossDefeated = false;

    public Unit selectedUnit;

    public int playerTurn = 1;
    public int turnNumber = 1;
    public Text turnNumberText;

    public GameObject selectedUnitSquare;

    public SpriteRenderer rend;

    public Sprite player;
    public Sprite enemy;

    public GameObject statsPanel;
    public Unit viewedUnit;

    public Text nameText;
    public Text healthText;
    public Text attackText;
    public Text critText;
    public Text avoidText;
    public Text rangeText;

    public Image mugshot;

    public void ResetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    public void ToggleStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit) == false)
        {
            statsPanel.SetActive(true);
            viewedUnit = unit;
            UpdateStatsPanel();
        }
        else
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    public void UpdateStatsPanel()
    {
        if(viewedUnit != null)
        {
            nameText.text = viewedUnit.unitName;
            healthText.text = "HP: " + viewedUnit.hp.ToString();
            attackText.text = "Attack: " + viewedUnit.attack.ToString();
            critText.text = "Crit: " + viewedUnit.crit.ToString();
            avoidText.text = "Avoid: " + viewedUnit.avoid.ToString();
            rangeText.text = "Range: " + viewedUnit.attackRange.ToString();
            mugshot.sprite = viewedUnit.mugshot;
        }
    }

    public void RemoveStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        if(selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

    public void EndTurn()
    {
        if(playerTurn == 1)
        {
            playerTurn = 2;
            rend.sprite = enemy;
        }
        else if(playerTurn == 2)
        {
            turnNumber++;
            playerTurn = 1;
            rend.sprite = player;
        }

        if(selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.weaponIcon.SetActive(false);
            unit.hasAttacked = false;
        }
    }
}
