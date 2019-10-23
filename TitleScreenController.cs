using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    GameObject[] choices;
    AsyncOperation asyncLoadLevel;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        choices = GameObject.FindGameObjectsWithTag("PlayerChoice");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            foreach (GameObject choice in choices)
            {
                
                if (choice.GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
                {                    
                    choice.tag = "Player";
                    DontDestroyOnLoad(choice);
                    StartCoroutine(LoadLevel("Scene1"));
                }
            }
        }
    }

    IEnumerator LoadLevel(string scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.GetSceneByName(scene);
        
        asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        //SceneManager.MoveGameObjectToScene(playerChoice, newScene);
        //SceneManager.SetActiveScene(newScene); 
        SceneManager.UnloadSceneAsync(currentScene);
        
    }


}
