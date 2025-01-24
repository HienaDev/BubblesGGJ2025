using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SphereMeshGenerator : MonoBehaviour
{
    [SerializeField] private Material material;

    private MeshRenderer meshRenderer;

    [Range(2, 256)] public int longitudeSegments = 24;
    [Range(2, 128)] public int latitudeSegments = 16;
    public float radius = 1f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        GenerateSphere();
    }

    private void GenerateSphere()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        int vertexCount = (longitudeSegments + 1) * (latitudeSegments + 1);
        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];

        int[] triangles = new int[longitudeSegments * latitudeSegments * 6];

        int vertIndex = 0;
        int triIndex = 0;

        for (int lat = 0; lat <= latitudeSegments; lat++)
        {
            float latAngle = Mathf.PI * lat / latitudeSegments; // From 0 to PI
            float y = Mathf.Cos(latAngle);
            float ringRadius = Mathf.Sin(latAngle);

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float lonAngle = 2 * Mathf.PI * lon / longitudeSegments; // From 0 to 2*PI
                float x = ringRadius * Mathf.Cos(lonAngle);
                float z = ringRadius * Mathf.Sin(lonAngle);

                vertices[vertIndex] = new Vector3(x, y, z) * radius;
                normals[vertIndex] = vertices[vertIndex].normalized;

                // Correct UV mapping for consistent texture wrapping
                uv[vertIndex] = new Vector2((float)lon / longitudeSegments, 1 - (float)lat / latitudeSegments);

                if (lat < latitudeSegments && lon < longitudeSegments)
                {
                    int current = vertIndex;
                    int next = vertIndex + longitudeSegments + 1;

                    // First triangle
                    triangles[triIndex++] = current;
                    triangles[triIndex++] = next;
                    triangles[triIndex++] = current + 1;

                    // Second triangle
                    triangles[triIndex++] = current + 1;
                    triangles[triIndex++] = next;
                    triangles[triIndex++] = next + 1;
                }

                vertIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;

        // Assign material to the mesh renderer
        if (material != null)
        {
            meshRenderer.material = material;
        }
    }
}
