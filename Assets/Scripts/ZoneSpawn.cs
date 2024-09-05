using UnityEngine;

public class ZoneSpawn : MonoBehaviour
{
    [SerializeField] private EndlessLevelGenerator[] _endlessLevelGenerator;
    [SerializeField] private InitializatorLocations _initializatorLocations;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _endlessLevelGenerator[_initializatorLocations.GetCurrentLocation()].MoveCamera();
        }
    }
}
