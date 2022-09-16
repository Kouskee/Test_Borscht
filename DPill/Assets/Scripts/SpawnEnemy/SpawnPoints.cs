using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
