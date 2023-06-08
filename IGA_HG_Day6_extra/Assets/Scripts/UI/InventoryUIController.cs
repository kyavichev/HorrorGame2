using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public Vector2Int size;
    public InventoryCellView cellPrefab;
    public GameObject gridParent;

    public Texture2D emptyCellTexture;

    public List<InventoryCellView> cells;

    private Vector2Int selectedCellPosition;


    // Start is called before the first frame update
    void Start()
    {
        cells = new List<InventoryCellView>();

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                InventoryCellView newCell = Instantiate<InventoryCellView>(cellPrefab);
                newCell.transform.SetParent(gridParent.transform, false);
                newCell.gridPosition = new Vector2Int(y, x);

                cells.Add(newCell);
            }
        }

        selectedCellPosition = new Vector2Int(-1, -1);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject hero = GameManager.GetInstance().hero;
        if(hero == null)
        {
            return;
        }

        Collector collector = hero.GetComponent<Collector>();
        if(collector == null)
        {
            return;
        }

        //LoadoutController loadoutController = collector.GetComponent<LoadoutController>();

        int selectedIndex = GridPositionToIndex(selectedCellPosition);

        for(int i = 0; i < cells.Count; i++)
        {
            InventoryCellView cell = cells[i];

            if (i < collector.collectedCollectables.Count)
            {
                Collectable collectable = collector.collectedCollectables[i];
                InventoryItemData itemData = collectable.GetComponent<InventoryItemData>();

                cell.SetName(itemData.collectable.name);
                cell.SetIconTexture(itemData.texture);
                cell.SetButtonActive(true);

                cell.useAction = () =>
                {
                    collectable.Use(collector);

                    // This is how you might want to show selected item
                    /*
                    if(collectable is WeaponCollectable)
                    {
                        selectedCellPosition = cell.gridPosition;
                    }
                    */
                };

                // This would be useful for weapons
                /*
                if (loadoutController.leftHandItem == collectable || loadoutController.rightHandItem == collectable)
                {
                    cell.SetButtonActive(false);
                }
                else
                {
                    cell.SetButtonActive(true);
                }
                */
            }
            else
            {
                cell.SetIconTexture(emptyCellTexture);
                cell.SetButtonActive(false);

                cell.useAction = () => { };
            }

            if(i == selectedIndex)
            {
                cell.SetSelected(true);
            }
            else
            {
                cell.SetSelected(false);
            }
        }
    }


    private int GridPositionToIndex(Vector2Int gridPosition)
    {
        if(gridPosition.x == -1 && gridPosition.y == -1)
        {
            return -1;
        }

        int i = gridPosition.x + gridPosition.y * size.x;
        return i;
    }


    public void MoveSelection(Vector2Int dir)
    {
        selectedCellPosition.x += dir.x;
        if (selectedCellPosition.x >= size.x)
        {
            selectedCellPosition.x -= size.x;
        }
        if (selectedCellPosition.x < 0)
        {
            selectedCellPosition.x += size.x;
        }

        selectedCellPosition.y += dir.y;
        if (selectedCellPosition.y >= size.y)
        {
            selectedCellPosition.y -= size.y;
        }
        if (selectedCellPosition.y < 0)
        {
            selectedCellPosition.y += size.y;
        }
    }
}
