using UnityEngine;
using UnityEngine.UI;

public class TreeCooldownHandler : MonoBehaviour {

    private Button treeButton;

    private Image cooldownImage;

    [SerializeField]private bool isCooldown;

    private float cooldown;
    [SerializeField] private float cooldownTimer = 0;

	// Use this for initialization
	void Start () {
        isCooldown = false;
        cooldown = GetComponent<TreeProperties>().cooldown;
        treeButton = GetComponent<Button>();
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.transform.parent != null)
            {
                cooldownImage = image;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.A)) { SetCooldownTimer(); }
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount -= Time.deltaTime / cooldown;
            if(cooldownTimer <= 0)
            {
                cooldownTimer = 0;
                isCooldown = false;
                treeButton.interactable = true;
                cooldownImage.fillAmount = 0;
            }
        }
	}

    public void SetCooldownTimer()
    {
        //Debug.Log("start cooldown");
        cooldownTimer = cooldown;
        treeButton.interactable = false;
        cooldownImage.fillAmount = 1;
        isCooldown = true;
    }
}
