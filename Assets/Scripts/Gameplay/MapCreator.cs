using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;

    [SerializeField] private float TileOffset = 10f;

    private int MapSize;
    private int[,] Map;

    public int[,] CreateMap(int mapSize)
    {
        MapSize = mapSize;
        Map = new int[MapSize, MapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                var spawnPosition = new Vector3(i * TileOffset, 0, j * TileOffset);
                var tile = Instantiate(TilePrefab, spawnPosition, Quaternion.identity);
                tile.transform.SetParent(gameObject.transform, true);
            }
        }

        return Map;
    }
}