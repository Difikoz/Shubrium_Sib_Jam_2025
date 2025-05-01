using System.Collections.Generic;

namespace WinterUniverse
{
    public abstract class BasicComponentHolder : BasicComponent
    {
        protected List<BasicComponent> _components;

        public override void InitializeComponent()
        {
            _components = new();
            FillComponents();
            foreach (BasicComponent component in _components)
            {
                component.InitializeComponent();
            }
        }

        public virtual void FillComponents()
        {

        }

        public override void DestroyComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.DestroyComponent();
            }
        }

        public override void EnableComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.EnableComponent();
            }
        }

        public override void DisableComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.DisableComponent();
            }
        }

        public override void ActivateComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.ActivateComponent();
            }
        }

        public override void DeactivateComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.DeactivateComponent();
            }
        }

        public override void UpdateComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.UpdateComponent();
            }
        }

        public override void FixedUpdateComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.FixedUpdateComponent();
            }
        }

        public override void LateUpdateComponent()
        {
            foreach (BasicComponent component in _components)
            {
                component.LateUpdateComponent();
            }
        }
    }
}