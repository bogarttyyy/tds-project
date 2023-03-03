using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField]
    private bool useEdgeScrolling = false;
    [SerializeField]
    private bool useDragPan = true;
    [SerializeField]
    private bool useRotate = true;

    [SerializeField]
    private float dragPanSpeed = -1f;

    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;

    private void Update()
    {
        HandleCameraMovement();
        if (useEdgeScrolling)
        {
            HandleCameraMovementEdgeScrolling();
        }
        if (useDragPan)
        {
            HandleCameraMovementDragPan();
        }
        if (useRotate)
        {
            HandleCameraRotation();
        }
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //NOTE: Use z instead of y in the case of 3d camera ( i.e. inputDir.z)
        if (Input.GetKey(KeyCode.W)) inputDir.y = +1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.y + transform.right * inputDir.x;

        float moveSpeed = 25f;
        transform.position += inputDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        int edgeScrollSize = 20;
        if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.y = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.y = +1f;
    }

    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition; ;

            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.y = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 150f;
        transform.eulerAngles += new Vector3(0, 0, rotateDir * rotateSpeed * Time.deltaTime);
    }
}
