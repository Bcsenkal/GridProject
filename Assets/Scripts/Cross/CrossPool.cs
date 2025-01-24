using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class CrossPool : MonoBehaviour
{
    [SerializeField]private Cross crossPrefab;
    private int poolSize = 50;
    private List<Cross> crosses = new List<Cross>();
    private void Awake() 
    {
        CreatePool();
    }

    void Start()
    {
        EventManager.Instance.OnCreateCrossOnCell += CreateCrossOnCell;
        EventManager.Instance.OnDisableCross += DisableCross;
        
    }

    // Create a pool of crosses, and set them to inactive
    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Cross cross = Instantiate(crossPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Cross>();
            cross.gameObject.SetActive(false);
            crosses.Add(cross);
            cross.PoolParent = transform;
        }
    }

    // Create an available cross on the cell
    private void CreateCrossOnCell(Cell cell)
    {
        var cross = crosses.Find(c => !c.gameObject.activeInHierarchy);
        if(cross == null)
        {
            cross = Instantiate(crossPrefab, Vector3.zero, Quaternion.identity, transform);
        }
        cross.Coordinates = cell.Coordinates;
        cross.transform.SetParent(cell.transform);
        cross.transform.localScale = Vector3.one;
        cross.transform.localPosition = Vector3.back * 0.1f;
        cross.gameObject.SetActive(true);

    }

    // Disable the cross
    private void DisableCross(Cross cross)
    {
        cross.gameObject.SetActive(false);
        cross.transform.SetParent(transform);
        cross.transform.localPosition = Vector3.zero;
    }
}
