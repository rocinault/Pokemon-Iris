namespace Voltorb
{
    public abstract class Menu : Graphic
    {
        private void OnEnable()
        {
            AddListeners();
        }

        protected virtual void AddListeners()
        {

        }

        private void OnDisable()
        {
            RemoveListeners();    
        }

        protected virtual void RemoveListeners()
        {

        }
    }

    public abstract class Menu<T> : Graphic<T> where T : GraphicProperties
    {
        private void OnEnable()
        {
            AddListeners();
        }

        protected virtual void AddListeners()
        {

        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        protected virtual void RemoveListeners()
        {

        }
    }
}
