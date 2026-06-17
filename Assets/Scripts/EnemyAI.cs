using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    float minSpeed = 200f;
    float speed;
    public float nextWayPointDistance = 3f;
    public Animator animator;
    [SerializeField] Timer timer;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        speed = minSpeed + (GameManager.level * 100f);
        Debug.Log("AI Speed ->"+ speed);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 1f);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position,target.position,OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {

            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath=true;
            return;
        }
        else
        {
            reachedEndOfPath=false;
        }

        if(timer.RequestTime() < 30f)
        {
            speed += 1.5f;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);


        if (rb.linearVelocity.magnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }


        transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.linearVelocity);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

}
