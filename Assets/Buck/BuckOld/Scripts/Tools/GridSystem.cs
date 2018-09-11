using UnityEngine;

public class GridSystem : MonoBehaviour
{

    [SerializeField]
    float gridSize;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int yCount = Mathf.RoundToInt(position.y / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);

        Vector3 result = new Vector3((float)xCount * gridSize, (float)yCount * gridSize, (float)zCount * gridSize);

        result += transform.position;

        return result;
    }
}
