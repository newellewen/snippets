using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Vector2 playerStart;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);


        Vector2 rockStart = new Vector2(-2.4f, -1.0f);

        for (float x = rockStart.x; x <= -rockStart.x; x = x + 0.165f)
        {
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("Prefabs/Rock"), new Vector2(x, rockStart.y), Quaternion.identity));
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("Prefabs/Rock"), new Vector2(x, -rockStart.y), Quaternion.identity));
        }
        for (float y = rockStart.y; y <= -rockStart.y; y = y + 0.15f)
        {
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("Prefabs/Rock"), new Vector2(rockStart.x, y), Quaternion.identity));
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("Prefabs/Rock"), new Vector2(-rockStart.x, y), Quaternion.identity));
        }

        Vector2[] goblins =
        {
            new Vector2 (2.0f, 0.8f),
            new Vector2 (2.0f, -0.8f),
            new Vector2 (-2.0f, 0.8f),
            new Vector2 (-2.0f, -0.8f)
        };

        foreach (Vector2 goblinPosition in goblins)
        {
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("Prefabs/BadGuys/BadGoblin"), goblinPosition, Quaternion.identity));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnGUI()
    //{
    //}

}
