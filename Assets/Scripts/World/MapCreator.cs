using UnityEngine;

namespace World
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private GameObject TilePrefab;

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
                    var spawnPosition = new Vector3(i * GameSettings.TileOffset, 0, j * GameSettings.TileOffset);
                    var tile = Instantiate(TilePrefab, spawnPosition, Quaternion.identity);
                    tile.transform.SetParent(gameObject.transform, true);
                }
            }

            return Map;
        }
    }
}