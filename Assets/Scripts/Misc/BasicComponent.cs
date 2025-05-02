using UnityEngine;

namespace WinterUniverse
{
    public abstract class BasicComponent : MonoBehaviour
    {
        public virtual void InitializeComponent()
        {
            //Debug.Log($"Initialize {gameObject.name}");
        }

        public virtual void DestroyComponent()
        {
            //Debug.Log($"Destroyed {gameObject.name}");
        }

        public virtual void EnableComponent()
        {
            //Debug.Log($"Enabled {gameObject.name}");
        }

        public virtual void DisableComponent()
        {
            //Debug.Log($"Disabled {gameObject.name}");
        }

        public virtual void ActivateComponent()
        {
            gameObject.SetActive(true);
            //Debug.Log($"Activated {gameObject.name}");
        }

        public virtual void DeactivateComponent()
        {
            gameObject.SetActive(false);
            //Debug.Log($"Deactivated {gameObject.name}");
        }

        public virtual void UpdateComponent()
        {
            //Debug.Log($"Updated {gameObject.name}");
        }

        public virtual void FixedUpdateComponent()
        {
            //Debug.Log($"Fixed Updated {gameObject.name}");
        }

        public virtual void LateUpdateComponent()
        {
            //Debug.Log($"Late Updated {gameObject.name}");
        }
    }
}