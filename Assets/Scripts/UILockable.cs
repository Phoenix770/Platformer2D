using UnityEngine;

public class UILockable : MonoBehaviour
{

    private void OnEnable()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked"; //"Level1Unlocked"
        int _unlocked = PlayerPrefs.GetInt(key, 0);
        if (_unlocked == 0)
            gameObject.SetActive(false);
    }

    [ContextMenu("Clear Unlocked Level")]
    void ClearLevelUnlocked()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked"; //"Level1Unlocked"
        PlayerPrefs.DeleteKey(key);
    }
}
