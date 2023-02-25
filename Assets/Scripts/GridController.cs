using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridController : MonoBehaviour
{
    private Grid _grid;
    private Tilemap _platformTilemap;
    private List<Vector3Int> _positionsToMoves;
    private bool _movingRight = true;
    private int _moveDistance = 1;

    void Start()
    {
        _grid = GetComponent<Grid>();
        _platformTilemap = _grid.GetComponentsInChildren<Tilemap>().First(tilemap => tilemap.name == "Platform Tilemap");
        _positionsToMoves = new List<Vector3Int>();
        _positionsToMoves.Add(new Vector3Int(-5, 1, 0));
        StartCoroutine(MoveTileCoroutine());
    }

    IEnumerator MoveTileCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            // Move tiles
            _positionsToMoves.ForEach(MoveTile);
            
            // Update positions to move
            _positionsToMoves = _positionsToMoves.Select(position => new Vector3Int(position.x + (_movingRight ? 1 : -1), position.y, position.z)).ToList();

            // Reverse direction if moving out of bounds
            if (_positionsToMoves.Any(position => position.x >= _platformTilemap.cellBounds.xMax) ||
                _positionsToMoves.Any(position => position.x < _platformTilemap.cellBounds.xMin))
            {
                _movingRight = !_movingRight;
            }
        }
    }

    private void MoveTile(Vector3Int position)
    {
        var matrix = Matrix4x4.Translate(new Vector3(_movingRight ? _moveDistance : -_moveDistance, 0, 0));
        _platformTilemap.SetTransformMatrix(position, matrix);
    }
}

