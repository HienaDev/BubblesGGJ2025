using UnityEngine;

public class DebugPosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
