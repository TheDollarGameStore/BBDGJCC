using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public Constants.Towers holding = Constants.Towers.None; //Which type of tower are you holding on the cursor for placement?

    public static CursorManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
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
    }
}
