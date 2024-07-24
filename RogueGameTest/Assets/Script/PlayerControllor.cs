using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    public Rigidbody2D rb;

    public Animator head_anima;
    public Animator body_anima;

    public float speed = 3.0f;
    public float cooltime = 1.0f;

    public float x = 0;
    public float y = 0;

    [Header("PlayerInformation")]
    public float MaxHp = 6.0f;
    public float CurrentHp;

    public float shootatk = 5;
    public float boomatk = 10;
    public int keyNum = 0;
    public int Money = 0;

    private bool isAttacking = false;

    void Move(float x, float y)
    {
        //�ı������ٶ�
        rb.velocity = new Vector2(x * speed, y * speed);
    }//WASD��·
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            head_anima.SetTrigger("isUp");
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.up);
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            head_anima.SetTrigger("isDown");
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.down);
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            head_anima.SetTrigger("isRight");
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.right);
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            head_anima.SetTrigger("isLeft");
            GameObject bulletObj = Instantiate(Bullet);
            bulletObj.transform.position = transform.position;
            BulletControl bullet = bulletObj.GetComponent<BulletControl>();
            bullet.SetDirection(Vector2.left);
            isAttacking = true;
        }
    }//Arrow�����

    void NotAttacking()//δ�ڹ���
    {
        if (Input.GetAxisRaw("Vertical")==0 && Input.GetAxisRaw("Horizontal")==0)
        {
            isAttacking=false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal_Player");
        y = Input.GetAxisRaw("Vertical_Player");
        Move(x, y);
        Shoot();
        NotAttacking();
        SwitchAnimation();
    }
    public void SwitchAnimation()
    {
        body_anima.SetFloat("Horizontal", x);
        body_anima.SetFloat("Vertical", y);
        head_anima.SetFloat("Horizontal", x);
        head_anima.SetFloat("Vertical", y);
    }
    private void OnTriggerEnter2D(Collider2D collision)//������
    {
        if(collision.gameObject.CompareTag("EnemyShoot") && isAttacking==false)
        {
            MaxHp -= 0.5f;
            if (CurrentHp > 0.5)
            {
                body_anima.SetBool("isHitten", true);
                head_anima.SetBool("isHitten", true);
                CurrentHp -=0.5f;
            }
        }
        if (CurrentHp <= 0)
        {
            Debug.Log("Have Died");
            Destroy(gameObject);
        }
    }
}
