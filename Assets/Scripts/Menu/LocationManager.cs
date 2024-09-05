using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance { get; private set; }

    [SerializeField] private Button[] _locationButtons;
    [SerializeField] private GameObject[] _locations;
    [SerializeField] private Text _locationText;
    [SerializeField] private GameObject _locationsCanvas;
    [SerializeField] private Text _coinsRecord, _distanceRecord, _starsRecord;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

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
        InitializeButtons();
        LoadGameProgress();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
            InitializeButtons();
        }
    }

    public int GetCurrentLocation()
    {
        return _currentLocation;
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _locationButtons.Length; i++)
        {
            int location = i;
            _locationButtons[i].onClick.RemoveAllListeners();
            _locationButtons[i].onClick.AddListener(() => OnLocationButtonClicked(location));

            _locationButtons[i].interactable = true;
        }
    }

    private void OnLocationButtonClicked(int location)
    {
        _audioSource.PlayOneShot(_buttonSound);
        _currentLocation = location;
        OpenLocation(location);
        SaveGameProgress();
    }

    public void OpenLocation(int location)
    {
        for (int i = 0; i < _locations.Length; i++)
        {
            _locations[i].SetActive(false);
        }

        _locations[location].SetActive(true);
    }
    private void SaveGameProgress()
    {
        PlayerPrefs.SetInt("CurrentLocation", _currentLocation);
        PlayerPrefs.Save();
    }
    private void LoadGameProgress()
    {
        if (_coinsRecord != null)
        {
            _coinsRecord.text = PlayerPrefs.GetInt("RecordCoins", 0).ToString();
            _starsRecord.text = PlayerPrefs.GetInt("RecordStars", 0).ToString();
            _distanceRecord.text = PlayerPrefs.GetInt("RecordDistance", 0).ToString();
        }
        _currentLocation = PlayerPrefs.GetInt("CurrentLocation", 0);
    }
}
