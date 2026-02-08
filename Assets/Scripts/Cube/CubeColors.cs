using UnityEngine;
using System.Collections.Generic;
using static CubieFace;

public static class CubeColors
{
    public static readonly Dictionary<CubeFace, Color> FaceColors = new()
    {
        { CubeFace.Up, Color.white },
        { CubeFace.Down, Color.yellow },
        { CubeFace.Left, new Color(1f, 0.5f, 0f) },   // orange
        { CubeFace.Right, Color.red },
        { CubeFace.Front, Color.blue },
        { CubeFace.Back, Color.green }
    };
}