using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void NavigateToGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
     public void NavigateToHome() {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
