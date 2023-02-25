using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voltorb
{
    public abstract class SelectableNavigation : MonoBehaviour, IMoveHandler, ISubmitHandler
    {
        [SerializeField]
        protected Selectable[] m_Selectables;

        public int selectedIndex => m_SelectedIndex;
        protected int m_SelectedIndex;

        public virtual void SetSelectable(BaseEventData eventData, int selectedIndex)
        {
            m_SelectedIndex = selectedIndex;
            m_Selectables[m_SelectedIndex].OnSelect(eventData);
        }

        public virtual void OnMove(AxisEventData eventData)
        {
            var direction = eventData.moveDir;

            switch (direction)
            {
                case MoveDirection.Up:
                    OnMoveUp(eventData);
                    break;
                case MoveDirection.Down:
                    OnMoveDown(eventData);
                    break;
                case MoveDirection.Left:
                    OnMoveLeft(eventData);
                    break;
                case MoveDirection.Right:
                    OnMoveRight(eventData);
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnMoveUp(AxisEventData eventData)
        {

        }

        protected virtual void OnMoveDown(AxisEventData eventData)
        {

        }

        protected virtual void OnMoveLeft(AxisEventData eventData)
        {

        }

        protected virtual void OnMoveRight(AxisEventData eventData)
        {

        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            
        }

        protected bool IsTargetSelectableInteractable(int index)
        {
            return m_Selectables[index].IsInteractable();
        }
    }
}
