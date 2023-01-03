using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class player_combat_mech : MonoBehaviour
{
    Actions controls;
    float fire_rate;
    weapon_so current_weapon;
    [SerializeField] weapon_so[] weapons;
    [SerializeField] Camera main_cam;
    [SerializeField] Transform bullet_direction;
    [SerializeField] bool fire_rate_bool = true;
    Animator anim;
    bool isController;

    private void Awake() 
    {
        controls = new Actions();
        set_weapon(weapon_index: 0);  // default pistol
        anim = GetComponentInParent<Animator>();
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
        anim.SetTrigger("isshooting");
        current_weapon.fire(bullet_direction.position, bullet_direction.rotation, current_weapon.get_weapon_name());

        yield return new WaitForSeconds(fire_rate);
        fire_rate_bool = true;
        anim.SetTrigger("isshooting");

    }

    void rotate_player()
    {
        // convert mouse coord to world coords
        Vector2 coord = controls.player.aim.ReadValue<Vector2>();
        float angle;

        if (!isController)
        {
            Vector3 mouse_world_coord = main_cam.ScreenToWorldPoint(coord);
            Vector3 target_position = mouse_world_coord - transform.position;
            angle = (Mathf.Atan2(target_position.y, target_position.x) * Mathf.Rad2Deg) - 90f; // 90f to account for mouse offset.
        }
        else
        {
            // calculate bullet angle
            angle = (Mathf.Atan2(coord.y, coord.x) * Mathf.Rad2Deg) - 90f; // 90f to account for mouse offset.
        }
        
        
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

    public void OnDeviceChange(PlayerInput input)
    {
        isController = input.currentControlScheme.Equals("gamepad");
        if(isController)
        {
            FindObjectOfType<UI_behavior>().toggle_mobile_controls(true);
        }
        else
        {
            FindObjectOfType<UI_behavior>().toggle_mobile_controls(false);
        }
    }

    private void Update()
    { 
        rotate_player();
    }

}
