using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class random_emoj : MonoBehaviour
{
    public GameObject[] arr;
    private GameObject childObj;
    private Random rnd = new Random();
    public bool norm = true;
    // Start is called before the first frame update
    void Awake()
    {
        if(norm){
            spawn logic = GameObject.Find("logic").GetComponent<spawn>();
            int inti = rnd.Next(0,arr.Length);
            childObj = (GameObject) Instantiate(arr[inti], this.transform.position, Quaternion.identity);;
            logic.main_emoj = childObj;
            childObj.transform.parent = this.gameObject.transform;
            // in case you want the new gameobject to be a child of the gameobject that your script is attached to
            childObj.transform.SetParent(this.gameObject.transform);
        }else{
            int inti = rnd.Next(0,arr.Length);
            childObj = (GameObject) Instantiate(arr[inti], this.transform.position, Quaternion.identity);;
            childObj.transform.parent = this.gameObject.transform;
            // in case you want the new gameobject to be a child of the gameobject that your script is attached to
            childObj.transform.SetParent(this.gameObject.transform);
        }
    }

    void Update()
    {
        
    }
}
