using UnityEngine;
using System.Collections;
using Collect.Items;
using UnityEngine.EventSystems;

namespace Collect.Slots {

    [AddComponentMenu("Collect/Slots/Slot With Type")]
    public class SlotWithType : Slot {

        [Tooltip("Only allows items to be placed if they are this type")]
        public ItemType Type;

        /**
         *  When this slot gets an item dropped on it,
         *  it verifies that it is of the same ItemType.
         *  If so, it adds the item to the slot. If not,
         *  it uses the eventData as the drop was successful.
         *
         **/
        public override void OnDrop(PointerEventData eventData) {

            DraggableItemType itemType = Draggable.DraggedItem.GetComponent<DraggableItemType>();

            if (itemType == null) {
                eventData.Use();
                return;
            }

            if (itemType.ItemType == Type) {
                base.OnDrop(eventData);
            } else {
                eventData.Use();
            }
        }
    }
}
