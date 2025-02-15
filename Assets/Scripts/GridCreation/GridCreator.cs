using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Managers;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int gridSize;
    private bool isRuntime;


    void Start()
    {
        isRuntime = true;
        CreateGrid();
        Managers.EventManager.Instance.OnRebuildButtonClick += RebuildGrid;
        Invoke(nameof(SendGridInfo),0.1f);
    }
    
    // Added a button to the inspector to create a grid
    [Button("Create Grid",buttonSize:ButtonSizes.Large), GUIColor(0f,0.8f,0)]
    // Create a grid of cells with the given size, it is totally flexible and can be used in any aspect ratio, well, almost
    private void CreateGrid()
    {
        var cam = Camera.main;
        float screenWidth = cam.orthographicSize * cam.aspect * 2;
        float screenHeight = cam.orthographicSize * 2;
        float cellSize = Mathf.Min(screenWidth, screenHeight) / gridSize;
        ClearCurrentGrid();

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float posX = (i - gridSize / 2) * cellSize + cam.transform.position.x;
                float posY = (j - gridSize / 2) * cellSize + cam.transform.position.y;

                Cell cell = Instantiate(gridPrefab, new Vector3(posX, posY, 0), Quaternion.identity).GetComponent<Cell>();
                cell.Coordinates = new Vector2(i, j);
                cell.transform.localScale = new Vector3(cellSize * 0.95f, cellSize * 0.95f, 1);
                if(gridSize % 2 == 0) cell.transform.position += new Vector3(cellSize / 2, cellSize / 2, 0);
                cell.transform.parent = transform;
            }
        }
    }

    // Rebuild the grid with the given size
    private void RebuildGrid(int size)
    {
        gridSize = size;
        CreateGrid();
        SendGridInfo();
    }

    // Clear the current grid
    private void ClearCurrentGrid()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            if(isRuntime)
            {
                transform.GetChild(i).GetComponent<Cell>().Destroy();
            } 
            else
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }

    // Send the grid size to the CrossMatcher
    private void SendGridInfo()
    {
        EventManager.Instance.ONOnCreateCrossMatrix(gridSize);
    }
}
