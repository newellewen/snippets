using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private bool mainMenu;
    private bool equipmentSlotMenu;
    private string equipmentFilter;
    
    private List<KeyValuePair<string, GameObject>> playerEquipment;
    private List<KeyValuePair<string, GameObject>> playerInventory;
    private Rect screenBox;

    public float float1;
    public float float2;
    public float float3;
    public float float4;
    public float float5;
    public float float6;
    public float float7;
    public float float8;
    public float float9;
    public float float10;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = true;
        equipmentSlotMenu = false;
        screenBox = new Rect((Screen.width / 2) + 30, (Screen.height / 2) - 100, 200.0f, 200.0f);
        playerEquipment = transform.parent.GetComponent<PlayerController>().playerEquipment;
        playerInventory = transform.parent.GetComponent<PlayerController>().playerInventory;
    }




    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        if (mainMenu)
        {
            GUI.Box(screenBox, "Inventory");

            int count = 1;
            foreach (KeyValuePair<string, GameObject> equipmentSlot in playerEquipment)
            {
                Rect buttonPosition = new Rect(screenBox.x + (screenBox.width / 4), screenBox.y + (30 * count), screenBox.width / 2, screenBox.height / 8);
                if (GUI.Button(buttonPosition, equipmentSlot.Key))
                {
                    equipmentFilter = equipmentSlot.Key;
                    mainMenu = false;
                    equipmentSlotMenu = true;
                }
                count++;
            }
        }

        if (equipmentSlotMenu)
        {
            float viewLength = playerInventory.Count * 40.0f;

            GUI.BeginScrollView(new Rect(550.0f, 150.0f, 150.0f, 100.0f), new Vector2 (0.0f, 0.0f), new Rect(0.0f, 0.0f, 0.0f, viewLength)); ;
            
            for (int i = 0; i < playerInventory.Count; i++)
            {   
                if (playerInventory[i].Key == equipmentFilter)
                {
                    //Rect buttonPosition = new Rect(screenBox.x + (screenBox.width / 4), screenBox.y + (30 * (i + 1)), screenBox.width / 2, screenBox.height / 8);

                    Rect buttonPosition = new Rect(0.0f, i * 30.0f, 100.0f, 20.0f);


                    if (GUI.Button(buttonPosition, playerInventory[i].Value.name))
                    {
                        KeyValuePair<string, GameObject> currentItem = playerEquipment.Where(item => item.Key == equipmentFilter).First();
                        int currentItemIndex = playerEquipment.IndexOf(currentItem);
                        int inventoryItemIndex = playerInventory.IndexOf(playerInventory[i]);

                        playerEquipment[currentItemIndex] = playerInventory[i];
                        playerInventory[inventoryItemIndex] = currentItem;                        
                    }
                }                
            }

            if (Input.mouseScrollDelta.y > 0.0f)
            {
                //GUI.ScrollTo()
            }

            

            GUI.EndScrollView();

            if (GUI.Button(new Rect(screenBox.x + (screenBox.width / 2), screenBox.y + screenBox.height, screenBox.width / 2, screenBox.height / 8), "Back"))
            {
                equipmentSlotMenu = false;
                mainMenu = true;
            }

        }




    }



}
