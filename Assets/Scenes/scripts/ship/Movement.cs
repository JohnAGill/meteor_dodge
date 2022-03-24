using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Ship;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MoveLeft()
    {
        // add animation
        if (Ship.transform.position.x != -2f && Time.timeScale != 0){
             rb.velocity = new Vector3(-3f, 0, 0);
        }
    }
    public void StopMove()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void MoveRight()
    {
        // add animation
        if (Ship.transform.position.x != 2f && Time.timeScale != 0){
              rb.velocity = new Vector3(3f, 0, 0);
        }
    }
   
}
