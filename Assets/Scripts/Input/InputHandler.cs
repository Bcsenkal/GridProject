using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class InputHandler : MonoBehaviour
{
    private Camera cam;
    private LayerMask clickableLayer;
    void Start()
    {
        clickableLayer = LayerMask.GetMask("Clickable");
        cam = Camera.main;
        EventManager.Instance.OnMouseDown += MouseDown;
    }

    private void MouseDown(Vector2 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, clickableLayer))
        {
            if (hit.transform.TryGetComponent(out IClickable clickable))
            {
                clickable.Click();
            }
        }
    }
}
