using UnityEngine;
using UnityEngine.UI;

public class ScrollRectController : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;

    private Vector2 _previousTouchPosition;
    private bool _isDragging;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _previousTouchPosition = touch.position;
                _isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                Vector2 delta = touch.position - _previousTouchPosition;
                float normalizedDelta = delta.y / _scrollRect.viewport.rect.height;
                _scrollRect.verticalNormalizedPosition -= normalizedDelta;
                _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition);

                _previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isDragging = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _previousTouchPosition = Input.mousePosition;
            _isDragging = true;
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - _previousTouchPosition;
            float normalizedDelta = delta.y / _scrollRect.viewport.rect.height;
            _scrollRect.verticalNormalizedPosition -= normalizedDelta;
            _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition);

            _previousTouchPosition = currentMousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }
}
