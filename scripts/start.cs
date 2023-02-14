using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public Button b1, b2;
    // Start is called before the first frame update
    void Start(){
        b1.onClick.AddListener(click);
        b2.onClick.AddListener(Application.Quit);
    }

    

    void click(){
        Debug.Log("click");
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // if (Input.GetButtonDown("Fire1")&&
        // hit.collider != null&&
        // hit.collider.gameObject.transform.name == "play") {
        //     click();
        // }else if (Input.GetButtonDown("Fire1")&&
        // hit.collider != null&&
        // hit.collider.gameObject.transform.name == "exit") {
        // }
    }
}
