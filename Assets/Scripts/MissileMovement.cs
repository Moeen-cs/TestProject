using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    Transform target;
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
                    // Swap the position of the cylinder.
                    // rached = true;
                }
            }
        }
        else target = GameObject.FindGameObjectWithTag("Base").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="usergun" )
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "usermissile")
        {
            PlayerMissileMovement go = collision.transform.gameObject.GetComponent<PlayerMissileMovement>();
            go.destryAim();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "attackcircle" )
        {
            Destroy(this.gameObject);
        }
    }
}
