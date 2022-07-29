using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int width = 9, height = 5;

    [SerializeField] private GameObject tile;
    [SerializeField] private float cameraOffsetX = 0.5f, cameraOffsetY = 0.5f;
    void Start()
    {
        for (float x = 0; x < width * 1f; x += 1f)
        {
            for (float y = 0; y < height * 1f; y += 1f)
            {
                bool isOffset = ((x / 1f) + (y / 1f)) % 2 == 1;

                GameObject spawnedTile = Instantiate(tile, new Vector3(x,0,y), Quaternion.identity);

                spawnedTile.transform.parent = transform;
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.GetComponent<Tile>().Init(isOffset);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
