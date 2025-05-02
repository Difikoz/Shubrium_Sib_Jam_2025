using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : BasicComponent
    {
        [field: SerializeField] public float FollowSpeed { get; private set; }

        public override void LateUpdateComponent()
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.StaticInstance.Player.transform.position, FollowSpeed * Time.deltaTime);
        }
    }
}