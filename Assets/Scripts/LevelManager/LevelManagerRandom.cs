using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerRandom : MonoBehaviour
{
    public Transform container;

    [Header("Tiles")]
    public List<LevelTileBase> levelTiles;
    public List<LevelTileBase> levelTilesStart;
    public List<LevelTileBase> levelTilesEnd;
    public int tilesNumber = 5;
    public int tilesNumberStart = 5;
    public int tilesNumberEnd = 5;
    public float timeBetweenTiles = .3f;

    private List<LevelTileBase> _spawnedTiles;

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        StartCoroutine(CreateLevelTileCoroutine());
    }

    private void CreateLevelTile(List<LevelTileBase> list)
    {
        var tile = list[Random.Range(0, list.Count)];
        var spawnedTile = Instantiate(tile, container);
        if(_spawnedTiles.Count > 0)
        {
            var lastTile = _spawnedTiles[_spawnedTiles.Count - 1];
            spawnedTile.transform.position = lastTile.endTile.position;
        }
        _spawnedTiles.Add(spawnedTile);
    }

    IEnumerator CreateLevelTileCoroutine()
    {
        _spawnedTiles = new List<LevelTileBase>();

        for (int i = 0; i < tilesNumberStart; i++)
        {
            CreateLevelTile(levelTilesStart);
            yield return new WaitForSeconds(timeBetweenTiles);
        }

        for (int i = 0; i < tilesNumber; i++)
        {
            CreateLevelTile(levelTiles);
            yield return new WaitForSeconds(timeBetweenTiles);
        }

        for (int i = 0; i < tilesNumberEnd; i++)
        {
            CreateLevelTile(levelTilesEnd);
            yield return new WaitForSeconds(timeBetweenTiles);
        }
    }
}
