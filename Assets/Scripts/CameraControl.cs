using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Properties")]
    public float zoomAmount = 0.5f;
    public float moveSpeed = 1.5f;
    public bool acceptInput = true;

    //[Header("Keybinds")]
    //public KeyCode toggleProjection = KeyCode.P;

    private float mouseWheel;
    private float horizontal;
    private float vertical;

    // Input polling 
    void Update()
    {
        if (!acceptInput) return;

        mouseWheel = Input.mouseScrollDelta.y;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //if (Input.GetKeyDown(toggleProjection))
        //{
        //    projection = !projection;
        //    GetComponent<Camera>().orthographic = projection;
        //}
    }

    // Perform camera movement here
    void FixedUpdate()
    {
        if (mouseWheel != 0)
        {
            //if (projection)
            //{
            // GetComponent<Camera>().orthographicSize += zoomAmount * -mouseWheel * 0.5f;
            Camera cam = GetComponent<Camera>();
            float oldSize = cam.orthographicSize;
            cam.orthographicSize += -mouseWheel * (Mathf.Max(cam.orthographicSize / 10, 0.5f));
            if (cam.orthographicSize <= 0) cam.orthographicSize = oldSize;
            //}
            //else
            //{
            //    Vector3 pos = transform.position;
            //    pos.y += zoomAmount * -mouseWheel;
            //    transform.position = pos;
            //}
        }

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 pos = transform.position;
            pos.x += horizontal * moveSpeed;
            pos.y += vertical * moveSpeed;
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.R)) Reset();
    }

    public void ResetInput()
    {
        vertical = 0;
        horizontal = 0;
        mouseWheel = 0;
    }

    public void Reset()
    {
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = 1000;
        Vector3 newPos = new Vector3(0, 0, -10);
        cam.transform.position = newPos;
    }
}
