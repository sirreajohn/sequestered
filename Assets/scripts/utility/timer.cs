using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField] float total_time = 600f;
    float remain_time;
    bool timer_acitve = true;

    private void Start() 
    {
        remain_time = total_time;
    }

    public string get_str_time()
    {
        float minutes = Mathf.FloorToInt(remain_time / 60); 
        float seconds = Mathf.FloorToInt(remain_time % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float get_time()
    {
        return remain_time;
    }
    
    void time_tick()
    {
        if (timer_acitve)
        {
            if (remain_time < Mathf.Epsilon)
                timer_acitve = false;
            else
                remain_time -= Time.deltaTime;
        }
        else
        {
            remain_time = 0f;
        }
    }
    private void Update() 
    {
        time_tick();
    }

}
