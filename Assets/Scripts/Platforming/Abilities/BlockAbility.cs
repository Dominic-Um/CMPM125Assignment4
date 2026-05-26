using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BlockAbility : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tileToPlace;
    [SerializeField] private int maxBlocks = 15;

    [SerializeField] private Camera cam;
    private int blocksPlaced;
    private readonly HashSet<Vector3Int> placedBlocks = new();
    private readonly Stack<Vector3Int> blockStack = new();

    private void OnEnable()
    {
        inputReader.MarkEvent += HandlePlace;
        inputReader.UndoMarkEvent += HandleRemove;
    }

    private void OnDisable()
    {
        inputReader.MarkEvent -= HandlePlace;
        inputReader.UndoMarkEvent -= HandleRemove;
    }

    private void HandlePlace()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector3Int cell = GetMouseCell();

        if (placedBlocks.Contains(cell))
        {
            RemoveBlock(cell);
            return;
        }

        PlaceBlock(cell);
    }

    private void HandleRemove()
    {
        while (blockStack.Count > 0 && !placedBlocks.Contains(blockStack.Peek()))
        {
            blockStack.Pop();
        }

        if (blockStack.Count == 0) return;

        RemoveBlock(blockStack.Pop());
    }

    private void PlaceBlock(Vector3Int cell)
    {
        if (blocksPlaced >= maxBlocks)
        {
            return;
        }
        if (tilemap.HasTile(cell))
        {
            Debug.Log("Has tile");
            return;
        }

        placedBlocks.Add(cell);
        blockStack.Push(cell);
        tilemap.SetTile(cell, tileToPlace);
        blocksPlaced++;
    }

    private void RemoveBlock(Vector3Int cell)
    {
        if (!placedBlocks.Contains(cell)) return;

        tilemap.SetTile(cell, null);
        placedBlocks.Remove(cell);
        blocksPlaced--;
    }

    private Vector3Int GetMouseCell()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z)));
        worldPos.z = 0f;
        return tilemap.WorldToCell(worldPos);
    }


}
