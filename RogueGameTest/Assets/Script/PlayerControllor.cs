using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    private SpriteRenderer SR;
    public Rigidbody2D rb;

/*    public Animator head_anima;
    public Animator body_anima;*/

    public float speed = 3.0f;
    public float cooltime = 1.0f;

    public float x = 0;
    public float y = 0;


    void Move(float x, float y)
    {
        //改变物体速度
        rb.velocity = new Vector2(x * speed, y * speed);
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.left);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        //body_anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal_Player");
        y = Input.GetAxisRaw("Vertical_Player");
        Move(x, y);
        Shoot();
    }
}
