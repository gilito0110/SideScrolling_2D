using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MenuManager : MonoBehaviour
{
    public Animator ani;
    public AsyncOperation async; 

    public void StartGame()
    { 
        StartCoroutine(Chang());
    }
    public void EndGame()
    {
        Application.Quit();
    }

    IEnumerator Chang()
    {
        ani.SetBool("in", true);
        ani.SetBool("out", false);
        yield return new WaitForSeconds(1);
        async=SceneManager.LoadSceneAsync("ingame");
        async.completed += Loaded;

    }

    private void Loaded(AsyncOperation obj)
    {
        if (ani != null)
        {
            ani.SetBool("in", false);
            ani.SetBool("out", true);
        }
    }
}
