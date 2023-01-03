using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behavior : MonoBehaviour
{
    [SerializeField] float move_speed;
    [SerializeField] GameObject follow_obj;
    Animator anim;
    enemy_health health_script;
    bool check_player = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health_script = GetComponent<enemy_health>();
        follow_obj = FindObjectOfType<player_movement>().gameObject;
    }

    private void Update()
    {
        if(check_player)
        {
            if (!follow_obj.GetComponent<player_health>().get_player_status())
            {  
                if (health_script.get_health() > Mathf.Epsilon)
                {
                    anim.SetBool("isrunning", true);
                    transform.position = Vector2.MoveTowards(transform.position, follow_obj.GetComponent<Transform>().position, move_speed * Time.deltaTime);
                }    
            }
            else
            {
                anim.SetBool("isrunning", false);
                check_player = false;
            }
                
        }

    }
}
