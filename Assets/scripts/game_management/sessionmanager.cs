using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class sessionmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static sessionmanager instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this;
            DontDestroyOnLoad(instance);
        } 
    }

    [SerializeField] float score = 0f;
    bool score_bool = true; 

    public float get_score()
    {
        return score;
    }

    public void set_score(float increment)
    {
        score += increment;
    }

    void set_ui_score()
    {

        if(SceneManager.GetActiveScene().name == "end")
        {
            if(score_bool)
            {
                score_bool = false;
                GameObject score_card = GameObject.Find("score_card");
                score_card.GetComponent<TMP_Text>().text = $"Score: {score}";
                score = 0f;
            }
        }
        else
        {
            score_bool = true;
        }
           
        
    }

    public void chage_to_level()
    {
        SceneManager.LoadScene("l_1");
    }
    public void change_to_score()
    {
        SceneManager.LoadScene("end");
    }
    public void change_to_menu()
    {
        score_bool = true;
        SceneManager.LoadScene("start");
    }

    private void Update() 
    {
        set_ui_score();  // bad way to go about this.
    }

}
