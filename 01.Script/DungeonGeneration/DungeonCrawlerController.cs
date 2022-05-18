using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    top = 0,
    left = 1,
    down = 2,
    right =3
};

public class DungeonCrawlerController : MonoBehaviour
{
   public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
   private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>{
       {Direction.top, Vector2Int.up},
       {Direction.left, Vector2Int.left},
       {Direction.right, Vector2Int.right},
       {Direction.down, Vector2Int.down}
   };

   public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData){
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();   

        for(int i = 0; i < dungeonData.numberOfCrawlers; i++){
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin,dungeonData.iterationMax);

        for(int i = 0; i <iterations; i++){
            foreach(DungeonCrawler dungeonCrawler in dungeonCrawlers){
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
   }
}
