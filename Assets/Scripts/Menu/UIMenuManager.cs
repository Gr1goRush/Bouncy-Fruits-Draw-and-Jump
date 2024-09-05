using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private GameObject _shopButtons;
    [SerializeField] private GameObject _locationsCanvas;
    [SerializeField] private GameObject[] _locationsButtons;
    [SerializeField] private GameObject _gamemodesCanvas;
    [SerializeField] private GameObject _gamemodeButtons;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _settingsButtons;
    [SerializeField] private GameObject[] _locationsCanvases;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;
        SetCanvasActive(_menuCanvas, true);
        SetCanvasActive(_shopCanvas, false);
        SetCanvasActive(_locationsCanvas, false);
        SetCanvasActive(_gamemodesCanvas, false);
        SetCanvasActive(_settingsCanvas, false);

        foreach (var canvas in _locationsCanvases)
        {
            SetCanvasActive(canvas, false);
        }
    }

    private void SetCanvasActive(GameObject canvas, bool isActive)
    {
        canvas.SetActive(isActive);
    }

    private void AnimateButtons(GameObject buttons)
    {
        foreach (Transform button in buttons.transform)
        {
            button.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);
        }
    }

    private void AnimateButtonArray(GameObject[] buttonsArray)
    {
        foreach (GameObject button in buttonsArray)
        {
            button.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);
        }
    }

    public void EndlessMode()
    {
        SceneManager.LoadScene(5);
    }

    public void Gamemodes()
    {
        SetCanvasActive(_menuCanvas, false);
        SetCanvasActive(_gamemodesCanvas, true);
        AnimateButtons(_gamemodeButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void Locations()
    {
        SetCanvasActive(_gamemodesCanvas, false);
        SetCanvasActive(_locationsCanvas, true);
        AnimateButtonArray(_locationsButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void Shop()
    {
        SetCanvasActive(_menuCanvas, false);
        SetCanvasActive(_shopCanvas, true);
        AnimateButtons(_shopButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void Settings()
    {
        SetCanvasActive(_menuCanvas, false);
        SetCanvasActive(_settingsCanvas, true);
        AnimateButtons(_settingsButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void BackToMenu()
    {
        SetCanvasActive(_menuCanvas, true);
        SetCanvasActive(_gamemodesCanvas, false);
        SetCanvasActive(_shopCanvas, false);
        SetCanvasActive(_settingsCanvas, false);
        AnimateButtons(_menuButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void BackToGamemodes()
    {
        SetCanvasActive(_locationsCanvas, false);
        SetCanvasActive(_gamemodesCanvas, true);
        AnimateButtons(_gamemodeButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void BackToLocations()
    {
        SetCanvasActive(_locationsCanvas, true);
        foreach (var canvas in _locationsCanvases)
        {
            SetCanvasActive(canvas, false);
        }
        AnimateButtonArray(_locationsButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }
}
