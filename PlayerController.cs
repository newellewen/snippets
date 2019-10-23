using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public float baseHealth;

    public int strength;
    public int intelligence;
    public int dexterity;
    public int wisdom;
    public float health;
    public float maxHealth;


    private GameObject RightHand;
    private GameObject LeftHand;

    public List<KeyValuePair<string, GameObject>> playerEquipment = new List<KeyValuePair<string, GameObject>>();
    public List<KeyValuePair<string, GameObject>> playerInventory = new List<KeyValuePair<string, GameObject>>();

    private bool inventoryOpen;


    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = baseHealth + strength;
        health = maxHealth;

        inventoryOpen = false;
        rigidBody = GetComponent<Rigidbody2D>();

        playerEquipment.Add(new KeyValuePair<string, GameObject>("Head", null));
        playerEquipment.Add(new KeyValuePair<string, GameObject>("Body", null));
        playerEquipment.Add(new KeyValuePair<string, GameObject>("RightHand", Resources.Load<GameObject>("Prefabs/Weapons/Axe")));
        playerEquipment.Add(new KeyValuePair<string, GameObject>("LeftHand", null));

        playerInventory.Add(new KeyValuePair<string, GameObject>("RightHand", Resources.Load<GameObject>("Prefabs/Weapons/Sword")));
        playerInventory.Add(new KeyValuePair<string, GameObject>("RightHand", Resources.Load<GameObject>("Prefabs/Weapons/Hammer")));

        


    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0) && !inventoryOpen)
        {
            RightHand = playerEquipment.Where(item => item.Key == "RightHand").First().Value;

            GameObject rightHand = Instantiate(RightHand, transform.position, Quaternion.identity);
            rightHand.transform.parent = transform;
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }


    void Move()
    {
        rigidBody.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity += new Vector2(0.0f, moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 flip = transform.localScale;
            flip.x = -1 * Mathf.Abs(flip.x);
            transform.localScale = flip;

            if (inventoryOpen)
            {
                GameObject inventory = GameObject.Find("InventoryController");
                flip = inventory.transform.localScale;
                flip.x = -Mathf.Abs(flip.x);
                inventory.transform.localScale = flip;
            }

            rigidBody.velocity += new Vector2(-moveSpeed, 0.0f);


        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity += new Vector2(0.0f, -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 flip = transform.localScale;
            flip.x = Mathf.Abs(flip.x);
            transform.localScale = flip;

            if (inventoryOpen)
            {
                GameObject inventory = GameObject.Find("InventoryController");
                flip = inventory.transform.localScale;
                flip.x = Mathf.Abs(flip.x);
                inventory.transform.localScale = flip;
            }

            rigidBody.velocity += new Vector2(moveSpeed, 0.0f);
        }
    }

    void Inventory()
    {
        switch (inventoryOpen)
        {
            case true:
                Destroy(GameObject.Find("InventoryController"));
                break;
            case false:
                GameObject playerEquipment = new GameObject();
                playerEquipment.transform.parent = transform;
                playerEquipment.transform.position = transform.position;
                playerEquipment.AddComponent<PlayerEquipment>();
                playerEquipment.name = "InventoryController";
                break;
        }

        inventoryOpen = !inventoryOpen;
    }

}
