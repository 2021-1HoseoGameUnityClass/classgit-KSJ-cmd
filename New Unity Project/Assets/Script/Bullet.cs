using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 20f;
    float Direction = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = moveSpeed * Time.deltaTime * Direction;

        Vector3 vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
    }

    public void InstantiateBullet(float _direction)
    {
        Direction = _direction;
        Vector3 vector3 = new Vector3(_direction, 0, 0);
        transform.localScale = vector3;

        Destroy(this.gameObject, 3f);
    }
}
