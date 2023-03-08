using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button retry;
    public Button quit;
    public Transform player;

    void Start () 
    {
		retry.onClick.AddListener(Retry);
        quit.onClick.AddListener(Quit);
    }

    /// <summary>
    /// exits the application
    /// </summary>
    private void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarts the level, at least that's what it's supposed to do
    /// </summary>
    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
