using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManagerRandom : MonoBehaviour
{
    public float timeBetweenTiles = .3f;
    public Transform container;
    public List<SOLevelTileSetup> levelTileBaseSetups;

    [Header("Animation")]
    public float scaleDuration = .25f;
    public float scaleTimeBetweenTiles = .1f;
    public Ease ease = Ease.OutBounce;

    [Header("Tiles List")]
    [SerializeField] private int _index;
    [SerializeField] private List<LevelTileBase> _spawnedTiles = new List<LevelTileBase>();
    private SOLevelTileSetup _currentSetup;

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        CreateLevelTileCoroutine();
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
            CreateLevelTileCoroutine();
            CoinsAnimatorManager.Instance.ClearCoins();
        }
    }

    public void CreateLevelTileCoroutine()
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
        }

        for (int i = 0; i < _currentSetup.tilesNumber; i++)
        {
            CreateLevelTile(_currentSetup.levelTiles);
        }

        for (int i = 0; i < _currentSetup.tilesNumberEnd; i++)
        {
            CreateLevelTile(_currentSetup.levelTilesEnd);
        }
        StartCoroutine(ScaleTilesByTime()); 
    }

    IEnumerator ScaleTilesByTime()
    {
        foreach(var t in _spawnedTiles)
        {
            t.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0; i < _spawnedTiles.Count; i++)
        {
            _spawnedTiles[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenTiles);
        }

        CoinsAnimatorManager.Instance.StartAnimations();
    }
}
