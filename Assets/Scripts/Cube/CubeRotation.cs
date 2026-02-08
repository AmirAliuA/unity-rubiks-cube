using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 300f;

    private CubeGenerator generator;
    private bool rotating;

    private void Awake()
    {
        generator = GetComponent<CubeGenerator>();
    }

    public void RotateLayer(Vector3Int axis, int layer, int direction)
    {
        if (rotating) return;
        StartCoroutine(RotateRoutine(axis, layer, direction));
    }

    private IEnumerator RotateRoutine(Vector3Int axis, int layer, int direction)
    {
        rotating = true;

        GameObject pivot = new("RotationPivot");
        pivot.transform.SetParent(transform, false);

        var layerCubies = generator.Cubies
            .Where(c =>
                (axis.x != 0 && c.GridPosition.x == layer) ||
                (axis.y != 0 && c.GridPosition.y == layer) ||
                (axis.z != 0 && c.GridPosition.z == layer))
            .ToList();

        foreach (var c in layerCubies)
            c.transform.SetParent(pivot.transform);

        float rotated = 0f;
        float target = 90f;

        while (rotated < target)
        {
            float step = rotationSpeed * Time.deltaTime;
            pivot.transform.Rotate(axis * direction, step, Space.Self);
            rotated += step;
            yield return null;
        }

        pivot.transform.rotation = Quaternion.identity;

        foreach (var c in layerCubies)
        {
            c.transform.SetParent(transform);
            c.SetGridPosition(RotateGrid(c.GridPosition, axis, direction));
            c.transform.localPosition = (Vector3)c.GridPosition * generator.Spacing;
        }

        Destroy(pivot);
        rotating = false;
    }

    private Vector3Int RotateGrid(Vector3Int p, Vector3Int axis, int dir)
    {
        if (axis.x != 0) return new Vector3Int(p.x, -dir * p.z, dir * p.y);
        if (axis.y != 0) return new Vector3Int(dir * p.z, p.y, -dir * p.x);
        return new Vector3Int(-dir * p.y, dir * p.x, p.z);
    }
}