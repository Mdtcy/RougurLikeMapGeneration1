using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;

    public GameObject[] rooms;
    // Update is called once per frame
    void Update()
    {
        if (LevelGeneration.stopGeneration == true)
        {
            Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
            if (collider2D == null)
            {
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
