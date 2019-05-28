using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{

    public ButtonToggler toggler;

    private Image image;
    private Button button;

    public void Start()
    {
        toggler = GameManager.FindObjectOfType<GameManager>().GetComponent<ButtonToggler>();

        image = toggler.toggle.GetComponent<Image>();

        button = this.gameObject.GetComponent<Button>();
    }

    public void OnClick()
    {
        if (GameManager.seed >= this.GetComponent<TreeProperties>().seedCost)
        {
            image.color = Color.white;
            toggler.toggle = button;
            toggler.Select = true;
        }
    }
}
