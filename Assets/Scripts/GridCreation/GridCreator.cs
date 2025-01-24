using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int gridSize;


    void Start()
    {
        
    }
    
    [Button]
    private void CreateGrid()
    {
        var cam = Camera.main;
        float screenWidth = cam.orthographicSize * cam.aspect * 2;
        float screenHeight = cam.orthographicSize * 2;
        float cellSize = Mathf.Min(screenWidth, screenHeight) / gridSize;
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float posX = (i - gridSize / 2) * cellSize + cam.transform.position.x;
                float posY = (j - gridSize / 2) * cellSize + cam.transform.position.y;

                GameObject grid = Instantiate(gridPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
                grid.transform.localScale = new Vector3(cellSize * 0.95f, cellSize * 0.95f, 1);
                if(gridSize % 2 == 0) grid.transform.position += new Vector3(cellSize / 2, cellSize / 2, 0);
                grid.transform.parent = transform;
            }
        }
        
        
    }
}
