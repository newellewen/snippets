using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    public Vector2 positionOffset;    
    public Vector2 centerOfMass;
    public float startRotation;
    public float maxRotation;
    public float baseSpeed;

    public float damage;
    public float force; 


    private bool forward;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (transform.parent.tag == "Player")
        {
            Physics2D.IgnoreCollision(transform.parent.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            if (transform.parent.localScale.x < 0)
            {
                forward = false;
            }
            else
            {
                forward = true;
            }
            rigidBody.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
            transform.parent = null;

            Attack();

        }


    }

    void Attack()
    {
        Vector2 position = transform.position;
        float rotation = 0.0f;

        if (forward)
        {
            position += new Vector2(positionOffset.x, positionOffset.y);
            rotation += startRotation;
            baseSpeed = -baseSpeed;
        }
        else
        {
            position += new Vector2(-positionOffset.x, positionOffset.y);
            rotation -= startRotation;
            Vector2 flip = transform.localScale;
            flip.x = -Mathf.Abs(flip.x);
            transform.localScale = flip;
            centerOfMass = new Vector2(-centerOfMass.x, centerOfMass.y);
        }

        rigidBody.centerOfMass = centerOfMass;
        transform.position = position;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);

        rigidBody.angularVelocity = baseSpeed;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (forward && rigidBody.rotation < -maxRotation)
        {
            Destroy(this.gameObject);
        }
        else if (!forward && rigidBody.rotation > maxRotation)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<BadGoblinController>().health -= damage;



                //collision.gameObject.GetComponent<Rigidbody2D>().velocity
            }

            Destroy(this.gameObject);
        }
          
    }
}
