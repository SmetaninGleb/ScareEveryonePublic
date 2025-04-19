using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
        game.NavMeshSurface = surface;
    }
}
