using UnityEngine;

public class InitializatorLocations : MonoBehaviour
{
    [SerializeField] private GameObject[] _locations;
    public GameObject[] _players;
    private int _currentLocation;
    private int _currentPlayer;

    private void Start()
    {
        _currentPlayer = PlayerPrefs.GetInt("SelectedItem", 0);
        _players[_currentPlayer].SetActive(true);
        _currentLocation = Random.Range(0, 3);
        _locations[_currentLocation].SetActive(true);
    }
    public int GetCurrentLocation()
    {
        return _currentLocation;
    }
    public int GetCurrentPlayer()
    {
        return _currentPlayer;
    }
}
