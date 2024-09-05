using UnityEngine;

public class LevelGeneratorCandyWorld : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject[] _playerSkins;
    [SerializeField] private GameObject _basket;
    [SerializeField] private GameObject[] _spikes;
    [SerializeField] private GameObject[] _branches;
    [SerializeField] private GameObject[] _bushes;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private GameObject[] _platforms;
    [SerializeField] private LocationManager _locationManager;

    private float _screenWidth;
    private float _screenHeight;

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
                break;
            case 2:
                InitializeLevel2();
                break;
            case 3:
                InitializeLevel3();
                break;
            case 4:
                InitializeLevel4();
                break;
            case 5:
                InitializeLevel5();
                break;
            case 6:
                InitializeLevel6();
                break;
            case 7:
                InitializeLevel7();
                break;
            case 8:
                InitializeLevel8();
                break;
            case 9:
                InitializeLevel9();
                break;
            case 10:
                InitializeLevel10();
                break;
            // Добавьте дополнительные уровни здесь
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
        PlaceBranche(0, _screenWidth / 5.5f, _screenHeight / 2f);
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
        PlaceBranche(0, _screenWidth / 5.5f, _screenHeight / 2f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.8f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlaceSpike(0, _screenWidth / 2, _screenHeight / 6.5f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.8f);
        PlaceBranche(1, _screenWidth / 2f, _screenHeight / 10f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 1.9f);
        PlaceSpike(1, _screenWidth / 2, _screenHeight / 5.5f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceBranche(1, _screenWidth / 2f, _screenHeight / 10f);
        PlaceSpike(0, _screenWidth - _screenWidth / 6.5f, _screenHeight - _screenHeight / 4f);
        PlaceSpike(1, _screenWidth / 2, _screenHeight / 5.5f);
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
        PlaceBranche(0, _screenWidth / 2f, _screenHeight / 10f);
        PlaceBush(0, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
        PlaceSpike(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceSpike(1, _screenWidth / 2, _screenHeight / 5.5f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceBush(0, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
        PlaceSpike(0, _screenWidth / 5.5f, _screenHeight / 6.5f);
        PlaceSpike(1, _screenWidth - _screenWidth / 5.5f, _screenHeight - _screenHeight / 1.9f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceBush(0, _screenWidth / 2f, _screenHeight - _screenHeight / 2.6f);
        PlaceBush(1, _screenWidth / 2f, _screenHeight / 8.5f);
        PlaceSpike(0, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlaceSpike(1, _screenWidth / 2, _screenHeight / 6.5f);
        PlaceSpike(2, _screenWidth - _screenWidth / 6.5f, _screenHeight - _screenHeight / 4f);
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
        PlaceBranche(0, _screenWidth - _screenWidth / 5.5f, _screenHeight / 2.5f);
        PlaceBranche(1, _screenWidth / 5.5f, _screenHeight / 9.5f);
        PlaceBush(0, _screenWidth / 2f, _screenHeight - _screenHeight / 2.9f);
        PlaceBush(1, _screenWidth / 2f, _screenHeight / 13.5f);
        PlaceSpike(0, _screenWidth / 2.9f, _screenHeight / 7f);
        PlaceSpike(1, _screenWidth / 2, _screenHeight / 6.5f);
        PlaceSpike(2, _screenWidth - _screenWidth / 6.5f, _screenHeight - _screenHeight / 4f);
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

    private void PlaceBranche(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _coins.Length)
        {
            _branches[index].transform.position = position;
        }
        else
        {
           
        }
    }

    private void PlaceBush(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _coins.Length)
        {
            _bushes[index].transform.position = position;
        }
        else
        {
           
        }
    }
    private void PlaceSpike(int index, float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        if (index >= 0 && index < _coins.Length)
            _spikes[index].transform.position = position;
    }

    private Vector3 GetScreenToWorldPoint(float screenX, float screenY)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 1));
    }
}
