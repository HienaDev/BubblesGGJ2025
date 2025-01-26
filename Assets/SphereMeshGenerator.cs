using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SphereMeshGenerator : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float springStrength = 10f;
    [SerializeField] private float springDamping = 0.1f;
    [SerializeField] private float gravityForce = 9.81f;
    [SerializeField] private float movementSpeed = 5f;

    private MeshRenderer meshRenderer;

    [Range(2, 256)] public int longitudeSegments = 24;
    [Range(2, 128)] public int latitudeSegments = 16;
    public float radius = 1f;

    private GameObject[] vertexObjects;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        GenerateSphere();
    }

    private void Update()
    {
        HandleMovement();
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

                // Ensure the normals are correctly normalized
                normals[vertIndex] = new Vector3(x, y, z).normalized;

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
        mesh.normals = normals; // Assign precomputed normals
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Recalculate bounds and ensure normals are consistent
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        // Assign material to the mesh renderer
        if (material != null)
        {
            meshRenderer.material = material;
        }

        // Initialize vertex objects for physics
        InitializePhysics(vertices);
    }

    private void InitializePhysics(Vector3[] vertices)
    {
        vertexObjects = new GameObject[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            // Create a small sphere at each vertex
            GameObject vertexObject = new GameObject("Vertex_" + i);
            vertexObject.transform.position = transform.TransformPoint(vertices[i]);
            vertexObject.transform.parent = transform;

            // Add Rigidbody
            Rigidbody rb = vertexObject.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
            rb.linearDamping = 0.1f;
            rb.useGravity = false; // Gravity is simulated manually

            // Add SphereCollider
            SphereCollider collider = vertexObject.AddComponent<SphereCollider>();
            collider.radius = 0.05f;

            vertexObjects[i] = vertexObject;
        }

        // Add SpringJoints between all vertices
        for (int i = 0; i < vertexObjects.Length; i++)
        {
            for (int j = 0; j < vertexObjects.Length; j++)
            {
                if (i != j)
                {
                    SpringJoint spring = vertexObjects[i].AddComponent<SpringJoint>();
                    spring.connectedBody = vertexObjects[j].GetComponent<Rigidbody>();
                    spring.spring = springStrength;
                    spring.damper = springDamping;
                    spring.minDistance = 0;
                    spring.maxDistance = Vector3.Distance(vertexObjects[i].transform.position, vertexObjects[j].transform.position);
                }
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            inputDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputDirection = Vector3.right;
        }

        foreach (GameObject vertexObject in vertexObjects)
        {
            Rigidbody rb = vertexObject.GetComponent<Rigidbody>();

            // Apply gravity manually
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);

            // Apply movement force
            rb.AddForce(inputDirection * movementSpeed, ForceMode.Acceleration);
        }
    }
}
