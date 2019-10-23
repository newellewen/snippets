using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float count;
    
    public float timeConstant;

    private LineRenderer lineRenderer;

    private BoxCollider2D collider;

    private bool moving;
    public float moveSpeed;
    public float moveInterval;

    public float offsetX;
    public float offsetY;

    private float t;

    public Vector3 controlPoint1;
    public Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeed == 0)
        {
            moveSpeed = 0.1f;
        }

        if (timeConstant == 0)
        {
            timeConstant = 0.1f;
        }

        if (controlPoint1 == Vector3.zero)
        {
            controlPoint1 = new Vector3(1.0f, -3.0f, 0.0f);
        }
        
        

        if (startPos.x == 0)
        {
            startPos = new Vector3(1.8f, -0.04f, 0.0f);
        }

        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        collider = gameObject.GetComponent<BoxCollider2D>();

        Physics2D.alwaysShowColliders = true;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;


        t = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {



        Vector2 targetPos = GameObject.FindGameObjectWithTag("Target").transform.position;

        RaycastHit2D hitRay = Physics2D.Raycast(collider.bounds.center, targetPos);

        //Debug.Log(t);

        //if (hitRay.collider != null && hitRay.collider.gameObject.tag != "Border")
        //{
            if (t < 1.0f)
            {
                Debug.Log(String.Format("{0} - {1}", hitRay.collider.gameObject.name, t));
                //rigidBody.velocity = Vector2.zero;



                //Vector3 p4 = targetPos;

                

                Move(targetPos, t);
                t += Time.deltaTime * timeConstant;
            }
        //}

        else
        {
            rigidBody.velocity = Vector2.zero;
            Debug.Log("Thing");
        }

        //if (hitRay.collider != null && hitRay.collider.gameObject.tag != "Border")
        //    {
        //        lineRenderer.SetPositions(new Vector3[] { this.top, hitRay.collider.bounds.min });




        //    }
        //    else
        //    {
        //Debug.Log("Path clear");
        //this.rigidBody.velocity = Vector2.zero;
        //t = 0.0f;
        //Move(targetPos);
        //lineRenderer.SetPositions(new Vector3[2] { this.transform.position, targetPos });
        //}


        if (Input.GetKeyDown(KeyCode.R))
        {
            rigidBody.velocity = Vector2.zero;
            transform.position = startPos;
            t = 0.0f;
        }

        


    }

    void Move(Vector3 targetPos, float time = 0.0f)
    {



        if (time > 0)
        {

            RaycastHit2D hitRay = Physics2D.Raycast(collider.bounds.center, targetPos);

            Vector3 p1 = this.transform.position;
            Vector3 p2 = new Vector3(hitRay.collider.bounds.min.x - offsetX, hitRay.collider.bounds.min.y - offsetY, 0.0f);
            Vector3 p3 = targetPos;
            //lineRenderer.SetPositions(new Vector3[] { this.transform.position, p2 });
            Tuple<Vector3, float> vector = qBezier(p1, p2, p3, time);

            this.rigidBody.velocity = (vector.Item1 - this.transform.position).normalized * vector.Item2 * moveSpeed;

        }
        


        //this.rigidBody.velocity = (targetPos - this.transform.position).normalized * qBezierPrime(, )
        
    }

    Tuple<Vector3, float> qBezier(Vector3 p0, Vector3 p1, Vector3 p2, float time)
    {
        float timeRemaining = 1.0f - time;

        Vector3 velocity = new Vector3(
            p1.x + (timeRemaining * timeRemaining * (p0.x - p1.x)) + (time * time * (p2.x - p1.x)),
            p1.y + (timeRemaining * timeRemaining * (p0.y - p1.y)) + (time * time * (p2.y - p1.y)),
            0.0f
        );

        return new Tuple<Vector3, float>(velocity, qBezierPrime(p0, p1, p2, time));
    }


    float qBezierPrime(Vector3 p0, Vector3 p1, Vector3 p2, float time)
    {
        float timeRemaining = 1.0f - time;

        float coef1 = 2 * timeRemaining;
        float coef2 = 2 * time;

        return new Vector3(
            coef1 * (p1.x - p0.x) + coef2 * (p2.x - p1.y),
            coef1 * (p1.y - p0.y) + coef2 * (p2.y - p1.y),
            0.0f
        ).magnitude;
    }


}
