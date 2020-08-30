using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float hoverAmount;

    public LayerMask obstacleLayer;

    public SpriteRenderer rend;
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public bool isWalkable;
    GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    public bool IsClear()
    {
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if(obstacle != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Highlight()
    {
        rend.sprite = highlightedSprite;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.sprite = normalSprite;
        isWalkable = false;
    }

    private void OnMouseDown()
    {
        if(isWalkable == true)
        {
            if(isWalkable && gm.selectedUnit != null)
            {
                gm.selectedUnit.Move(this.transform.position);
            }
        }

    }
}
