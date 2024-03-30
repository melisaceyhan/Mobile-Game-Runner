using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
}
