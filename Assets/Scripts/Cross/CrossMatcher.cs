using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class CrossMatcher : MonoBehaviour
{
    private int[,] crossMatrix;
    void Start()
    {
        EventManager.Instance.OnCreateCrossMatrix += CreateMatrix;
        EventManager.Instance.OnCheckForMatch += OnCrossPlacement;
        EventManager.Instance.OnDisableCross += OnCrossDisable;
    }

    // On cross placement, check for match
    private void OnCrossPlacement(Cell cell)
    {
        crossMatrix[(int)cell.Coordinates.x, (int)cell.Coordinates.y] = 1;
        CheckForMatch();
    }

    // On cross disable, set the matrix value to 0
    private void OnCrossDisable(Cross cross)
    {
        crossMatrix[(int)cross.Coordinates.x, (int)cross.Coordinates.y] = 0;
    }

    // Check for a match
    private void CheckForMatch()
    {
        var gridSize = crossMatrix.GetLength(0);
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if(crossMatrix[i, j] == 1)
                {
                    if(i + 1 < gridSize && crossMatrix[i + 1, j] == 1)
                    {
                        if(i + 2 < gridSize && crossMatrix[i + 2, j] == 1 || i - 1 >= 0 && crossMatrix[i - 1, j] == 1 || j + 1 < gridSize && crossMatrix[i, j + 1] == 1 || j - 1 >= 0 && crossMatrix[i, j - 1] == 1)
                        {
                            EventManager.Instance.ONOnCrossMatched(i,j);
                            return;
                        }
                    }
                    else if(j + 1 < gridSize && crossMatrix[i, j + 1] == 1)
                    {
                        if(j + 2 < gridSize && crossMatrix[i, j + 2] == 1 || j - 1 >= 0 && crossMatrix[i, j - 1] == 1 || i + 1 < gridSize && crossMatrix[i + 1, j] == 1 || i - 1 >= 0 && crossMatrix[i - 1, j] == 1)
                        {
                            EventManager.Instance.ONOnCrossMatched(i,j);
                            return;
                        }
                    }
                    else if(i - 1 >= 0 && crossMatrix[i - 1, j] == 1)
                    {
                        if(i - 2 >= 0 && crossMatrix[i - 2, j] == 1 || i + 1 < gridSize && crossMatrix[i + 1, j] == 1 || j + 1 < gridSize && crossMatrix[i, j + 1] == 1 || j - 1 >= 0 && crossMatrix[i, j - 1] == 1)
                        {
                            EventManager.Instance.ONOnCrossMatched(i,j);
                            return;
                        }
                    }
                    else if(j - 1 >= 0 && crossMatrix[i, j - 1] == 1)
                    {
                        if(j - 2 >= 0 && crossMatrix[i, j - 2] == 1 || j + 1 < gridSize && crossMatrix[i, j + 1] == 1 || i + 1 < gridSize && crossMatrix[i + 1, j] == 1 || i - 1 >= 0 && crossMatrix[i - 1, j] == 1)
                        {
                            EventManager.Instance.ONOnCrossMatched(i,j);
                            return;
                        }
                    }

                }
            }
        }
    }

    // Create the cross matrix
    private void CreateMatrix(int gridSize)
    {
        crossMatrix = new int[gridSize, gridSize];
    }
}
