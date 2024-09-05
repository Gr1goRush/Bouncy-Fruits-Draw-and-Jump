using UnityEngine;

public class Spike : MonoBehaviour
{
    private UIGameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIGameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameManager.DecreaseEnergy();
            _gameManager.PlaySpikeSound();
        }
    }
}
