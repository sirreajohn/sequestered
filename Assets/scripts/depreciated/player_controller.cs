using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class player_controller : MonoBehaviour
{

    Actions controls;
    float boost_multiplier;
    float fire_rate;
    weapon_so current_weapon;
    [SerializeField] weapon_so[] weapons;
    [SerializeField] Camera main_cam;
    [SerializeField] Transform bullet_direction;
    [SerializeField] bool fire_rate_bool = true;
    [SerializeField] float move_velocity;
    [SerializeField] float boost_velocity_multipler = 1f;
    [SerializeField] float boost_recharge_rate = 3f;
    [SerializeField] float boost_duration = 0.5f;
    [SerializeField] bool boost_bool = true;
    Vector3 move_axis;

    private void Awake() 
    {
        controls = new Actions();
        boost_multiplier = boost_velocity_multipler;
        boost_velocity_multipler = 1f;

        set_weapon(weapon_index: 0);  // default pistol

    }

    void set_weapon(int weapon_index)
    {
        current_weapon = weapons[weapon_index];
        apply_weapon_attributes();
    }
    void apply_weapon_attributes()
    {
        fire_rate = current_weapon.get_fire_rate();
    }

    private void OnEnable() 
    {
        controls.Enable();
    }

    private void OnDisable() 
    {
        controls.Disable();
    }

    IEnumerator fire_bullet()
    {
        fire_rate_bool = false; // flag to control fire rate.

        // get aim coordinates
        // Vector2 mouse_pos = controls.player.aim.ReadValue<Vector2>();
        // mouse_pos = main_cam.ScreenToWorldPoint(mouse_pos);

        // Instantiate(bullet, bullet_direction.position, bullet_direction.rotation);

        current_weapon.fire(bullet_direction.position, bullet_direction.rotation, current_weapon.get_weapon_name());

        yield return new WaitForSeconds(fire_rate);
        fire_rate_bool = true;

    }

    IEnumerator dodge_player()
    {
        boost_bool = false;
        boost_velocity_multipler = boost_multiplier;
        yield return new WaitForSeconds(boost_duration);
        boost_velocity_multipler = 1f;

        yield return new WaitForSeconds(boost_recharge_rate);
        boost_bool = true;
    }

    void rotate_player()
    {
        // convert mouse coord to world coords
        Vector2 mouse_coord = controls.player.aim.ReadValue<Vector2>();
        Vector3 mouse_world_coord = main_cam.ScreenToWorldPoint(mouse_coord);

        // calculate bullet angle
        Vector3 target_position = mouse_world_coord - transform.position;
        float angle = (Mathf.Atan2(target_position.y, target_position.x) * Mathf.Rad2Deg) - 90f; // 90f to account for mouse offset.

        transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, angle));
    }

    public void shoot(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            if (fire_rate_bool)
            {
                StartCoroutine(fire_bullet());
            }
        }
    }
    public void dodge(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            if (boost_bool)
            {
                StartCoroutine(dodge_player());
            }
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        move_axis = context.action.ReadValue<Vector2>() * move_velocity; // read player inputs.
    }

    void move_player()
    {
         transform.position += move_axis * boost_velocity_multipler * Time.deltaTime;
    }

    private void Update()
    {   
        move_player();
        rotate_player();
    }
}
