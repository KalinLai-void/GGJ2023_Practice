using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTest : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
