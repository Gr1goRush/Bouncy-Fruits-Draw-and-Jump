using UnityEngine;

public class LevelGeneratorTropicalIsland : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject[] _playerSkins;
    [SerializeField] private GameObject _basket;
    [SerializeField] private GameObject[] _spikes;
    [SerializeField] private GameObject _coconut;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private GameObject[] _platforms;
    [SerializeField] private LocationManager _locationManager;

    private float _screenWidth;
    private float _screenHeight;

    private float _timeForSpawnCoconut;
    private float _currentTime;

    private int _currentLevel;


    private void Start()
    {
        _player = _playerSkins[PlayerPrefs.GetInt("SelectedItem", 0)];
        _player.SetActive(true);

        int currentLocation = _locationManager.GetCurrentLocation();
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel_" + currentLocation, 1);        

        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        GenerateLevel();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeForSpawnCoconut)
        {
            PlaceCoconut(Random.Range(1, 4));
        }
    }

    private void GenerateLevel()
    {
        InitializeBasket();
        InitializePlayer();
        InitializeLevel();
    }

    private void InitializeBasket()
    {
        Vector3 basketPosition = GetScreenToWorldPoint(_screenWidth - _screenWidth / 5.5f, _screenHeight / 10);
        _basket.transform.position = basketPosition;
    }

    private void InitializePlayer()
    {
        Vector3 playerPosition = GetScreenToWorldPoint(_screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 4.5f);
        _player.transform.position = playerPosition;
    }

    private void InitializeLevel()
    {
        switch (_currentLevel)
        {
            case 1:
                InitializeLevel1();
                _timeForSpawnCoconut = 4f;
                break;
            case 2:
                InitializeLevel2();
                _timeForSpawnCoconut = 3.8f;
                break;
            case 3:
                InitializeLevel3();
                _timeForSpawnCoconut = 3.6f;
                break;
            case 4:
                InitializeLevel4();
                _timeForSpawnCoconut = 3.4f;
                break;
            case 5:
                InitializeLevel5();
                _timeForSpawnCoconut = 3.2f;
                break;
            case 6:
                InitializeLevel6();
                _timeForSpawnCoconut = 3f;
                break;
            case 7:
                InitializeLevel7();
                _timeForSpawnCoconut = 2.8f;
                break;
            case 8:
                InitializeLevel8();
                _timeForSpawnCoconut = 2.6f;
                break;
            case 9:
                InitializeLevel9();
                _timeForSpawnCoconut = 2.4f;
                break;
            case 10:
                InitializeLevel10();
                _timeForSpawnCoconut = 2.2f;
                break;
            default:
                break;
        }
    }

    private void InitializeLevel1()
    {
        PlacePlayer(_screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 4.5f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 4f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 3f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 5f);
        PlaceCoin(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceSpike(0, _screenWidth / 2, _screenHeight / 6.5f);

    }

    private void InitializeLevel2()
    {
        PlacePlayer(_screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 4.5f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 4f);
        PlaceStar(1, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 5f);
        PlaceCoin(0, _screenWidth / 5.5f, _screenHeight / 1.65f);
        PlaceSpike(0, _screenWidth / 2, _screenHeight / 6.5f);
    }

    private void InitializeLevel3()
    {
        PlacePlayer(_screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 4.5f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 4.5f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 4f);
        PlaceStar(1, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3f);
        PlaceCoin(0, _screenWidth / 5.5f, _screenHeight / 1.65f);
        PlaceSpike(0, _screenWidth / 2, _screenHeight / 6.5f);
    }

    private void InitializeLevel4()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 6f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 1.9f);
        PlaceSpike(1, _screenWidth / 2f, _screenHeight / 10f);
    }

    private void InitializeLevel5()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 6f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight - _screenHeight / 1.7f);
        PlaceCoin(0, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 1.9f);
        PlaceSpike(1, _screenWidth / 2f, _screenHeight / 10f);
    }

    private void InitializeLevel6()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight / 2f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlacePlatform(2, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 6f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3.2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceSpike(1, _screenWidth / 2f, _screenHeight / 10f);
    }
    private void InitializeLevel7()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight / 2f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlacePlatform(2, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlacePlatform(3, _screenWidth / 2f, _screenHeight - _screenHeight / 2.3f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 6f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3.2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2f);
        PlaceCoin(1, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2.8f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceSpike(1, _screenWidth / 2f, _screenHeight / 10f);
        PlaceSpike(2, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
    }
    private void InitializeLevel8()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight / 2f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlacePlatform(2, _screenWidth / 2f, _screenHeight / 14f);
        PlacePlatform(3, _screenWidth / 2f, _screenHeight - _screenHeight / 2.3f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 2f, _screenHeight / 7f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3.2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2f);
        PlaceCoin(1, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2.8f);
        PlaceSpike(0, _screenWidth / 5.5f, _screenHeight / 6.5f);
        PlaceSpike(1, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceSpike(2, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
    }

    private void InitializeLevel9()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight / 2f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlacePlatform(2, _screenWidth / 2f, _screenHeight / 14f);
        PlacePlatform(3, _screenWidth / 2f, _screenHeight - _screenHeight / 2.3f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 5f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3.2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceCoin(1, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2.8f);
        PlaceSpike(0, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlaceSpike(1, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceSpike(2, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
    }

    private void InitializeLevel10()
    {
        PlacePlayer(_screenWidth / 5.5f, _screenHeight / 2f);
        PlacePlatform(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2.3f);
        PlacePlatform(1, _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlacePlatform(2, _screenWidth / 2f, _screenHeight / 50f);
        PlacePlatform(3, _screenWidth / 2f, _screenHeight - _screenHeight / 2.5f);
        PlaceStar(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 3f);
        PlaceStar(1, _screenWidth / 5.5f, _screenHeight / 5f);
        PlaceStar(2, _screenWidth / 5.5f, _screenHeight / 3.2f);
        PlaceCoin(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 2f);
        PlaceCoin(1, _screenWidth - _screenWidth / 3f, _screenHeight - _screenHeight / 2.8f);
        PlaceCoin(2, _screenWidth / 2f, _screenHeight / 6.5f);
        PlaceSpike(0, _screenWidth / 2.9f, _screenHeight / 7f);
        PlaceSpike(1, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceSpike(2, _screenWidth / 2f, _screenHeight - _screenHeight / 2.8f);
    }

    private void PlacePlatform(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _platforms.Length)
        {
            _platforms[index].transform.position = position;
        }
        else
        {
            
        }
    }

    private void PlaceStar(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _stars.Length)
        {
            _stars[index].transform.position = position;
        }
        else
        {
            
        }
    }

    private void PlaceCoin(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _coins.Length)
        {
            _coins[index].transform.position = position;
        }
        else
        {
            
        }
    }
    private void PlacePlayer(float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        _player.transform.position = position;
    }

    private void PlaceSpike(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _coins.Length)
        {
            _spikes[index].transform.position = position;
        }
        else
        {
            
        }
    }

    private void PlaceCoconut(int index)
    {
        if (index == 1)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth - _screenWidth / 4f, _screenHeight - _screenHeight / 3.5f);
            GameObject coconutSpawned = Instantiate(_coconut);
            coconutSpawned.transform.position = position;
        }
        if (index == 2)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth - _screenWidth / 2.2f, _screenHeight - _screenHeight / 2.3f);
            GameObject coconutSpawned = Instantiate(_coconut);
            coconutSpawned.transform.position = position;
        }
        if (index == 3)
        {
            Vector3 position = GetScreenToWorldPoint(_screenWidth / 4.2f, _screenHeight - _screenHeight / 2f);
            GameObject coconutSpawned = Instantiate(_coconut);
            coconutSpawned.transform.position = position;
        }
        _currentTime = 0;
    }

    private Vector3 GetScreenToWorldPoint(float screenX, float screenY)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 1));
    }
}
