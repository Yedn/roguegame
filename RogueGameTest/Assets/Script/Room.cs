using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Room room;

    public GameObject DoorUp,DoorDown,DoorLeft,DoorRight;//�ĸ��������
    public int StepToStart;//�뿪ʼ�������
    public int DoorNum;//�м�����
    public bool LeftHasRoom, RightHasRoom, UpHasRoom, DownHasRoom;//�ĸ�������û����
    public int EnemyNum;//�����ڵ�������
    private void Awake()
    {
        room = GetComponent<Room>();
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

    private void OnTriggerEnter2D(Collider2D collision)//�������
    {
        if (collision.CompareTag("Player"))
        {
            CameraControllor.instance.ChangeTarget(transform);
        }
    }

    public void OpenTheDoor()
    {
        if (EnemyNum == 0)
        {
            if (LeftHasRoom)
            {
                transform.Find("Door_left").Find("Door_L_Open").gameObject.SetActive(true);
                //transform.Find("Door_L_Open").gameObject.SetActive(true);
                Destroy(transform.Find("Door_left").gameObject.GetComponent<Rigidbody2D>());
            }
            if (UpHasRoom)
            {
                transform.Find("Door_up").Find("Door_U_Open").gameObject.SetActive(true);
                //transform.Find("Door_U_Open").gameObject.SetActive(true);
                Destroy(transform.Find("Door_up").gameObject.GetComponent<Rigidbody2D>());
            }
            if (DownHasRoom)
            {
                transform.Find("Door_down").Find("Door_D_Open").gameObject.SetActive(true);
                //transform.Find("Door_D_Open").gameObject.SetActive(true);
                Destroy(transform.Find("Door_down").gameObject.GetComponent<Rigidbody2D>());
            }
            if (RightHasRoom)
            {
                transform.Find("Door_right").Find("Door_R_Open").gameObject.SetActive(true);
                //transform.Find("Door_R_Open").gameObject.SetActive(true);
                Destroy(transform.Find("Door_right").gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }//����
}
