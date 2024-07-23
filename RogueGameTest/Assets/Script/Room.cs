using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorUp,DoorDown,DoorLeft,DoorRight;

    public int StepToStart;
    public int DoorNum;
    public bool LeftHasRoom, RightHasRoom, UpHasRoom, DownHasRoom;
    // Start is called before the first frame update
    void Start()
    {
        DoorUp.SetActive(UpHasRoom);
        DoorDown.SetActive(DownHasRoom);
        DoorRight.SetActive(RightHasRoom);
        DoorLeft.SetActive(LeftHasRoom);
    }

    public void UpdateRoom(float xOffset,float yOffset)
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
}
