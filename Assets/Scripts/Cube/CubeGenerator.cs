using UnityEngine;
using System.Collections.Generic;

public class CubeGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int cubeSize = 3;
    [SerializeField] private float spacing = 1.05f;
    public float Spacing => spacing;

    private readonly List<Cubie> cubies = new();
    public IReadOnlyList<Cubie> Cubies => cubies;

    public void Generate()
    {
        Clear();

        int half = cubeSize / 2;

        for (int x = -half; x <= half; x++)
        {
            for (int y = -half; y <= half; y++)
            {
                for (int z = -half; z <= half; z++)
                {
                    CreateCubie(new Vector3Int(x, y, z));
                }
            }
        }
    }

    private void CreateCubie(Vector3Int gridPos)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = (Vector3)gridPos * spacing;

        DestroyImmediate(obj.GetComponent<Collider>());
        var renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"))
        {
            color = Color.black
        };

        Cubie cubie = obj.AddComponent<Cubie>();
        cubie.Initialize(gridPos, cubeSize);

        cubies.Add(cubie);
    }

    private void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        cubies.Clear();
    }
}