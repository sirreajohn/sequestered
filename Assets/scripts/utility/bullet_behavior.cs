using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    [SerializeField] float bullet_speed = 4500f;
    [SerializeField] float damage_val = 100f;
    [SerializeField] float despawn_timer = 5f;
    [SerializeField] ParticleSystem ex;
    Rigidbody2D phys_engine;
    bool stop_bool = false;
    SpriteRenderer sprite;

    private void Awake() 
    {
        phys_engine = GetComponent<Rigidbody2D>(); 
        sprite = GetComponent<SpriteRenderer>();     
    }

    public float get_damage_no()
    {
        return damage_val;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "player_projectile") return;

        stop_bool = true;
        phys_engine.velocity = Vector3.zero;
        sprite.enabled = false; // diable sprite

        ex.Play();
        Destroy(gameObject, 1f);
    }

    void move_bullet()
    {
        Vector2 speed_vector = new Vector2(0, bullet_speed * Time.deltaTime);
        phys_engine.AddRelativeForce(speed_vector); 
        Destroy(gameObject, despawn_timer);
    }

    void Update() 
    {
        if (!stop_bool)
            move_bullet();
    }
}
