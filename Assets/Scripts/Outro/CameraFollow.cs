using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [field: SerializeField] public float FollowSpeed { get; private set; }
    [SerializeField] private Transform _transform;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _transform.position, FollowSpeed * Time.deltaTime);
    }
}
