using UnityEngine;

public class BiomeConnector : MonoBehaviour
{
    public enum ConnectorType { Left, Right }
    
    [Header("Connector Settings")]
    public ConnectorType connectorType;
    
    // Visual helper in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        
        // Draw arrow showing direction
        Vector3 direction = connectorType == ConnectorType.Left ? Vector3.left : Vector3.right;
        Gizmos.DrawLine(transform.position, transform.position + direction * 0.5f);
    }
}