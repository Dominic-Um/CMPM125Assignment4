using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MarkerSystem : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private int maxMarkers = 5;
    [SerializeField] private LayerMask markerLayer;
    [SerializeField] private Camera cam;

    private DetectiveMarker[] pool;
    private readonly Stack<DetectiveMarker> markersStack = new();
    private int activeMarkers;

    private void Awake()
    {
        pool = new DetectiveMarker[maxMarkers];
        for (int i = 0; i < maxMarkers; i++)
        {
            GameObject g = Instantiate(markerPrefab);
            g.SetActive(false);
            pool[i] = g.GetComponent<DetectiveMarker>();
        }
    }

    private void OnEnable()
    {
        inputReader.MarkEvent += HandleMark;
        inputReader.UndoMarkEvent += HandleUndo;
    }

    private void OnDisable()
    {
        inputReader.MarkEvent -= HandleMark;
        inputReader.UndoMarkEvent -= HandleUndo;
    }

    private void HandleMark()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z)));
        worldPos.z = 0f;

        Collider2D hit = Physics2D.OverlapPoint(worldPos, markerLayer);
        if (hit != null && hit.TryGetComponent(out DetectiveMarker existing))
        {
            RemoveMarker(existing);
            return;
        }

        if (activeMarkers >= maxMarkers) return;

        DetectiveMarker slot = GetMarker();
        if (slot == null) return;

        slot.Activate(worldPos);
        markersStack.Push(slot);
        activeMarkers++;
    }

    private void HandleUndo()
    {
        while (markersStack.Count > 0 && !markersStack.Peek().IsActive)
        {
            markersStack.Pop();
        }

        if (markersStack.Count == 0) return;

        RemoveMarker(markersStack.Pop());
    }

    private void RemoveMarker(DetectiveMarker marker)
    {
        marker.Deactivate();
        activeMarkers--;
    }


    private DetectiveMarker GetMarker()
    {
        foreach (DetectiveMarker m in pool)
        {
            if (!m.IsActive) 
            {
                return m;
            }
        }

        //send a message?
        return null;
    }
}
