using System.Collections;
using System.Collections.Generic;
using Rand = System.Random;
using UnityEngine;

public class up_down : MonoBehaviour
{
    public Rigidbody2D rb;
    public float up = 0.1f,down = -0.1f;
    private float time = 0.0f;
    public float time_to_del = 20;
    public float interpolationPeriod = 1;
    public float speed = 0.1f;
    public spawn main;
    public bool to_ship = false;
    public CircleCollider2D col;

    public bool going = false;
    public int sit;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("logic").GetComponent<spawn>();
        rb = this.transform.GetComponent<Rigidbody2D>();
        col = this.gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(to_ship){
            start_to_ship();
        }
        if (going){
            move_to(1.5f,main.sits[sit].position);
        }
        else{
            main_logic();
        }
        
        
    }

    void start_to_ship(){
        to_ship = false;
        going = true;
        col.enabled = false;
        sit = main.sits_left;
        // Debug.Log(main.sits_left);
    }

    void main_logic(){
        rb.AddForce(transform.right * -speed, ForceMode2D.Impulse);
        time += Time.deltaTime;
        time_to_del -= Time.deltaTime;
 
        if (time >= interpolationPeriod) {    
            if(rb.gravityScale == down){
                rb.gravityScale = up;
            }
            else if (rb.gravityScale == up)
            {
                rb.gravityScale = down;
            }
            time = 0.0f;
        }
        if(time_to_del<=0){
            Destroy(this.gameObject);
            main.count += 1;
        }
    }


    void move_to(float speedi,Vector3 pos){
        float speed = speedi;
        Vector3 targetPosition = pos;

        Vector3 currentPosition = this.transform.position;
        if(Vector3.Distance(currentPosition, targetPosition) > .1f) { 
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();

            this.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
        else{
            rb.gravityScale = 0;
            this.gameObject.transform.SetParent(GameObject.Find("main_emoj").transform);
            if(sit<=0){
                main.end = true;
                return;
            }
        }
    }


}
