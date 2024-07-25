using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Room room;

    public GameObject DoorUp,DoorDown,DoorLeft,DoorRight;//�ĸ��������
    public int StepToStart;//�뿪ʼ�������
    public int DoorNum;//�м�����
    public bool LeftHasRoom, RightHasRoom, UpHasRoom, DownHasRoom;//�ĸ�������û����
    public int EnemyNum;//�����ڵ�������

    public BoxCollider2D Collider;

    public bool PlayerInRoom=false;
    private void Awake()
    {
        room = GetComponent<Room>();
        Collider = room.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DoorUp.SetActive(UpHasRoom);
        DoorDown.SetActive(DownHasRoom);
        DoorRight.SetActive(RightHasRoom);
        DoorLeft.SetActive(LeftHasRoom);
    }

    public void UpdateRoom(float xOffset,float yOffset)//��StepToStart��DoorNum
    {
        StepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        if (LeftHasRoom)
        {
            DoorNum++;
        }
        if(RightHasRoom)
        {
            DoorNum++;
        }
        if (UpHasRoom)
        {
            DoorNum++;
        }
        if (DownHasRoom)
        {
            DoorNum++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//������ż��
    {
        if (collision.CompareTag("Player"))
        {
            CameraControllor.instance.ChangeTarget(transform);
        }
    }
    public void ControlDoor()
    {
        if (PlayerInRoom == true && EnemyNum >0)
        {
            CloseTheDoor();
        }
        else
        {
            OpenTheDoor();
        }
    }
    public void PlayerInside()
    {
        if (Collider.bounds.Contains( GameObject.Find("PlayerControllor").transform.position))
        {
            PlayerInRoom = true;
        }
        else
        {
            PlayerInRoom = false;
        }
    }

    public void OpenTheDoor()//���� Show����ͼ�� && ɾ������
    {
        if (LeftHasRoom)
        {
            transform.Find("Door_left").Find("Door_L_Open").gameObject.SetActive(true);
            Destroy(GameObject.Find("Door_left").GetComponent<Rigidbody2D>());
            GameObject.Find("Door_left").GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("Opend Left");
        }
        if (UpHasRoom)
        {
            transform.Find("Door_up").Find("Door_U_Open").gameObject.SetActive(true);
            Destroy(GameObject.Find("Door_up").GetComponent<Rigidbody2D>());
            GameObject.Find("Door_up").GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(GameObject.Find("Door_up").GetComponent<Rigidbody2D>());
            //Debug.Log("Opend Up");
        }
        if (DownHasRoom)
        {
            transform.Find("Door_down").Find("Door_D_Open").gameObject.SetActive(true);
            Destroy(GameObject.Find("Door_down").GetComponent<Rigidbody2D>());
            GameObject.Find("Door_down").GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(GameObject.Find("Door_down").GetComponent<Rigidbody2D>());
            //Debug.Log("Opend Down");
        }
        if (RightHasRoom)
        {
            transform.Find("Door_right").Find("Door_R_Open").gameObject.SetActive(true);
            Destroy(GameObject.Find("Door_right").GetComponent<Rigidbody2D>());
            GameObject.Find("Door_right").GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(GameObject.Find("Door_right").GetComponent<Rigidbody2D>());
            //Debug.Log("Opend Right");
        }

    }

    public void CloseTheDoor()//���� �ؿ���ͼ�� && ��Ӹ���
    {
        if (LeftHasRoom)
        {
            transform.Find("Door_left").Find("Door_L_Open").gameObject.SetActive(false);
            GameObject.Find("Door_left").GetComponent<BoxCollider2D>().enabled = true;
            if (GameObject.Find("Door_left").GetComponent<Rigidbody2D>() == null)
            {
                GameObject.Find("Door_left").AddComponent<Rigidbody2D>();
                GameObject.Find("Door_left").GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
        if (UpHasRoom)
        {
            transform.Find("Door_up").Find("Door_U_Open").gameObject.SetActive(false);
            GameObject.Find("Door_up").GetComponent<BoxCollider2D>().enabled = true;
            if (GameObject.Find("Door_up").GetComponent<Rigidbody2D>() == null)
            {
                GameObject.Find("Door_up").AddComponent<Rigidbody2D>();
                GameObject.Find("Door_up").GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
        if (DownHasRoom)
        {
            transform.Find("Door_down").Find("Door_D_Open").gameObject.SetActive(false);
            GameObject.Find("Door_down").GetComponent<BoxCollider2D>().enabled = true;
            if (GameObject.Find("Door_down").GetComponent<Rigidbody2D>() == null)
            {
                GameObject.Find("Door_down").AddComponent<Rigidbody2D>();
                GameObject.Find("Door_down").GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
        if (RightHasRoom)
        {
            transform.Find("Door_right").Find("Door_R_Open").gameObject.SetActive(false);
            GameObject.Find("Door_right").GetComponent<BoxCollider2D>().enabled = true;
            if (GameObject.Find("Door_right").GetComponent<Rigidbody2D>() == null)
            {
                GameObject.Find("Door_right").AddComponent<Rigidbody2D>();
                GameObject.Find("Door_right").GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
    }
}
