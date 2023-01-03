using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    float boost_multiplier;
    Vector3 move_axis;
    Animator anim;
    [SerializeField] float move_velocity;
    [SerializeField] float boost_velocity_multipler = 1f;
    [SerializeField] bool boost_bool = true;
    [SerializeField] float boost_recharge_rate = 3f;
    [SerializeField] float boost_duration = 0.5f;

    private void Awake() 
    {
        boost_multiplier = boost_velocity_multipler;
        boost_velocity_multipler = 1f;
        anim = GetComponent<Animator>();
    }

    public bool get_boost_bool()
    {
        return boost_bool;
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
        Vector2 move_direction = context.action.ReadValue<Vector2>();

        if (Mathf.Abs(move_direction.x) > Mathf.Epsilon)  // handling zero case
        {
            float sign = Mathf.Sign(move_direction.x);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * sign, transform.localScale.y);
        }

        anim.SetBool("isrunning", true);
        move_axis = move_direction * move_velocity; // read player inputs.
        if (Mathf.Abs(move_direction.x) < Mathf.Epsilon && (Mathf.Abs(move_direction.y) < Mathf.Epsilon))
        {
            anim.SetBool("isrunning", false);
        }
    }

    void move_player()
    {
         transform.position += move_axis * boost_velocity_multipler * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        move_player();
    }
}
