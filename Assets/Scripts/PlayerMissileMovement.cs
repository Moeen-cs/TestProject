using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    Transform target;
    public GameObject attackZone;
    bool rached = false;
    public void MoveMissile(Transform target,float speed)
    {
        this.target = target;
        this.speed = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (rached == false)
            {
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    GameObject Go = Instantiate(attackZone);
                    Go.transform.position = this.transform.position;
                    // Swap the position of the cylinder.
                    // rached = true;
                    Destroy(target.transform.gameObject);
                    Destroy(this.gameObject);

                }
            }
        }
    }
    public void destryAim()
    {
        Destroy(target.transform.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="boundry")
        {
            Destroy(this.gameObject);
        }

    }
}
