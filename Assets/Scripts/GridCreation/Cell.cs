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
    
    public void Click()
    {
        if(isOccupied) return;
        isOccupied = true;
        EventManager.Instance.ONOnCreateCrossOnCell(this);
        Invoke(nameof(CheckForMatch), 0.1f);
        
    }

    private void OnCrossMatched(int x, int y)
    {
        if(!isOccupied) return;
        if(x == (int)coordinates.x && y == (int)coordinates.y)
        {
            isOccupied = false;
        }
    }

    private void CheckForMatch()
    {
        EventManager.Instance.ONOnCheckForMatch(this);
    }

    public void Destroy()
    {
        if(isOccupied)
        {
            transform.GetChild(0).GetComponent<Cross>().OnRebuild();
        }
        Destroy(gameObject);
    }
}
