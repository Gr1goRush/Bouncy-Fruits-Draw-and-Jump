using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _blueStars;
    [SerializeField] private GameObject[] _yellowStars;

    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private GameObject _hiToPlay;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _buttonSource;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _obstacleSound;
    [SerializeField] private AudioClip _collisionSound;
    [SerializeField] private AudioClip _starSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;

    [SerializeField] private Text _textMoney;
    [SerializeField] private Text _textEnergy;

    [SerializeField] private Text _textStars;

    [SerializeField] private int _moneyForCoin;

    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private EndlessLevelGenerator[] _endlessLevelGenerator;
    [SerializeField] private InitializatorLocations _randomLocation;

    private int _playerMoney;
    private int _currentStar;
    private int _currentLevel;
    private int _currentCoinsRecord;
    private int _energyCapacity = 5;

    private bool _isFirstHiToPlay;
    private bool _isPause;

    private void Start()
    {
        DOTween.Clear(true);
        InitializeHiToPlay();
        Time.timeScale = 1.0f;
        _playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        if (_textStars != null)
            _textStars.text = _currentStar.ToString();
        _textMoney.text = _playerMoney.ToString();
        _textEnergy.text = _energyCapacity.ToString();
    }

    public void HideHiToPlay()
    {
        _hiToPlay.SetActive(false);
        PlayerPrefs.SetInt("HiToPlay", 1);
    }

    public void PlayCoinSound()
    {
        _audioSource.PlayOneShot(_coinSound);
    }
    public void PlayRepulsionSound()
    {
        _audioSource.PlayOneShot(_collisionSound);
    }

    public void PlaySpikeSound()
    {
        _audioSource.PlayOneShot(_obstacleSound);
    }

    public void PlayStarSound()
    {
        _audioSource.PlayOneShot(_starSound);
    }

    public void NextLevel()
    {
        _audioSource.PlayOneShot(_buttonSource);
        StartCoroutine(NextSceneWithDelay(0.12f));

    }

    public void Win()
    {
        Time.timeScale = 0f;
        _winCanvas.SetActive(true);
        if (_currentLevel == 10)
        {
            _nextLevelButton.SetActive(false);
        }
        _audioSource.PlayOneShot(_winSound);
        CompleteLevel();
        SaveMoneyProgress();
        _musicSource.Stop();
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        _loseCanvas.SetActive(true);
        _audioSource.PlayOneShot(_loseSound);
        if (_endlessLevelGenerator != null && _endlessLevelGenerator.Length > 0)
        {
            _endlessLevelGenerator[_randomLocation.GetCurrentLocation()]._isWin = false;
        }
        _musicSource.Stop();
    }
    public void Pause()
    {
        _audioSource.PlayOneShot(_buttonSource);
        _isPause = !_isPause;
        if (_isPause)
        {
            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseCanvas.SetActive(false);
        }
    }

    public void CollectStar()
    {
        _currentStar++;
        if (_blueStars.Length > 0)
            _blueStars[_currentStar - 1].SetActive(false);
        if (_yellowStars.Length > 0)
            _yellowStars[_currentStar - 1].SetActive(true);
        if (_textStars != null)
            _textStars.text = _currentStar.ToString();
    }

    public void IncreaseMoney()
    {
        _playerMoney += _moneyForCoin;
        _currentCoinsRecord += _moneyForCoin;
        _textMoney.text = _playerMoney.ToString();
    }

    public void DecreaseEnergy()
    {
        if (_energyCapacity > 0)
        {
            _energyCapacity--;
        }
        if (_energyCapacity == 0)
        {
            Lose();
        }
        _textEnergy.text = _energyCapacity.ToString();
    }

    public void Restart()
    {
        _audioSource.PlayOneShot(_buttonSource);
        Time.timeScale = 1.0f;
        StartCoroutine(RestartSceneWithDelay(0.12f));
    }
    public void Menu()
    {
        _audioSource.PlayOneShot(_buttonSource);
        StartCoroutine(MenuSceneWithDelay(0.12f));
    }

    public bool GetStatusWin()
    {
        return _currentStar > 2;
    }

    public void SaveMoneyProgress()
    {
        PlayerPrefs.SetInt("PlayerMoney", _playerMoney);
        PlayerPrefs.Save();
    }

    public void SaveStarsRecord()
    {
        if (_currentStar > PlayerPrefs.GetInt("RecordStars"))
        {
            PlayerPrefs.SetInt("RecordStars", (int)_currentStar);
            PlayerPrefs.Save();
        }
    }
    public void SaveCoinsRecord()
    {
        if (_currentCoinsRecord > PlayerPrefs.GetInt("RecordCoins"))
        {
            PlayerPrefs.SetInt("RecordCoins", (int)_currentCoinsRecord);
            PlayerPrefs.Save();
        }
    }

    public int GetCurrentStarCount()
    {
        return _currentStar;
    }

    public void CompleteLevel()
    {
        _levelManager.CompleteLevel(_currentStar);
    }

    private void InitializeHiToPlay()
    {
        _isFirstHiToPlay = PlayerPrefs.GetInt("HiToPlay", 0) == 0 ? true : false;
        _hiToPlay.SetActive(_isFirstHiToPlay);
    }

    private IEnumerator RestartSceneWithDelay(float delay)
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator MenuSceneWithDelay(float delay)
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(delay);
        if (_endlessLevelGenerator != null && _endlessLevelGenerator.Length > 0)
        {
            if (_endlessLevelGenerator[_randomLocation.GetCurrentLocation()]._isWin)
            {
                _endlessLevelGenerator[_randomLocation.GetCurrentLocation()].SaveProgressDistance();
                SaveCoinsRecord();
                SaveStarsRecord();
            }
        }
        SceneManager.LoadScene(1);
    }
    private IEnumerator NextSceneWithDelay(float delay)
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(delay);
        _levelManager.NextLevel();
    }
}
