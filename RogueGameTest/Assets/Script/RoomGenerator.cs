using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{

    [Header("RoomInformation")]
    public GameObject roomPrefab;
    public int roomNum = 7;
    public Color StartRoomColor, EndRoomColor, PropsRoomColor;
    private GameObject EndRoom;
    private GameObject PropsRoom;

    [Header("LocalControl")]
    public Transform GeneratorPoint;
    public float xOffset = 18;
    public float yOffset = 10;
    public LayerMask RoomLayer;
    public int MaxStep;
    public int HalfStep;

    public List<Room> rooms = new List<Room>();
    public List<GameObject> FarRooms = new List<GameObject>();
    public List<GameObject> SecFarRooms = new List<GameObject>();
    public List<GameObject> OneDoorRooms = new List<GameObject>();
    public List<GameObject> HalfFarRooms = new List<GameObject>();

    public enum Direction {Up, Down, Left, Right};
    public Direction direction;
    public WallType wallType;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNum; i++)
        {
            rooms.Add(Instantiate(roomPrefab, GeneratorPoint.position, Quaternion.identity).GetComponent<Room>());
            ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = StartRoomColor;
        //EndRoom = rooms[0].gameObject;
        
        //find end room
        foreach (Room room in rooms)
        {
            SetUpRoom(room,room.transform.position);
            /*
            if (room.transform.position.sqrMagnitude > EndRoom.transform.position.sqrMagnitude)
            {
                EndRoom = room.gameObject;
            }
            */
        }

        FindEndRoom();
        FindPropsRoom();
        PropsRoom.GetComponent<SpriteRenderer>().color = PropsRoomColor;
        EndRoom.GetComponent<SpriteRenderer>().color = EndRoomColor;
    }

    // Update is called once per frame
    void Update()
    {
        //测试生成效果
        /* 
         if (Input.anyKeyDown)
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         }
        */
    }

    public void ChangePointPos()
    {
        
        do
        {
            direction = (Direction)UnityEngine.Random.Range(0, 4);
            switch (direction)
            {
                case Direction.Up:
                    GeneratorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.Down:
                    GeneratorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.Left:
                    GeneratorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.Right:
                    GeneratorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(GeneratorPoint.position,0.2f,RoomLayer));
    }

    public void SetUpRoom(Room newRoom, Vector3 RoomPosition)
    {
        newRoom.UpHasRoom = Physics2D.OverlapCircle(RoomPosition + new Vector3(0, yOffset, 0), 0.2f, RoomLayer);
        newRoom.DownHasRoom = Physics2D.OverlapCircle(RoomPosition + new Vector3(0, -yOffset, 0), 0.2f, RoomLayer);
        newRoom.LeftHasRoom = Physics2D.OverlapCircle(RoomPosition + new Vector3(-xOffset, 0, 0), 0.2f, RoomLayer);
        newRoom.RightHasRoom = Physics2D.OverlapCircle(RoomPosition + new Vector3(xOffset, 0, 0), 0.2f, RoomLayer);

        newRoom.UpdateRoom(xOffset, yOffset);

        switch(newRoom.DoorNum) 
        {
            case 1:
                if (newRoom.UpHasRoom)
                {
                    Instantiate(wallType.OneUp, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.DownHasRoom)
                {
                    Instantiate(wallType.OneDown, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.LeftHasRoom)
                {
                    Instantiate(wallType.OneLeft, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.RightHasRoom)
                {
                    Instantiate(wallType.OneRight, RoomPosition, Quaternion.identity);
                }
                break;
            case 2:
                if (newRoom.UpHasRoom && newRoom.DownHasRoom)
                {
                    Instantiate(wallType.TwoUD, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.UpHasRoom && newRoom.LeftHasRoom)
                {
                    Instantiate(wallType.TwoUL, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.UpHasRoom && newRoom.RightHasRoom)
                {
                    Instantiate(wallType.TwoUR, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.RightHasRoom && newRoom.LeftHasRoom)
                {
                    Instantiate(wallType.TwoRL, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.RightHasRoom && newRoom.DownHasRoom)
                {
                    Instantiate(wallType.TwoRD, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.DownHasRoom && newRoom.LeftHasRoom)
                {
                    Instantiate(wallType.TwoLD, RoomPosition, Quaternion.identity);
                }
                break;
            case 3:
                if (newRoom.UpHasRoom && newRoom.DownHasRoom && newRoom.LeftHasRoom) 
                {
                    Instantiate(wallType.ThrULD, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.UpHasRoom && newRoom.DownHasRoom && newRoom.RightHasRoom)
                {
                    Instantiate(wallType.ThrURD, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.LeftHasRoom && newRoom.DownHasRoom && newRoom.RightHasRoom)
                {
                    Instantiate(wallType.ThrRLD, RoomPosition, Quaternion.identity);
                }
                else if (newRoom.LeftHasRoom && newRoom.UpHasRoom && newRoom.RightHasRoom)
                {
                    Instantiate(wallType.ThrURL, RoomPosition, Quaternion.identity);
                }
                break;
            case 4:
                if (newRoom.UpHasRoom && newRoom.DownHasRoom && newRoom.LeftHasRoom && newRoom.RightHasRoom)
                {
                    Instantiate(wallType.FouURLD, RoomPosition, Quaternion.identity);
                }
                break;

        }
    }

    public void FindEndRoom()
    {
        for (int i = 0; i <rooms.Count; i++)
        {
            if (rooms[i].StepToStart > MaxStep) 
            {
                MaxStep = rooms[i].StepToStart;
            }
        }

        foreach (Room room in rooms)
        {
            if (room.StepToStart == MaxStep)
            {
                FarRooms.Add(room.gameObject);
            }
            else if (room.StepToStart == MaxStep-1)
            {
                SecFarRooms.Add(room.gameObject);
            }
        }

        for (int i = 0;i < FarRooms.Count;i++) 
        {
            if (FarRooms[i].GetComponent<Room>().DoorNum == 1)
            {
                OneDoorRooms.Add(FarRooms[i]);
            }
        }
        for (int i = 0; i < SecFarRooms.Count; i++)
        {
            if (SecFarRooms[i].GetComponent<Room>().DoorNum == 1)
            {
                OneDoorRooms.Add(SecFarRooms[i]);
            }
        }

        if (OneDoorRooms.Count != 0)
        {
            EndRoom = OneDoorRooms[UnityEngine.Random.Range(0, OneDoorRooms.Count)];
        }
        else
        {
            EndRoom = FarRooms[UnityEngine.Random.Range(0, OneDoorRooms.Count)];
        }
    }
    public void FindPropsRoom()
    {
        HalfStep = MaxStep / 2+1;
        foreach (Room room in rooms)
        {
            if ((room.StepToStart == HalfStep || room.StepToStart == HalfStep-1) && room.StepToStart !=0)
            {
                HalfFarRooms.Add(room.gameObject);
            }
        }
        if (HalfFarRooms.Count != 0)
        {
            PropsRoom = HalfFarRooms[UnityEngine.Random.Range(0, HalfFarRooms.Count)];
        }
        else
        {
            PropsRoom = rooms[2].gameObject;
        }
    }
}



[System.Serializable]
public class WallType
{
    public GameObject OneLeft, OneRight, OneUp, OneDown,
                                    TwoUR, TwoUD, TwoUL, TwoRL, TwoLD, TwoRD,
                                    ThrURL, ThrURD, ThrULD, ThrRLD,
                                    FouURLD;
}

