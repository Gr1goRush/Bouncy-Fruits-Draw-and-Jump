using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelGenerator : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;

    [SerializeField] private InitializatorLocations _initializatorLocations;

    [SerializeField] private List<GameObject> _platformPrefabs; // Список платформ
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private List<GameObject> _obstaclePrefabs;
    [SerializeField] private GameObject _fallingObject;
    [SerializeField] private GameObject _zoneRemoving;

    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private float _cameraThreshold = 2f;
    [SerializeField] private float _edgeOffset = 1f;
    [SerializeField] private float _objectRadius = 0.5f;
    [SerializeField] private int _objectsPerSpawn = 3;

    private float _lastSpawnY;
    private float _screenHalfWidth;
    private List<GameObject> _activeObjects = new List<GameObject>();

    private float _totalDistanceTraveled = 0f;
    public bool _isWin = true;

    private float _timeForSpawn = 4f;
    private float _currentTime;

    private float _screenWidth;
    private float _screenHeight;

    void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        player = _initializatorLocations._players[_initializatorLocations.GetCurrentPlayer()];

        _lastSpawnY = player.transform.position.y;
        _screenHalfWidth = mainCamera.aspect * mainCamera.orthographicSize;
        SpawnInitialObjects();
        RemoveOffscreenObjects();
    }

    private void Update()
    {
        if (_fallingObject != null)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeForSpawn)
            {
                SpawnFallingObject(Random.Range(1, 4));
            }
        }
    }

    public void MoveCamera()
    {
        if (player.transform.position.y - mainCamera.transform.position.y < _cameraThreshold)
        {
            mainCamera.transform.position += new Vector3(0, -_spawnDistance * 1.4f, 0);
            SpawnInitialObjects();
            RemoveOffscreenObjects();
            UpdateDistanceTraveled();
            _timeForSpawn -= 0.01f;
        }
    }

    public void SaveProgressDistance()
    {
        if (_totalDistanceTraveled > PlayerPrefs.GetInt("RecordDistance"))
        {
            PlayerPrefs.SetInt("RecordDistance", (int)_totalDistanceTraveled);
            PlayerPrefs.Save();
        }
    }

    void SpawnInitialObjects()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        float minX = -_screenHalfWidth + _edgeOffset;
        float maxX = _screenHalfWidth - _edgeOffset;

        for (int i = 0; i < _objectsPerSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), _lastSpawnY - _spawnDistance * i / _objectsPerSpawn, 0);
            if (!IsPositionOccupied(spawnPosition))
            {
                // Спавн платформы из списка
                GameObject newPlatform = Instantiate(_platformPrefabs[Random.Range(0, _platformPrefabs.Count)], spawnPosition, Quaternion.identity);
                _activeObjects.Add(newPlatform);

                if (Random.value < 0.3f)
                {
                    Vector3 coinPosition = new Vector3(spawnPosition.x + Random.Range(-1f, 1f), spawnPosition.y + 1f, 0);
                    if (!IsPositionOccupied(coinPosition))
                    {
                        GameObject newCoin = Instantiate(_coinPrefab, coinPosition, Quaternion.identity);
                        _activeObjects.Add(newCoin);
                    }
                }
                else if (Random.value < 0.6f) // Увеличена вероятность для звезды
                {
                    Vector3 starPosition = new Vector3(spawnPosition.x + Random.Range(-1f, 1f), spawnPosition.y + 1f, 0);
                    if (!IsPositionOccupied(starPosition))
                    {
                        GameObject newStar = Instantiate(_starPrefab, starPosition, Quaternion.identity);
                        _activeObjects.Add(newStar);
                    }
                }

                // Отдельная проверка для спавна препятствий
                if (Random.value < 0.8f)
                {
                    SpawnObstacle(spawnPosition);
                }
            }
        }

        _lastSpawnY -= _spawnDistance;
    }

    void SpawnObstacle(Vector3 spawnPosition)
    {
        GameObject randomObstaclePrefab = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)];
        Vector3 obstaclePosition = new Vector3(spawnPosition.x, spawnPosition.y + 1f, 0);
        if (!IsPositionOccupied(obstaclePosition))
        {
            GameObject newObstacle = Instantiate(randomObstaclePrefab, obstaclePosition, Quaternion.identity);
            _activeObjects.Add(newObstacle);
        }
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _objectRadius);
        return colliders.Length > 0;
    }

    void RemoveOffscreenObjects()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in _activeObjects)
        {
            if (obj == null || obj.transform.position.y > _zoneRemoving.transform.position.y)
            {
                objectsToRemove.Add(obj);
            }
        }

        foreach (GameObject obj in objectsToRemove)
        {
            if (obj != null)
            {
                _activeObjects.Remove(obj);
                Destroy(obj);
            }
        }
    }

    private void SpawnFallingObject(int indexPalmTree)
    {
        if (indexPalmTree == 1)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth - _screenWidth / 4f, _screenHeight - _screenHeight / 3.5f);
            GameObject coconutSpawned = Instantiate(_fallingObject);
            coconutSpawned.transform.position = position;
        }
        if (indexPalmTree == 2)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth - _screenWidth / 2.2f, _screenHeight - _screenHeight / 2.3f);
            GameObject coconutSpawned = Instantiate(_fallingObject);
            coconutSpawned.transform.position = position;
        }
        if (indexPalmTree == 3)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth / 4.2f, _screenHeight - _screenHeight / 2f);
            GameObject coconutSpawned = Instantiate(_fallingObject);
            coconutSpawned.transform.position = position;
        }
        _currentTime = 0;
    }

    private Vector3 GetScreenToWorldPoint(float screenX, float screenY)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 1));
    }

    void UpdateDistanceTraveled()
    {
        _totalDistanceTraveled = Mathf.Abs(player.transform.position.y);
    }
}
