using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private UIGameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_gameManager.GetStatusWin())
            {
                _gameManager.Win();
            }
            else
            {
                _gameManager.Lose();
            }
            Destroy(collision.gameObject);
        }
    }
}
