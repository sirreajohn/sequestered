using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_behavior : MonoBehaviour
{

    [SerializeField] TMP_Text clock_text;
    [SerializeField] float objective_duration = 3f;
    [SerializeField] TMP_Text dodge;
    [SerializeField] TMP_Text score;
    [SerializeField] GameObject objective;
    [SerializeField] GameObject health_bar;
    [SerializeField] GameObject[] mobile_controls;
    

    player_movement movement_script;
    player_health health_script;
    float curr_score = 0f;
    timer clock;
    
    private void Start() 
    {
        clock = FindObjectOfType<timer>();
        movement_script = FindObjectOfType<player_movement>();
        health_script = FindObjectOfType<player_health>();
        StartCoroutine(set_objective());
    }
    IEnumerator set_objective()
    {
        yield return new WaitForSeconds(objective_duration);
        objective.SetActive(true);
        yield return new WaitForSeconds(objective_duration);
        objective.SetActive(false);
    }

    public void toggle_mobile_controls(bool toggle_action)
    {
        foreach(GameObject controls in mobile_controls)
        {
            controls.SetActive(toggle_action);
        }
    }
    public void get_score()
    {
        curr_score = FindObjectOfType<sessionmanager>().get_score();
    }

    void set_dodge_ready()
    {
        dodge.text = "<color=#33E91D> Dodge Ready</color>";
    }
    void set_dodge_not_ready()
    {
        dodge.text = "<color=#E91D1F> Dodge in cooldown</color>";
    }

    void handle_dodge()
    {
        if (movement_script.get_boost_bool())
            set_dodge_ready();
        else
            set_dodge_not_ready();
    }
    void handle_score()
    {
        get_score();  // updates curr_score
        score.text = $"{curr_score}";
    }
    void handle_health()
    {
        float health = health_script.get_health() / 200f;
        health_bar.GetComponent<Image>().fillAmount = health;
    }
    private void Update() 
    {
        clock_text.text = clock.get_str_time();
        handle_dodge();
        handle_score();
        handle_health();
    }
}
