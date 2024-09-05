using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private float _scaleCollider = 0.2f;

    private readonly List<Vector2> _points = new List<Vector2>();

    private float _time = 0;
    private int _hpLine = 3;

    public delegate void LineDestroyedHandler();
    public event LineDestroyedHandler OnLineDestroyed;

    void Start()
    {
        _collider.transform.position -= transform.position;
        transform.position = Vector2.zero;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 5 || _hpLine <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _hpLine--;
            if (_time > 5)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        OnLineDestroyed?.Invoke();
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        _points.Add(pos);

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);

        _collider.points = _points.ToArray();
        _collider.edgeRadius = _scaleCollider;
    }

    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0) return true;

        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public Vector2 GetLastPoint()
    {
        if (_points.Count > 0)
        {
            return _points[_points.Count - 1];
        }
        return Vector2.zero;
    }
}
