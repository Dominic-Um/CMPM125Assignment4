using UnityEngine;
using System.Collections.Generic;

//Put all dark areas here
public class UncoverDarkAreas : MonoBehaviour
{
    [SerializeField] private List<GameObject> darkAreas = new();

    public void HandleDarkAreas(bool on)
    {
        foreach (GameObject d in darkAreas)
        {
            d.SetActive(on);
        }
    }
}
