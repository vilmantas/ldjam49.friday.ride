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

    private int Count = 1;

    private GameObject CurrentTile;

    private void Start()
    {
        CurrentTile = GetRandomTileInternal();
        Count = Random.Range(1, 3);

        Bounds b = new Bounds();

        foreach (Transform item in transform)
        {
            b.Encapsulate(GetTileBounds(item.gameObject));
        }

        NextPosition = new Vector3(transform.position.x, transform.position.y, b.max.z);
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
        AppendTile(Tiles[Random.Range(0, Tiles.Length)]);
    }

    public GameObject GetRandomTile()
    {
        GameObject tile;

        if (Count > 0)
        {
            tile = CurrentTile;
            Count--;
        } else
        {

            CurrentTile = GetRandomTileInternal();
            tile = CurrentTile;
            Count = Random.Range(1, 3);
        }

        return tile;
    }

    private GameObject GetRandomTileInternal()
    {
        return Tiles[Random.Range(0, Tiles.Length)];
    }

    public int Counter = 0;

    public void AppendTile(GameObject tile)
    {
        Bounds b = new Bounds();

        foreach (Transform item in transform)
        {
            b.Encapsulate(GetTileBounds(item.gameObject));
        }

        var z = new Vector3(transform.position.x, transform.position.y, b.max.z);

        var bounds = GetTileBounds(tile);
        var newLocation = new Vector3(z.x, z.y, z.z + (bounds.size.z / 2));
        Instantiate(tile, newLocation, new Quaternion(), transform);

        NextPosition.z += bounds.size.z;
        Counter++;
        if (Counter == 1) return;
        Destroy(transform.GetChild(0).gameObject);
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

        Bounds b = new Bounds();

        foreach (Transform item in transform)
        {
            b.Encapsulate(GetTileBounds(item.gameObject));
        }

        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, b.max.z), new Vector3(transform.position.x, transform.position.y, b.max.z) + (Vector3.up * 100f));
        var newBounds = GetTileBounds(CurrentTile);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, b.max.z + (newBounds.size.z / 2)), newBounds.size);
    }
}
