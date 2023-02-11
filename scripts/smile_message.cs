using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class smile_message : MonoBehaviour
{
    public float time;
    public string texts =  "Я - радостный. Найди похожие на меня смайлы";
    public TMP_Text tex_tmp;
    public GameObject tex_obj;
    public char[] text_arr;
    public int char_i=0;
    public bool play_msg = false;
    public bool end=false;
    public Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        tex_obj.SetActive(false);
        text_arr = texts.ToCharArray();
        m_Animator = tex_obj.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(char_i<43&&play_msg&&!end){
            time += Time.deltaTime;
            tex_obj.SetActive(true);
            if(time>=0.02){
                time=0;
                tex_tmp.text += text_arr[char_i].ToString();
                char_i++;
            }
        }else if (char_i==43&&!end){
            time += Time.deltaTime;
            tex_tmp.text = texts;
            if(time>=5){
                end=true;
                time = 0;
            }
        }
        else if (end){
            time += Time.deltaTime;
            m_Animator.SetTrigger("out");
            char_i=44;
            play_msg=false;
            end=true;
            time=0;
            if(time>=10){
                tex_obj.SetActive(false);
            }
            return;
        }
        
    }
}
