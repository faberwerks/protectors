using UnityEngine;

public class TileScript : MonoBehaviour
{
    public AudioClip plantClip;

    private TreeProperties properties;

    private bool isEmpty;

    [SerializeField] private GameObject tree;
    private GameObject treeInLand;
    public GameObject gameManager;

    public Vector3 targetPoint;

    private bool select;

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>().gameObject;
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        select = gameManager.GetComponent<ButtonToggler>().Select;
        if (treeInLand == null)
        {
            isEmpty = true;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && select && isEmpty && Time.timeScale == 1f)
        {
            select = gameManager.GetComponent<ButtonToggler>().Select = false;
            properties = gameManager.GetComponent<ButtonToggler>().toggle.GetComponent<TreeProperties>();
            tree.GetComponent<Tree>().InitialiseAttribute(properties.health, properties.seedValue,
                properties.scoreValue, properties.harvestTime, properties.treeTypeNumber,
                properties.mapleMod, properties.mapleTimer, properties.healCount, properties.healAmount);
            Instantiate(tree, new Vector3(transform.position.x, transform.position.y, -1f),
                Quaternion.identity);
            GameManager.seed -= properties.seedCost;

            treeInLand = Physics2D.Raycast(transform.position, Vector2.up, 0.3f).transform.gameObject;
            // play plant clip
            GetComponent<AudioSource>().PlayOneShot(plantClip);
            isEmpty = false;
        }
    }



}
