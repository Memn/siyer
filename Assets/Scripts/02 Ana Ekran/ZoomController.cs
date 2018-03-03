using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [SerializeField] private float _orthoZoomSpeed = 0.01f;
    [SerializeField] private float _moveSpeed = 0.01f;
    [SerializeField] private float _orthoMinSize = 1.0f;
    [SerializeField] private float _orthoMaxSize = 4.5f;
    [SerializeField] private SpriteRenderer _map;

    private Camera _camera;


    private float _leftBound;
    private float _rightBound;
    private float _bottomBound;
    private float _topBound;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        UpdateCameraBounds();
    }

    private void Update()
    {
        switch (Input.touchCount)
        {
            case 1:
            {
                var touchZero = Input.GetTouch(0);
                var delta = touchZero.deltaPosition * touchZero.deltaTime * _moveSpeed * _camera.orthographicSize;
                UpdateCameraPosition(new Vector3(-delta.x, -delta.y, 0));
                break;
            }
            case 2:
            {
                // Store both touches.
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the _camera is orthographic...
                if (!_camera.orthographic) return;
                // ... change the orthographic size based on the change in distance between the touches.
                _camera.orthographicSize += deltaMagnitudeDiff * _orthoZoomSpeed;
                _camera.orthographicSize = Mathf.Max(_camera.orthographicSize, _orthoMinSize);
                _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _orthoMinSize, _orthoMaxSize);

                // Make sure the camera position is in bounds.
                UpdateCameraBounds();
                UpdateCameraPosition(Vector3.zero);
                break;
            }
        }

    }

    private void UpdateCameraPosition(Vector3 add)
    {
        var v3 = _camera.transform.position + add;
        v3.x = Mathf.Clamp(v3.x, _leftBound, _rightBound);
        v3.y = Mathf.Clamp(v3.y, _bottomBound, _topBound);
        _camera.transform.position = v3;
    }

    private void UpdateCameraBounds()
    {
        var camVertExtent = _camera.orthographicSize;
        var camHorzExtent = _camera.aspect * camVertExtent;

        var mapBounds = _map.bounds;
        _leftBound = mapBounds.min.x + camHorzExtent;
        _rightBound = mapBounds.max.x - camHorzExtent;
        _bottomBound = mapBounds.min.y + camVertExtent;
        _topBound = mapBounds.max.y - camVertExtent;
    }
}