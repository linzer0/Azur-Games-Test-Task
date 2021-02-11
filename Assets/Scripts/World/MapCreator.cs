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

            var position = 5 * mapSize - 5;
            var spawnPosition = new Vector3(position, 0, position);
            
            var tile = Instantiate(TilePrefab, spawnPosition, Quaternion.identity);
            tile.transform.localScale = new Vector3(mapSize, 1, mapSize);
            tile.transform.SetParent(gameObject.transform, false);

            return Map;
        }
    }
}