using UnityEngine;

public class EnemyShooter : Enemy
{
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; 
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerTransform.position);
        
    }
}
