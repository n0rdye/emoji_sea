using System.Collections;
using System.Collections.Generic;
using Rand = System.Random;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class spawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 origin = Vector3.zero;
    public float radius = 5;
    public int count = 6;
    public GameObject[] arr;
    public bool enabl = false;
    private Rigidbody2D rb;
    private float time = 0.0f;
    public float speed = 0.1f;
    public GameObject main_emoj;
    public int inti;
    public float time_betwen = 2f;
    public Animator m_Animator;
    public Animator fin_Animator;
    public Transform[] sits;
    public int sits_left = 3;
    public bool end = false;
    public smile_message msg;


    void Start(){
        m_Animator = GameObject.Find("ship").gameObject.GetComponent<Animator>();
        fin_Animator = GameObject.Find("final").gameObject.GetComponent<Animator>();
        sits_left = sits.Length;
        msg = GameObject.Find("logic_text").GetComponent<smile_message>();
    }


    public bool reload_bool = false;
    public bool leave_bool = false;
    void Update(){
        inti = new Rand().Next(0,arr.Length); 
        // animation
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle")){
            enabl = true;
        }
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("stop")){
            if(!msg.end){
                msg.play_msg = true;
                if (Input.GetButtonDown("Fire1")) {
                    msg.end = true;
                    msg.play_msg = false;
                }
            }
            else if(msg.end){
                m_Animator.SetTrigger("play_game");
            }
        }


        // end game
        if(end){end_game();}
        

        Transform playerTransform = this.transform;
        // get player position
        origin = playerTransform.position;
        time += Time.deltaTime;

        // every "time_betwen" seconds
        if(time>= time_betwen){
            if(enabl&&count>0){
                Vector3 randomPosition = origin + Random.insideUnitSphere * radius;

                GameObject projectile = (GameObject) Instantiate(arr[inti], randomPosition, Quaternion.identity);
                up_down ud = projectile.GetComponent<up_down>();
                ud.speed = speed;
                // Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                // rb = projectile.GetComponent<Rigidbody2D>();
                // projectileRb.AddForce(transform.right * -5, ForceMode2D.Impulse);
                count--;
            }
            else if (count<=0){
                enabl=false;
                count = 6;
            }

            time = 0;
        }

        // click
        if (Input.GetButtonDown("Fire1")&&sits_left>0) {
            click();
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetButtonDown("Fire1")&&
        hit.collider != null&&
        hit.collider.gameObject.transform.name == "reload") {
            reload();
        }
        else if (Input.GetButtonDown("Fire1")&&
        hit.collider != null&&
        hit.collider.gameObject.transform.name == "leave") {
            leave();
        }

        if(reload_bool){
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("end")){
                Debug.Log("reloading");
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                reload_bool = false;
            }
        }
        else if (leave_bool){
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("end")){
                Debug.Log("loading");
                SceneManager.LoadScene("start");
                leave_bool = false;
            }
        }
    }

    void reload(){
        m_Animator.SetTrigger("fin");
        fin_Animator.SetTrigger("out");
        Debug.Log("reload");
        reload_bool = true;
    }

    void leave(){
        m_Animator.SetTrigger("fin");
        fin_Animator.SetTrigger("out");
        Debug.Log("leave");
        leave_bool = true;
    }

    void end_game(){
        enabl = false;
        // Debug.Log("end  game");
        // m_Animator.SetTrigger("fin");
        fin_Animator.SetTrigger("fin");
    }

    void click(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null){
                    // Debug.Log(hit.collider.gameObject.transform.tag);
                    // Debug.Log(main_emoj.gameObject.name);
                if(hit.collider.gameObject.transform.tag + "(Clone)" == main_emoj.gameObject.name){
                    Debug.Log("nice!");
                    up_down script = hit.collider.gameObject.GetComponent<up_down>();
                    script.to_ship = true;
                    sits_left--;
                }
            }
    }
}
