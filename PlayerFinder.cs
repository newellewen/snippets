using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFinder : MonoBehaviour
{
    public string sceneName;
    public string[] tags;

    private bool foundPlayer;
    private bool foundTags;
    
    // Start is called before the first frame update
    void Start()
    {
        foundPlayer = false;
        foundTags = false;
    }

    // Update is called once per frame
    void Update()
    {    
        GameObject playerChoice = GameObject.FindGameObjectWithTag("Player");
        GameObject player = Resources.Load<GameObject>("Prefabs/" + playerChoice.name);
        //Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));

        if (player && SceneManager.GetActiveScene().name == sceneName)
        {
            Instantiate(player, GetComponent<GameController>().playerStart, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
            Destroy(playerChoice);            
            foundPlayer = true;
        }

        if (tags.Length > 0 && SceneManager.GetActiveScene().name == sceneName)
        {
            foreach(string tag in tags)
            {
                GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tag);
                GameObject GameObjects = new GameObject();
                GameObjects.name = "GameObjects";
                foreach(GameObject gameObject in tagObjects)
                {
                    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
                    gameObject.transform.parent = GameObjects.transform;
                }
            }
            foundTags = true;
        }

        if (foundPlayer && foundTags)
        {
            Destroy(this);
        }
    }
}
