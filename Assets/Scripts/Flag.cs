using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) return;
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        PlayerPrefs.SetInt(_sceneName + "Unlocked", 1);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_sceneName);
    }
}
