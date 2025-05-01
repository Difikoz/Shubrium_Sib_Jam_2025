using UnityEngine;

namespace WinterUniverse
{
    public abstract class BasicComponent : MonoBehaviour
    {
        public virtual void InitializeComponent()
        {

        }

        public virtual void DestroyComponent()
        {

        }

        public virtual void EnableComponent()
        {

        }

        public virtual void DisableComponent()
        {

        }

        public virtual void ActivateComponent()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeactivateComponent()
        {
            gameObject.SetActive(false);
        }

        public virtual void UpdateComponent()
        {

        }

        public virtual void FixedUpdateComponent()
        {

        }

        public virtual void LateUpdateComponent()
        {

        }
    }
}