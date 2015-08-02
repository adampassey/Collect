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
        private Slot slot;
        public Slot Slot {
            get { return slot; }
            set { slot = value; }
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
            slot = transform.parent.gameObject.GetComponent<Slot>();
            if (slot != null) {
                slot.item = this;
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
            slot.item = null;
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

            //  if it's still in the slot, put it back
            //  where it should be
            if (transform.parent == slot.transform) {

                //  TODO: This is currently getting called every time
                ItemDropEventManager.TriggerItemDidDrop(gameObject, eventData);

                transform.position = slot.transform.position;
                slot.item = this;
            }
            canvasGroup.blocksRaycasts = true;
        }
    }
}
