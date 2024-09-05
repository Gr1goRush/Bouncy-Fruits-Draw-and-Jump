using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private AudioSource _audioSource;

    public const float RESOLUTION = .1f;
    [SerializeField] private int maxLines = 5; // Maximum number of lines allowed
    [SerializeField] private float maxLineLength = 10f; // Maximum length of a line
    [SerializeField] private float pointInterval = 0.1f; // Interval in seconds between adding points

    private Line _currentLine;
    private int _currentLineCount = 0; // Current number of lines created
    private float _currentLineLength = 0f; // Current length of the line
    private float _timeSinceLastPoint = 0f; // Time since the last point was added

    private GameObject _linesContainer; // Container for all lines

    void Start()
    {
        _cam = Camera.main;
        if (_cam == null)
        {
            Debug.LogError("Main camera not found");
            return;
        }

        // Create a container to hold all lines
        _linesContainer = new GameObject("LinesContainer");
    }

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        if (mouseScreenPos.x >= 0 && mouseScreenPos.x <= Screen.width &&
            mouseScreenPos.y >= 0 && mouseScreenPos.y <= Screen.height)
        {
            try
            {
                Vector2 mousePos = _cam.ScreenToWorldPoint(mouseScreenPos);

                if (Input.GetMouseButtonDown(0) && _currentLineCount < maxLines)
                {
                    CreateNewLine(mousePos);
                }

                if (Input.GetMouseButton(0))
                {
                    DrawCurrentLine(mousePos);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    StopDrawSound();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Caught exception: " + ex.Message);
            }
        }
        else
        {
            StopDrawSound();
        }
    }

    private void CreateNewLine(Vector2 mousePos)
    {
        _currentLine = Instantiate(_linePrefab, _linesContainer.transform);
        _currentLineCount++;
        _currentLineLength = 0f; // Reset the line length
        _currentLine.OnLineDestroyed += HandleLineDestroyed; // Subscribe to the event
        PlayDrawSound();
        _timeSinceLastPoint = 0f; // Reset the time since the last point
        _currentLine.SetPosition(mousePos); // Set the first position of the line
    }

    private void DrawCurrentLine(Vector2 mousePos)
    {
        if (_currentLine != null)
        {
            _timeSinceLastPoint += Time.deltaTime * 5;
            if (_timeSinceLastPoint >= pointInterval && _currentLineLength < maxLineLength)
            {
                float distanceToLastPoint = Vector2.Distance(_currentLine.GetLastPoint(), mousePos);
                if (_currentLineLength + distanceToLastPoint <= maxLineLength)
                {
                    _currentLine.SetPosition(mousePos);
                    _currentLineLength += distanceToLastPoint; // Update the line length
                    _timeSinceLastPoint = 0f; // Reset the time since the last point
                }
            }
        }
    }

    private void PlayDrawSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void StopDrawSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private void HandleLineDestroyed()
    {
        _currentLineCount--;
    }
}
