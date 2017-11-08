using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera cam;
    // Use this for initialization
    private static readonly float PanSpeed = 10f;
    private Vector3 lastPanPosition;
    private static readonly float mouseZoomSpeed = 10f;

    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, 18f };
    private static readonly float PanBoundsPosX = -10f;
    private static readonly float PanBoundsNegX = 5f;
    private static readonly float PanBoundsPosY = -18f;
    private static readonly float PanBoundsNegY = 18f;
    private static readonly float ZoomBoundsMin = 10f;
    private static readonly float ZoomBoundsMax = 100f;

    private LoginScreen _loginSw;
    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Start ()
    {
       
        _loginSw = GameObject.Find("LoginControl").GetComponent<LoginScreen>();
    }
    void CameraZoom(float offset, float speed)
    {
        //
        if (offset == 0)
        {
            return;
        }
        else
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBoundsMin, ZoomBoundsMax);
            //_loginSw.sw.WriteLine("Camera Zoom selected");
        }
        
    }

    void PanCamera(Vector3 newPanPosition)
    {
       // _loginSw.sw.WriteLine("Panning Object.");

        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed,0 );
        transform.Translate(move, Space.World);
        Vector3 pos = transform.position;
    
        pos.x = Mathf.Clamp(transform.position.x, PanBoundsPosX, PanBoundsNegX);
        pos.y = Mathf.Clamp(transform.position.y, PanBoundsPosY, PanBoundsNegY);
        transform.position = pos;
        lastPanPosition = newPanPosition;
    }


    // Update is called once per frame
    void Update ()
    {
 
        {
            if (Input.GetMouseButtonDown(1))
            {
                lastPanPosition = Input.mousePosition;
                _loginSw.sw.WriteLine("Panning Object.");
            }
            else if (Input.GetMouseButton(1))
            {

                PanCamera(Input.mousePosition);
            }
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            CameraZoom(scroll, mouseZoomSpeed);
        }
        
    }
}
