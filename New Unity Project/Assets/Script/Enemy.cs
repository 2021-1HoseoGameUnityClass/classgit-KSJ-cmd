using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private GameObject rayObj = null;

    private bool moveRight = true;

    [SerializeField]
    private int lifePoint = 3;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckRay();
        Move();
    }

    private void Move()
    {
        float direction = 0f;
        if(moveRight == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;

        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);

        GetComponent<Animator>().SetBool("Walk", true);
    }

    private void CheckRay()
    {
        if(isDead == false)
        {
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            RaycastHit2D hit2D = Physics2D.Raycast(rayObj.transform.position, Vector2.down, 1f, layerMask.value);

            Debug.DrawRay(rayObj.transform.position, Vector3.down, Color.red);

            if(hit2D == false)
            {
                if (moveRight)
                    moveRight = false;
                else
                    moveRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            lifePoint--;

            if (lifePoint < 1)
            {
                GetComponent<Animator>().SetBool("Walk", false);
                GetComponent<Animator>().SetBool("Dead", true);

                isDead = true;

                Destroy(gameObject, 1);
            }
        }
    }
}
