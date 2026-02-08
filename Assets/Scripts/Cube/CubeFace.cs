using UnityEngine;

public enum CubeFace
{
    Up,
    Down,
    Left,
    Right,
    Front,
    Back
}

public static class CubieFace
{
    private static Material sharedMat;

    public static void Create(
        Transform parent,
        Vector3 localPos,
        Quaternion localRot,
        Color color)
    {
        GameObject face = GameObject.CreatePrimitive(PrimitiveType.Cube);
        face.transform.SetParent(parent, false);
        face.transform.localPosition = localPos;
        face.transform.localRotation = localRot;
        face.transform.localScale = new Vector3(0.96f, 0.96f, 0.08f);

        var mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        face.GetComponent<MeshRenderer>().material = mat;

        Object.Destroy(face.GetComponent<Collider>());
    }
}