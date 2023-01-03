using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_health : MonoBehaviour
{

    bool i_frame_bool = false;
    Animator anim;
    [SerializeField] float hit_points = 100f;
    [SerializeField] float i_frame_duration = 1f; // i frames
    PlayerInput input_system;
    PolygonCollider2D collision_manager;
    bool player_dead = false;
    spawner enemy_spawner;
    bool next_scene_bool = true;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        input_system = GetComponent<PlayerInput>();
        collision_manager = GetComponent<PolygonCollider2D>();
        enemy_spawner = FindObjectOfType<spawner>();
    }

    public bool get_player_status()
    {
        return player_dead;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (i_frame_bool) return;

        // reduce according to bullet. (other.gameobject.damage) --ensure damage is public
        if(other.gameObject.tag == "enemy")
        {
            i_frame_bool = true;
            anim.SetBool("ishurt", true);
            hit_points -= other.gameObject.GetComponent<enemy_health>().get_damage_no();
        }
        // i frame mechanics here.
        StartCoroutine(toggle_i_frame()); 
    }

    IEnumerator toggle_i_frame()
    {
        yield return new WaitForSeconds(i_frame_duration);
        i_frame_bool = false;
        anim.SetBool("ishurt", false);
    }

    public float get_health()
    {
        return hit_points; // for UI
    }

    void kill_player()
    {
        if (player_dead) return;

        Destroy(enemy_spawner.gameObject);
        player_dead = true;
        input_system.enabled = false;
        Destroy(gameObject, 3f);   // kill player after 3s delay (despawn object)   
        anim.SetBool("isdead", true);
        collision_manager.enabled = false;
        // some audio?
        // death noises  
    }
    IEnumerator change_scene_to_score()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<sessionmanager>().change_to_score();
    }
    private void Update() 
    {
        if (hit_points <= Mathf.Epsilon)
        {
            kill_player();
            if(next_scene_bool)
            {
                next_scene_bool = false;
                StartCoroutine(change_scene_to_score());

            }
        }
    }


}
