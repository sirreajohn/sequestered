using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawn_points;
    [SerializeField] GameObject[] enemy_prefabs;

    bool spawn_active = true;
    [SerializeField] float spawn_delay = 2f;
    [SerializeField] float spawn_rate = 1f;

    timer clock;

    private void Start() 
    {
        clock = FindObjectOfType<timer>();
    }
    
    public void spawn_enemy(float spawn_rate = 1)
    {
        for (int i = 0; i < spawn_rate; i++)
        {
            int chosen_spawn = Random.Range(0, spawn_points.Length - 1);
            int chosen_enemy = Random.Range(0, enemy_prefabs.Length - 1);

            Instantiate(enemy_prefabs[chosen_enemy], spawn_points[chosen_spawn].transform.position, Quaternion.identity);
        }
    }
    public void set_spawn_inactive()
    {
        spawn_active = false;
    }
    public void set_spawn_active()
    {
        spawn_active = true;
    }
    IEnumerator create_spawn()
    {
        spawn_enemy(spawn_rate);
        yield return new WaitForSeconds(spawn_delay);
        spawn_active = true;
    }
    void update_spawn_rate()
    {
        int minutes = (int)(clock.get_time() / 60);
        spawn_rate = (float)(10 - minutes);   // 9min - spawn_rate1, 8-2, 7-3
    }

    private void Update() 
    {
        if (spawn_active)
        {
            spawn_active = false;
            StartCoroutine(create_spawn());
        }
        update_spawn_rate();

        if (spawn_rate == 0)
        {
            spawn_rate = 1f;
        }
    }
}
