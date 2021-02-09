using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static int TileOffset = 10;
    public static int MaxTimeToFood = 5;
    public static int[,] Map;
    public static int MapSize => Map.GetLength(0);
    public static int AnimalSpeed = 10;
    public static int SimulationSpeed = 1;
}