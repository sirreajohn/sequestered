using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    [SerializeField] float health = 200f;
    [SerializeField] float score = 10f;
    [SerializeField] float damage = 50f;
    bool isalive = true;
    Animator anim;
    sessionmanager manager;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        manager = FindObjectOfType<sessionmanager>();
    }
    public float get_health()
    {
        return health;
    }
    public float get_damage_no()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "player_projectile")
        {
            health -= other.gameObject.GetComponent<bullet_behavior>().get_damage_no();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
            kill_enemy();
    }

    void kill_enemy()
    {
        // do some stuff
        // death noises
        if(isalive)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            isalive = false;
            manager.set_score(score);
            anim.SetBool("isrunning", false);
            anim.SetTrigger("isdead");
            Destroy(gameObject, 3f);   // kill enemy (despawn object) 
        }
   
    }

    private void Update() 
    {
        if (health <= Mathf.Epsilon)
        {
           kill_enemy(); 
        }
    }
}
