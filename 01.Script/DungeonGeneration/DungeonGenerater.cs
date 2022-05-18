using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerater : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start(){
        dungeonRooms=DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
        //foreach (Room room in RoomController.instance.loadedRooms)room.RemoveUnconnectedDoors();
    }

    private void SpawnRooms(IEnumerable<Vector2Int>rooms){
        RoomController.instance.LoadRoom("Start",0,0);
        foreach(Vector2Int roomLocation in rooms){
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(),roomLocation.x, roomLocation.y);
        }
    }
}
