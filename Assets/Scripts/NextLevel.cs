using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;

    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelIndex < 3)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            gameManager.YouWin();
        }
    }
}
