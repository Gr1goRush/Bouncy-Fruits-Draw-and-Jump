using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField] private Button[] _itemButtons;
    [SerializeField] private Text _selectedItemText; 
    [SerializeField] private GameObject _shopCanvas; 
    [SerializeField] private int[] _itemPrices; 
    [SerializeField] private Text _playerMoneyText;

    [SerializeField] private Sprite _lockedSprite; 
    [SerializeField] private Sprite _unlockedSprite; 
    [SerializeField] private Sprite _selectedSprite;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _purchaseSound;
    [SerializeField] private AudioClip _blockSound;
    [SerializeField] private AudioClip _selectSound;

    private int _selectedItem; 
    private int _playerMoney; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        LoadShopProgress();
        _playerMoneyText.text = _playerMoney.ToString();
        InitializeButtons();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
            _playerMoney = 0;
            PlayerPrefs.SetInt("ItemPurchased_0", 1);
            SaveShopProgress();
            InitializeButtons();

        }
        
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _itemButtons.Length; i++)
        {
            int itemIndex = i;
            Text buttonText = _itemButtons[i].GetComponentInChildren<Text>();
            Image buttonImage = _itemButtons[i].transform.GetChild(1).GetComponent<Image>();

            _itemButtons[i].onClick.RemoveAllListeners();
            _itemButtons[i].onClick.AddListener(() => OnItemButtonClicked(itemIndex));

            if (PlayerPrefs.GetInt("ItemPurchased_" + itemIndex, 0) == 1)
            {
                if (itemIndex == _selectedItem)
                {
                    buttonImage.sprite = _selectedSprite;
                    buttonText.text = null;
                }
                else
                {
                    buttonImage.sprite = _unlockedSprite;
                    buttonText.text = null;
                }
            }
            else
            {
                buttonImage.sprite = _lockedSprite;
                buttonText.text = _itemPrices[i].ToString();
            }
        }
    }

    private void OnItemButtonClicked(int itemIndex)
    {
        if (PlayerPrefs.GetInt("ItemPurchased_" + itemIndex, 0) == 1)
        {
            _selectedItem = itemIndex;
            SaveShopProgress();
            InitializeButtons();
            _audioSource.PlayOneShot(_selectSound);
        }
        else if (_playerMoney >= _itemPrices[itemIndex])
        {
            _playerMoney -= _itemPrices[itemIndex];
            PlayerPrefs.SetInt("ItemPurchased_" + itemIndex, 1);
            SaveShopProgress();
            InitializeButtons();
            _audioSource.PlayOneShot(_purchaseSound);
        }
        else
        {
            _audioSource.PlayOneShot(_blockSound);
        }
        _playerMoneyText.text = _playerMoney.ToString();
    }

    private void SaveShopProgress()
    {
        PlayerPrefs.SetInt("SelectedItem", _selectedItem);
        PlayerPrefs.SetInt("PlayerMoney", _playerMoney);
        PlayerPrefs.Save();
    }

    private void LoadShopProgress()
    {
        _selectedItem = PlayerPrefs.GetInt("SelectedItem", 0);
        PlayerPrefs.SetInt("ItemPurchased_0", 1);
    }
}
