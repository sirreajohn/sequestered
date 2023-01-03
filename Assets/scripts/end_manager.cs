using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end_manager : MonoBehaviour
{
    public void change_to_menu()
    {
        SceneManager.LoadScene("start");
    }

    public void change_to_level()
    {
        SceneManager.LoadScene("l_1");
    }
}
