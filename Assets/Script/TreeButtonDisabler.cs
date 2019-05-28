using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to enable and disable the tree buttons dynamically.
/// </summary>
public class TreeButtonDisabler : MonoBehaviour
{
    private Button button;
    private float seedCost;

    // Use this for initialization
    private void Start()
    {
        button = GetComponent<Button>();

        seedCost = GetComponent<TreeProperties>().seedCost;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.seed < seedCost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
