using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int strength;
    public int magic;
    public int skill;
    public int defense;
    public int resistence;
    public int stamina;
    public int luck;
    public int speed;

    public int hp;
    public int lvl;
    public int exp;

    public int attack;
    public int hit;
    public int crit;
    public int avoid;

    public int attackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;

    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    public int playerNumber;

    public GameObject weaponIcon;

    public Weapon equippedWeapon;

    public Sprite mugshot;

    private void Start()
    {
        if (name == "Leah")
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        if (equippedWeapon.magical == true)
        {
            attack = magic + equippedWeapon.might;
        }
        else
        {
            attack = strength + equippedWeapon.might;

        }

        hit = equippedWeapon.hit + (skill * 3 + luck) / 2;
        crit = equippedWeapon.crit + skill / 2;
        avoid = (speed * 3 + luck) / 2;
        attackRange = equippedWeapon.attackRange;

        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            gm.ToggleStatsPanel(this);
        }
    }

    private void OnMouseDown()
    {
        ResetWeaponIcons();

        if(selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if(playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }

                selected = true;
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();
        if(gm.selectedUnit != null)
        {
            if(gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                gm.selectedUnit.Attack(unit);
            }
        }
    }

    void Attack(Unit enemy)
    {
        hasAttacked = true;

        FirstAttack(enemy);

        if (enemy.hp <= 0)
        {
            gm.RemoveStatsPanel(enemy);
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }
        else if(hp <= 0)
        {
            gm.RemoveStatsPanel(this);
            Destroy(this.gameObject);
            GetWalkableTiles();
        }

        CounterAttack(enemy);

        if (enemy.hp <= 0)
        {
            gm.RemoveStatsPanel(enemy);
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }
        else if (hp <= 0)
        {
            gm.RemoveStatsPanel(this);
            Destroy(this.gameObject);
            GetWalkableTiles();
        }
    }
    void FirstAttack(Unit enemy)
    {
        int allyHit = Random.Range(1, 101);
        int allyCrit = Random.Range(1, 101);

        if (allyHit < hit)
        {
            //You Hit
            if (allyCrit < crit)
            {
                //You Crit
                if (equippedWeapon.magical == true)
                {
                    enemy.hp = enemy.hp - ((attack * 3) - enemy.resistence);
                }
                else
                {
                    enemy.hp = enemy.hp - ((attack * 3) - enemy.defense);
                }
            }
            //You Don't Crit
            else if (allyCrit > crit)
            {
                if (equippedWeapon.magical == true)
                {
                    enemy.hp = enemy.hp - ((attack) - enemy.resistence);
                }
                else
                {
                    enemy.hp = enemy.hp - ((attack) - enemy.defense);
                }
            }
        }
        else if (allyHit > hit)
        {
            //You miss
            return;
        }
    }

    void CounterAttack(Unit enemy)
    {
        int enemyHit = Random.Range(1, 101);
        int enemyCrit = Random.Range(1, 101);

        if (enemyHit < enemy.hit)
        {
            //You Hit
            if (enemyCrit < crit)
            {
                //You Crit
                if (equippedWeapon.magical == true)
                {
                    hp = hp - ((enemy.attack * 3) - resistence);
                }
                else
                {
                    hp = hp - ((enemy.attack * 3) - defense);
                }
            }
            //You Don't Crit
            else if (enemyCrit > enemy.crit)
            {
                if (equippedWeapon.magical == true)
                {
                    hp = hp - ((enemy.attack) - resistence);
                }
                else
                {
                    hp = hp - ((enemy.attack) - defense);
                }
            }
        }
        else if (enemyHit > enemy.hit)
        {
            //You miss
            return;
        }
    }


    void GetWalkableTiles()
    {
        if(hasMoved == true)
        {
            return;
        }
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }

    void GetEnemies()
    {
        enemiesInRange.Clear();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= attackRange)
            {
                if(unit.playerNumber != gm.playerTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }
            }
        }
    }

    public void ResetWeaponIcons()
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.weaponIcon.SetActive(false);
        }
    }

    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        while(transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasMoved = true;
        ResetWeaponIcons();
        GetEnemies();
    }
}
