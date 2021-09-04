using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    private SpriteRenderer sr;
    public Constants.Towers holding = Constants.Towers.None; //Which type of tower are you holding on the cursor for placement?

    public List<Sprite> towerSprites;

    public static CursorManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        transform.position = new Vector3(transform.position.x, transform.position.y, 10);

        if (Mouse.current.leftButton.wasPressedThisFrame && holding != Constants.Towers.None)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Tile"))
            {
                if (hit.collider.transform.GetComponent<GridTile>().PlaceTower(holding))
                {
                    holding = Constants.Towers.None;
                }        
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && holding != Constants.Towers.None)
        {
            holding = Constants.Towers.None;
            GameManager.instance.UpdateDiscipline(100); //TODO Calculate discipline refund based on tower
        }

        if (holding != Constants.Towers.None)
        {
            sr.sprite = towerSprites[(int)holding - 1];
        }
        else
        {
            sr.sprite = null;
        }
    }
}
