using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector2 v1;
    [SerializeField] private Vector2 v2;

    private void Update()
    {
        //float angle = Vector2.Angle(startPos.normalized, currentPos.normalized);
        float angle = Vector2.Angle(v1, v2);

        Debug.Log(angle);
    }
}
