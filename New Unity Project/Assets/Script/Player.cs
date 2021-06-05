using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 3f;
    [SerializeField]
    float playerJumpForce = 0.1f;
    [SerializeField]
    GameObject bulletObj = null;
    [SerializeField]
    GameObject InstantiateObj = null;
    [SerializeField]
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float moveSpeed = h * playerSpeed * Time.deltaTime;

        Vector3 vector3 = new Vector3();

        vector3.x = moveSpeed;

        transform.Translate(vector3);
        if (h < 0)
        {
            this.GetComponent<Animator>().SetBool("Walk", true);

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            this.GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void PlayerJump()
    {
        GetComponent<Animator>().SetBool("Walk", false);
        GetComponent<Animator>().SetBool("Jump", true);

        Vector2 vector2 = new Vector2(0, playerJumpForce);
        GetComponent<Rigidbody2D>().AddForce(vector2);
        jump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            jump = false;
        }
    }

    private void Fire()
    {
        
        AudioClip audioClip = Resources.Load<AudioClip>("RangedAttack.ogg");
        GetComponent<AudioSource>().clip = audioClip; 
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);

        Instantiate(bulletObj, InstantiateObj.transform.position, quaternion);
        GetComponent<Bullet>().InstantiateBullet(direction);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            DataManager.instance.playerHP--;
            UIManager.instance.PlayerHP();
        }
    }
}