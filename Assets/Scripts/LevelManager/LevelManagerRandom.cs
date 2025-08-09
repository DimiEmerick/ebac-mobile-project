using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManagerRandom : MonoBehaviour
{
    public float timeBetweenTiles = .3f;
    public float tileSpawnAnimationTime = .5f;
    public Transform container;
    public List<SOLevelTileSetup> levelTileBaseSetups;

    [SerializeField] private int _index;
    [SerializeField] private List<LevelTileBase> _spawnedTiles = new List<LevelTileBase>();
    private SOLevelTileSetup _currentSetup;

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
            spawnedTile.transform.DOMoveZ(lastTile.endTile.position.z - 3, .2f);
            spawnedTile.transform.DOMoveZ(lastTile.endTile.position.z, tileSpawnAnimationTime).SetEase(Ease.OutBounce);
        }
        else
        {
            spawnedTile.transform.position = Vector3.zero;
        }
        foreach(var piece in spawnedTile.GetComponentsInChildren<ArtPiece>())
        {
            piece.ChangePiece(ArtManager.Instance.GetSetupByType(_currentSetup.artType).gameObject);
        }
        _spawnedTiles.Add(spawnedTile);
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    private void CleanSpawnedTiles()
    {
        for(int i = _spawnedTiles.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedTiles[i].gameObject);
        }
        _spawnedTiles.Clear();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(CreateLevelTileCoroutine());
        }
    }

    IEnumerator CreateLevelTileCoroutine()
    {
        CleanSpawnedTiles();

        if (_currentSetup != null)
        {
            _index++;

            if(_index >= levelTileBaseSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentSetup = levelTileBaseSetups[_index];
        ColorManager.Instance.ChangeColorByType(_currentSetup.artType);

        for (int i = 0; i < _currentSetup.tilesNumberStart; i++)
        {
            CreateLevelTile(_currentSetup.levelTilesStart);
            yield return new WaitForSeconds(timeBetweenTiles);
        }

        for (int i = 0; i < _currentSetup.tilesNumber; i++)
        {
            CreateLevelTile(_currentSetup.levelTiles);
            yield return new WaitForSeconds(timeBetweenTiles);
        }

        for (int i = 0; i < _currentSetup.tilesNumberEnd; i++)
        {
            CreateLevelTile(_currentSetup.levelTilesEnd);
            yield return new WaitForSeconds(timeBetweenTiles);
        }
    }
}
