using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;

    internal void Unlock()
    {
        _onUnlocked.Invoke();
    }
}
