using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    private Vector2 coordinates = new Vector2(-5,-5);
    public Vector2 Coordinates{ get => coordinates; set => coordinates = value;}
    private Transform poolParent;
    public Transform PoolParent{ get => poolParent; set => poolParent = value;}

    private void Start() 
    {
        Managers.EventManager.Instance.OnCrossMatched += OnCrossMatched;
    }

    private void OnCrossMatched(int x, int y)
    {
        if(x == (int)coordinates.x && y == (int)coordinates.y)
        {
            Managers.EventManager.Instance.ONOnDisableCross(this);
            if(coordinates.x < 0 || coordinates.y < 0) return;
            coordinates = new Vector2(-5,-5);

            Managers.EventManager.Instance.ONOnCrossMatched(x+1, y);
            Managers.EventManager.Instance.ONOnCrossMatched(x-1, y);
            Managers.EventManager.Instance.ONOnCrossMatched(x, y+1);
            Managers.EventManager.Instance.ONOnCrossMatched(x, y-1);
        }
    }

    public void OnRebuild()
    {
        transform.SetParent(poolParent);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        coordinates = new Vector2(-5,-5);
    }
}
