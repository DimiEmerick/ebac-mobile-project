using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOLevelTileSetup : ScriptableObject
{
    public ArtManager.ArtType artType;

    [Header("Tiles")]
    public int tilesNumber = 5;
    public int tilesNumberStart = 5;
    public int tilesNumberEnd = 5;

    public List<LevelTileBase> levelTiles;
    public List<LevelTileBase> levelTilesStart;
    public List<LevelTileBase> levelTilesEnd;
}
