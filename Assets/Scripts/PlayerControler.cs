using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VibrationManager _vibrationManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collisionSound;
    [SerializeField] private AudioClip _repulsionSound;

    private float _slowdownFactorDuringStick = 0.2f;
    private float _slowdownFactorAfterStick = 0.5f;
    private Rigidbody2D _rb;

    private bool _isAttached;
    private Vector2 _initialVelocity;
    private Vector2 _initialPositionRelative;
    private int _originalLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _originalLayer = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Coconut"))
        {
            _vibrationManager?.Vibrate();
            SlowDown();
            if (_audioSource != null && _collisionSound != null)
            {
                _audioSource.PlayOneShot(_collisionSound);
            }
        }

        if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("Platform"))
        {
            if (_audioSource != null && _repulsionSound != null)
            {
                _audioSource.PlayOneShot(_repulsionSound);
            }
        }

        if (collision.gameObject.CompareTag("StickyDough"))
        {
            AttachToStickyObject(collision.transform, 0.7f, collision.gameObject);
            if (_audioSource != null && _collisionSound != null)
            {
                _audioSource.PlayOneShot(_collisionSound);
            }
            if(Time.deltaTime > 2f)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BottomZone"))
        {
            SlowDown();
        }
    }

    private void SlowDown() => _rb.velocity *= _slowdownFactorAfterStick;

    private void AttachToStickyObject(Transform stickyTransform, float duration, GameObject collision)
    {
        if (!_isAttached)
        {
            _initialVelocity = _rb.velocity;
            _initialPositionRelative = transform.position - stickyTransform.position;
            StartCoroutine(StickForDuration(stickyTransform, duration, collision));
        }
    }

    private IEnumerator StickForDuration(Transform stickyTransform, float duration, GameObject collision)
    {
        _isAttached = true;
        _rb.velocity = _initialVelocity * _slowdownFactorDuringStick;
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (stickyTransform != null)
            {
                transform.position = stickyTransform.position + (Vector3)_initialPositionRelative;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isAttached = false;
        gameObject.layer = _originalLayer;
        _rb.velocity = _initialVelocity * _slowdownFactorAfterStick;

        if (collision != null)
        {
            Destroy(collision);
        }
    }
}
