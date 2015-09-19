using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using Collect.Items;
using Collect.Exceptions;
using System;

namespace Collect.Slots {

    [AddComponentMenu("Collect/Slots/Slot")]
    public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler {

        //  the item in this slot
        public Draggable Item;

        //  delegate called when an item is added to this slot
        public ItemAddedToSlot ItemAddedDelegate;

        //  delegate called when an item is removed from this slot
        public ItemRemovedFromSlot ItemRemovedDelegate;

        public void Start() {
            Item = GetComponentInChildren<Draggable>();
        }

        /**
         *  When this slot is clicked it will invoke an 
         *  `OnPointerClick` event. This is useful if an item
         *  is dropped onto an empty slot (otherwise the event
         *  will be consumed by the item in the slot)
         **/
        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.used || Draggable.DraggedItem == null) {
                return;
            }

            OnDrop(eventData);
        }

        /**
         *  This event is fired when an item is dropped
         *  onto this slot. Will accept the item
         *  into the slot OR swap the item.
         *
         **/
        public virtual void OnDrop(PointerEventData eventData) {
            if (eventData.used || Draggable.DraggedItem == null) {
                return;
            }

            if (Item == null) {
                AddItem(Draggable.DraggedItem);
            } else {

                Stackable stackableItem = Draggable.DraggedItem.GetComponent<Items.Stackable>();
                Stackable slotStackableItem = Item.GetComponent<Items.Stackable>();

                //  the dropped item is stackable, as is
                //  the item in this slot. Attempt to stack.
                //  will return if successful.
                if (stackableItem != null && slotStackableItem != null) {
                    try {
                        slotStackableItem.Add(stackableItem);
                        return;
                    } catch(NotStackableException e) {
                        Debug.Log("Not able to stack this item: " + e);
                    }
                }

                //  swap the item with the item
                //  that was dropped
                Draggable otherItem = Draggable.DraggedItem;
                Slot otherSlot = otherItem.OldSlot;

                otherSlot.AddItem(RemoveItem());
                AddItem(otherItem);
            }

            Item.OnEndDrag(eventData);
            eventData.Use();
        }

        /**
         *  Remove the `DragHandler` from this slot.
         *  Will return null if no item is present.
         **/
        public Draggable RemoveItem() {
            if (Item == null) {
                return null;
            }

            Draggable oldItem = Item;
            Item = null;

            if (ItemRemovedDelegate != null) {
                ItemRemovedDelegate(oldItem.gameObject);
            }

            return oldItem;
        }

        /**
         *  Add the `DragHandler` to this slot.
         **/
         public void AddItem(Draggable item) {
            this.Item = item;
            item.transform.SetParent(transform);

            if (ItemAddedDelegate != null) {
                ItemAddedDelegate(item.gameObject);
            }
        }
    }
}
