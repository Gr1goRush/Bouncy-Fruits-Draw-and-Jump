using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private bool _isVibrate;

    private void Awake()
    {
        _isVibrate = GetStateVibration();
    }
    public void Vibrate()
    {
        if (_isVibrate)
        {
            Handheld.Vibrate();
        }
    }
    private bool GetStateVibration()
    {
        return PlayerPrefs.GetInt("VibrationInfo", 1) == 1;
    }
}
