using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is a simple enemy that patrols and when on range it chases the player
public class EnemyHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float moveSpeed = 4.0f;
    int currentWaypoint = 0;
    float detectionRadius = 2.0f;
    float loseRadius = 3.0f;
    float health = 2f;
    bool chasing = false;

    void Update()
    {
        if (health <= 0)
            Death();
        if(Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
            chasing = true;
        else if (Vector3.Distance(transform.position, player.transform.position) < loseRadius)
            chasing = false;
        if (!chasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
                currentWaypoint++;
            if (currentWaypoint > waypoints.Length - 1)
                currentWaypoint = 0;
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    void Death()
    {
        anim.SetBool("IsDead", true);
    }
    
    void OnDeathAnimationFinishes()    //this is triggered via the animation controller
    {
        Destroy(gameObject);
    }
    void TakeDamage(float amount)
    {
      currentHealth -= amount;
    }
}
