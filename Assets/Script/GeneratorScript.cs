using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{


    [SerializeField]
    private GameObject[] availableRooms;

    public List<GameObject> currentRooms;
    private float screenWidthInPoints;
    // Start is called before the first frame update
    void Start()
    {
        float height = 2 * Camera.main.orthographicSize;
        screenWidthInPoints = height*Camera.main.aspect;
        Debug.Log(screenWidthInPoints);
        StartCoroutine(GeneratorCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AddRoom(float farthestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        GameObject room = Instantiate(availableRooms[randomRoomIndex]);

        float roomWidth = room.transform.Find("floor").localScale.x;
        Debug.Log(roomWidth);

        float roomCenter = farthestRoomEndX + roomWidth*0.5f;

        room.transform.position = new Vector3(roomCenter, 0, 0);

        currentRooms.Add(room);
    }

    private void GeneratorRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();

        bool addRooms = true;

        float playerX = transform.position.x;

        float removeRoomX = playerX- screenWidthInPoints;

        float addRoomX = playerX+ screenWidthInPoints;

        float fartherestRoomEndX = 0;

        foreach(var room in currentRooms)
        {
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - roomWidth*0.5f;
            float roomEndX = roomStartX + roomWidth;
            
            if(roomStartX > addRoomX)
            {
                addRooms = false;
            }
            
            if(roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }

            fartherestRoomEndX = Mathf.Max(fartherestRoomEndX, roomEndX);

        }

        foreach( var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms)
        {
            AddRoom(fartherestRoomEndX);
        }
    }


    private IEnumerator GeneratorCheck()
    {
        while(true)
        {
            GeneratorRoomIfRequired();

            yield return new WaitForSeconds(0.25f);
        }
    }
}
