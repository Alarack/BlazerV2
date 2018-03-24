using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TerrainTile")]
public class TerrainTile : Tile {

    public bool breakable;
    public TerrainEffect currentEffect;

    public TerrainTile(bool _breakable)
    {
        breakable = _breakable;
    }

}
