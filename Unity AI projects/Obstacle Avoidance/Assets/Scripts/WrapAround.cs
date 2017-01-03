using UnityEngine;
using System.Collections;

public class WrapAround : MonoBehaviour
{

    private Camera mainCamera;
    private bool isWrappingAroundX;
    private bool isWrappingAroundY;
    private Vector3 viewPortPosition;
    private Vector3 newPositionAfterWrap;

    private void Start()
    {
        mainCamera = Camera.main;
        isWrappingAroundX = isWrappingAroundY = false;
    }

    private void OnBecameInvisible()
    {
        if (!isWrappingAroundX && !isWrappingAroundY && mainCamera != null)
        {
            viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);
            newPositionAfterWrap = transform.position;
            if ((viewPortPosition.x > 1.0f || viewPortPosition.x < 0.0f))
            {
                newPositionAfterWrap.x = -newPositionAfterWrap.x;
                isWrappingAroundX = true;
            }

            if ((viewPortPosition.y > 1.0f || viewPortPosition.y < 0.0f))
            {
                newPositionAfterWrap.y = -newPositionAfterWrap.y;
                isWrappingAroundY = true;
            }
            transform.position = newPositionAfterWrap;
        }
    }

    private void OnBecameVisible()
    {
        isWrappingAroundX = isWrappingAroundY = false;
    }
}
