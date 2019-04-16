using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startPositions;

    public GameObject[] rooms;//0-->LR 1-->LRB 2-->LRT 3-->LRTB

    public float movement=10;

    private float timeBtwMove;

    public float startTimeBtwMove = 0.25f;
    private int direction;

    private int downCounter = 0;
    public LayerMask whatIsRoom;
    
    public float minX;
    public float maxX;
    public float minY;

    public static bool stopGeneration;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, startPositions.Length);
        transform.position = startPositions[rand].position;
        Instantiate(rooms[rand], transform.position, Quaternion.identity);
        
        direction = Random.Range(1, 6);
        
    }


    private void Update()
    {
        if (timeBtwMove <= 0 && stopGeneration==false)
        {
            Move();
            timeBtwMove = startTimeBtwMove;
        }
        else
        {
            timeBtwMove -= Time.deltaTime;
        }
    }

    void Move()
    {
        
        Vector2 newPos;
        
        if (direction == 1 || direction == 2)
        {
            
            if (transform.position.x < maxX) //Move Right
            {
                downCounter = 0;
                newPos = new Vector2(transform.position.x + movement, transform.position.y);
                transform.position = newPos;
                
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    
                direction = Random.Range(1,6);


                if (direction == 3)
                {
                    direction = 2;
                }
                else if(direction == 4)
                {
                    direction = 5;
                }
                    
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Move Left
        {
            
            if (transform.position.x > minX)
            {
                downCounter = 0;
                newPos = new Vector2(transform.position.x - movement, transform.position.y);
                transform.position = newPos;
                
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 5)
        {

            downCounter++;
            if (downCounter >= 2)
            {
                
            }
            
            if (transform.position.y > minY)
            {
                //要向下时检测当前这个room是否有向下的出口
                Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

                if (collider2D.GetComponent<RoomType>().type == 0 || collider2D.GetComponent<RoomType>().type == 2)
                {
                    if (downCounter >= 2)
                    {
                        collider2D.GetComponent<RoomType>().Destruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        collider2D.GetComponent<RoomType>().Destruction();
                    
                        int randButtomRoom = Random.Range(1, 4);
                        if (randButtomRoom == 2)
                        {
                            randButtomRoom = 3;
                        }
                    
                        Instantiate(rooms[randButtomRoom], transform.position, Quaternion.identity);
                    }
                    
                    
                }
                
                
                newPos = new Vector2(transform.position.x, transform.position.y- movement);
                transform.position = newPos;

                int rand = Random.Range(2,4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                
                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
            }
        }



    }
    
}
