using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGoblinController : MonoBehaviour
{

    public float health;
    public float moveSpeed;
    public float damage;

    private Rigidbody2D rigidBody;
    private bool hittingPlayer;    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        hittingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        float distance = Vector2.Distance(playerPosition, transform.position);

        if (distance <= 1.0f)
        {
            rigidBody.velocity = moveSpeed * (playerPosition - new Vector2 (transform.position.x, transform.position.y));
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }

        if (hittingPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health -= damage;
        }


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hittingPlayer = true;            
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hittingPlayer = false;
        }
    }

}
