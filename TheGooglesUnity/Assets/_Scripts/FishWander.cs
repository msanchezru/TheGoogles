using UnityEngine;

public class FishWander : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float speed = 1f;
    [SerializeField, Min(0f)] private float turnSpeed = 180f;
    [SerializeField, Min(0f)] private float stoppingDistance = 0.1f;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("A target transform must be assigned.", this);
            enabled = false;
        }
    }

    private void Update()
    {
        Vector3 toTarget = target.position - transform.position;
        float distance = toTarget.magnitude;

        if (distance <= stoppingDistance)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime);

        transform.position += transform.forward * (speed * Time.deltaTime);
    }
}