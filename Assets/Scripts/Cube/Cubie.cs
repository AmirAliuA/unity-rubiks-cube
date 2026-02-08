using UnityEngine;
using static CubieFace;

public class Cubie : MonoBehaviour
{
    public Vector3Int GridPosition { get; private set; }

    public void Initialize(Vector3Int gridPosition, int cubeSize)
    {
        GridPosition = gridPosition;
        name = $"Cubie [{gridPosition.x},{gridPosition.y},{gridPosition.z}]";

        int half = cubeSize / 2;
        float offset = 0.501f;

        if (gridPosition.y == half)
            CreateFace(CubeFace.Up, Vector3.up * offset, Quaternion.Euler(90, 0, 0));

        if (gridPosition.y == -half)
            CreateFace(CubeFace.Down, Vector3.down * offset, Quaternion.Euler(-90, 0, 0));

        if (gridPosition.x == half)
            CreateFace(CubeFace.Right, Vector3.right * offset, Quaternion.Euler(0, -90, 0));

        if (gridPosition.x == -half)
            CreateFace(CubeFace.Left, Vector3.left * offset, Quaternion.Euler(0, 90, 0));

        if (gridPosition.z == half)
            CreateFace(CubeFace.Front, Vector3.forward * offset, Quaternion.identity);

        if (gridPosition.z == -half)
            CreateFace(CubeFace.Back, Vector3.back * offset, Quaternion.Euler(0, 180, 0));
    }

    private void CreateFace(CubeFace face, Vector3 pos, Quaternion rot)
    {
        CubieFace.Create(transform, pos, rot, CubeColors.FaceColors[face]);
    }

    public void SetGridPosition(Vector3Int pos)
    {
        GridPosition = pos;
    }
}