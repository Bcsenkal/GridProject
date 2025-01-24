using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Cell : MonoBehaviour, IClickable
{
    private Vector2 coordinates;
    public Vector2 Coordinates{ get => coordinates; set => coordinates = value;}
    private bool isOccupied;

    private void Start() 
    {
        EventManager.Instance.OnCrossMatched += OnCrossMatched;
    }
    
    // Respond to click event and try to create a cross on the cell, on successful creation, check for match
    public void Click()
    {
        if(isOccupied) return;
        isOccupied = true;
        EventManager.Instance.ONOnCreateCrossOnCell(this);
        Invoke(nameof(CheckForMatch), 0.1f);
        
    }

    // Check if the cell is still occupied, if not, set isOccupied to false
    private void OnCrossMatched(int x, int y)
    {
        if(!isOccupied) return;
        if(x == (int)coordinates.x && y == (int)coordinates.y)
        {
            isOccupied = false;
        }
    }

    // Check for a match after a cross is placed, with a slight delay
    private void CheckForMatch()
    {
        EventManager.Instance.ONOnCheckForMatch(this);
    }

    // Destroy the cell, save the cross if it's placed before rebuilding the grid, and unsubscribe from the event
    public void Destroy()
    {
        if(isOccupied)
        {
            transform.GetChild(0).GetComponent<Cross>().OnRebuild();
        }
        EventManager.Instance.OnCrossMatched -= OnCrossMatched;
        Destroy(gameObject);
    }
}
