using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    public Vector2 Size => size;
    
    [Header("Prefab")]
    [SerializeField] private GameObject grassPrefab;

    [Header("Zone")]
    [SerializeField] private Vector2 size = new Vector2(5, 5);

    [Header("Spawn Settings")]
    [SerializeField] private int density = 50;
    [SerializeField] private Vector2 scaleRange = new Vector2(0.8f, 1.3f);

    private void Start()
    {
        GenerateGrass();
    }

    private void GenerateGrass()
    {
        int total = Mathf.RoundToInt(size.x * size.y * density);

        for (int i = 0; i < total; i++)
        {
            float x = Random.Range(-size.x / 2, size.x / 2);
            float z = Random.Range(-size.y / 2, size.y / 2);

            Vector3 pos = new Vector3(
                transform.position.x + x,
                transform.position.y,
                transform.position.z + z
            );

            GameObject grass = Instantiate(grassPrefab, pos, Quaternion.identity, transform);

            grass.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            float scale = Random.Range(scaleRange.x, scaleRange.y);
            grass.transform.localScale = Vector3.one * scale;
        }
    }

    public void EnableGrass(bool state)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, 0.1f, size.y));
    }
}