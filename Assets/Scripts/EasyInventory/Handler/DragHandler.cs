using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

using EasyInventory.Slots;
using EasyInventory.Events;

namespace EasyInventory.Handler {

    [AddComponentMenu("Easy Inventory/Handler/Drag Handler")]
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [Tooltip("The item currently being dragged")]
        public static DragHandler draggedItem;

        //  the parent slot
        private Slot originalSlot;
        public Slot OriginalSlot {
            get { return originalSlot; }
            set { originalSlot = value; }
        }

        private CanvasGroup canvasGroup;

        /**
         *  Will create a CanvasGroup component on this
         *  object if it does not already exist. This
         *  keeps the children aligned properly.
         *
         *  Will also retrieve the parent slot if this
         *  item starts in a slot and create a reference
         *  to itself in the slot.
         *
         **/
        public void Start() {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            //  get the current parent slot (if in a slot)
            originalSlot = transform.parent.gameObject.GetComponent<Slot>();
            if (originalSlot != null) {
                originalSlot.item = this;
            }
        }

        /**
         *  This event is fired when the item starts being
         *  dragged. Sets up this specific item to be dragged
         *  and turns off raycasts on this object.
         *
         **/
        public void OnBeginDrag(PointerEventData eventData) {
            draggedItem = this;
            canvasGroup.blocksRaycasts = false;
            originalSlot.RemoveItem();
        }

        /**
         *  While this object is being dragged it will
         *  follow the position of the event. 
         *
         **/
        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position;
        }

        /**
         *  Fired when the item stops being dragged. Will get
         *  called _after_ `OnDrop` (which `Slot` implements).
         *  This will test if the item is now inside a different
         *  slot (as `Slot` will move it if it receives an item).
         *  It also re-enables raycasts.
         *  
         **/
        public void OnEndDrag(PointerEventData eventData) {
            draggedItem = null;
            Slot currentSlot = transform.parent.gameObject.GetComponent<Slot>();

            //  IF the item has been dropped in an invalid
            //  location, this is called- triggering the
            //  ItemDidDrop event 
            if (currentSlot == originalSlot) {
                transform.position = originalSlot.transform.position;
                originalSlot.item = this;

                ItemDropEventManager.TriggerItemDidDrop(gameObject, originalSlot, eventData);
            } else {
                originalSlot = currentSlot;
            }
            canvasGroup.blocksRaycasts = true;
        }
    }
}
