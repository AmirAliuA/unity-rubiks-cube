using UnityEngine;

[RequireComponent(typeof(CubeGenerator))]
public class RubiksCube : MonoBehaviour
{
    private CubeGenerator generator;

    private void Awake()
    {
        generator = GetComponent<CubeGenerator>();
    }

    private void Start()
    {
        generator.Generate();
    }
}