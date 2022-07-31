using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Material baseColor, offsetColor;
    [SerializeField] MeshRenderer sprite;

    GameObject characterOnTile;

    public void Init(bool isOffSet)
    {
        sprite.material = isOffSet ? offsetColor : baseColor;
    }

 
}