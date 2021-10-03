using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TileGeneratorScript : MonoBehaviour
{
    public GameObject[] Tiles;

    public bool Reset = false;

    public bool Generate = false;

    private Vector3 NextPosition = Vector3.zero;

    private void Start()
    {
        Bounds b = new Bounds();

        foreach (Transform item in transform)
        {
            b.Encapsulate(GetTileBounds(item.gameObject));
        }

        NextPosition.z += b.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Reset)
        {
            NextPosition = transform.position;
            Reset = false;
        }

        if (!Generate) return;

        Generate = false;
        AppendTile(Tiles[Random.Range(0, Tiles.Length + 1)]);
    }

    public GameObject GetRandomTile()
    {
        return Tiles[Random.Range(0, Tiles.Length + 1)];
    }

    public void AppendTile(GameObject tile)
    {
        var instance = Instantiate(tile, NextPosition, new Quaternion(), transform);

        var renderer = tile.GetComponentsInChildren<Renderer>();

        Bounds b = new Bounds();

        foreach (var item in renderer)
        {
            b.Encapsulate(item.bounds);
        }

        NextPosition.z += b.size.z;
    }

    private Bounds GetTileBounds(GameObject Tile)
    {
        var renderer = Tile.GetComponentsInChildren<Renderer>();

        Bounds b = new Bounds();

        foreach (var item in renderer)
        {
            b.Encapsulate(item.bounds);
        }

        return b;
    }

    private void OnDrawGizmos()
    {
        if (Tiles == null || Tiles.Length == 0) return;

        Gizmos.DrawWireCube(NextPosition, GetTileBounds(Tiles[0]).size);
    }
}
