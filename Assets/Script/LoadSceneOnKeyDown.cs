using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A component to load a scene when a key is pressed.
/// </summary>
public class LoadSceneOnKeyDown : MonoBehaviour
{
    public string targetScene;
    public KeyCode keyToPress;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
