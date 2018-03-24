using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MouseTileSelector : MonoBehaviour
{
    public Vector3Int mouseTilePos;
    public Vector2 offset;
    public Tilemap tilemap;
    public TileBase tileToChangeTo;
    public TerrainEffect terrEffectToApply;
    private List<Vector3Int> tempChangeTiles = new List<Vector3Int>();


    public Camera cam;
    public void FixedUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseTilePos = new Vector3Int(Mathf.RoundToInt(mousePos.x + offset.x), Mathf.RoundToInt(mousePos.y + offset.y), 0);
        if (tilemap.GetTile(mouseTilePos) != null && tilemap.GetTile(mouseTilePos).GetType().ToString() == "TerrainTile")
        {
            //Debug.Log(tilemap.GetTile(mouseTilePos).GetType().ToString());
            if (Input.GetKeyDown(KeyCode.Mouse0) && !tempChangeTiles.Contains(mouseTilePos))
            {
                //Debug.Log("Added " + mouseTilePos);
                tempChangeTiles.Add(mouseTilePos);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (Vector3Int tiles in tempChangeTiles)
            {
                //Debug.Log(tiles + " " + tempChangeTiles.Count);
                ApplyTerrainEffect((TerrainTile)tilemap.GetTile(mouseTilePos), terrEffectToApply);
            }
        }
    }
    public void ChangeTilePermanently(TileBase tile)
    {
        tilemap.SetTile(mouseTilePos, tile);
        tilemap.RefreshTile(mouseTilePos);
    }

    public void ApplyTerrainEffect(TerrainTile tile, TerrainEffect toApply, float duration = 0f)
    {
        tile.currentEffect = toApply;
        Debug.Log(tile.currentEffect);
    }
}