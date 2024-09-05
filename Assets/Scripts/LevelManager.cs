using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private Text _levelText;
    [SerializeField] private GameObject _levelsCanvas;
    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _unlockedSprite;
    [SerializeField] private Sprite _completedSprite;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

    [SerializeField] private int _scene;

    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private LoadScreen _loadScreen;

    private int _lastLevel;
    private int _currentLevel;
    private int _currentLocation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _currentLocation = PlayerPrefs.GetInt("CurrentLocation", 0);
        LoadGameProgress();
        InitializeButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
            LoadGameProgress();
            InitializeButtons();
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public int GetLastLevel()
    {
        return _lastLevel;
    }

    public void CompleteLevel(int starsCollected)
    {
        if (_currentLevel == _lastLevel)
        {
            _lastLevel++;
        }
        SaveGameProgress(starsCollected);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int level = i + 1;
            Text buttonText = _levelButtons[i].GetComponentInChildren<Text>();

            _levelButtons[i].onClick.RemoveAllListeners();
            _levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(level));

            if (level <= _lastLevel)
            {
                _levelButtons[i].interactable = true;
                buttonText.text = level.ToString();
                _levelButtons[i].GetComponent<Image>().sprite = (level < _lastLevel) ? _completedSprite : _unlockedSprite;
                UpdateStarsForLevel(_levelButtons[i], level);
            }
            else
            {
                _levelButtons[i].interactable = false;
                buttonText.text = string.Empty;
                _levelButtons[i].GetComponent<Image>().sprite = _lockedSprite;
            }
        }
    }

    private void UpdateStarsForLevel(Button levelButton, int level)
    {
        int starsCollected = PlayerPrefs.GetInt("LevelStars_" + _currentLocation + "_" + level, 0);
        GameObject starsContainer = levelButton.transform.Find("Stars").gameObject;

        for (int i = 0; i < starsContainer.transform.childCount; i++)
        {
            starsContainer.transform.GetChild(i).gameObject.SetActive(i < starsCollected);
        }
    }

    private void OnLevelButtonClicked(int level)
    {
        if (level <= _lastLevel)
        {
            _currentLevel = level;
            PlayerPrefs.SetInt("CurrentLevel_" + _currentLocation, _currentLevel);
            _audioSource.PlayOneShot(_buttonSound);
            _loadScreen.Load();           
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(_scene);
        PlayerPrefs.SetInt("CurrentLevel_" + _currentLocation, _currentLevel);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        if (_currentLevel < _lastLevel)
        {
            _currentLevel++;
            OpenLevel();
            _audioSource.PlayOneShot(_buttonSound);
            
        }
    }

    private void SaveGameProgress(int starsCollected)
    {
        PlayerPrefs.SetInt("LastLevel_" + _currentLocation, _lastLevel);
        PlayerPrefs.SetInt("CurrentLevel_" + _currentLocation, _currentLevel);
        if(starsCollected > PlayerPrefs.GetInt("LevelStars_" + _currentLocation + "_" + _currentLevel, 0))
        PlayerPrefs.SetInt("LevelStars_" + _currentLocation + "_" + _currentLevel, starsCollected);
        PlayerPrefs.Save();
    }

    private void LoadGameProgress()
    {
        _lastLevel = PlayerPrefs.GetInt("LastLevel_" + _currentLocation, 1);
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel_" + _currentLocation, 1);
    }
}
