using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

using Collect.Slots;
using Collect.Events;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Draggable Item")]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [Tooltip("The item currently being dragged")]
        public static Draggable DraggedItem;

        //  stores the old slot when the
        //  item is dragged so we can reset it
        //  if the drag is unsuccessful
        private Slot oldSlot;
        public Slot OldSlot {
            get { return oldSlot; }
            set { oldSlot = value; }
        }

        private CanvasGroup canvasGroup;
        private Canvas canvas;

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
        }

        /**
         *  This event is fired when the item starts being
         *  dragged. Sets up this specific item to be dragged
         *  and turns off raycasts on this object.
         *
         **/
        public void OnBeginDrag(PointerEventData eventData) {
            DraggedItem = this;
            canvasGroup.blocksRaycasts = false;

            oldSlot = GetComponentInParent<Slot>();
            oldSlot.RemoveItem();

            if (canvas == null) {
                canvas = getParentCanvas();
            }

            //  place it in the parent canvas so
            //  it renders above everything else
            transform.SetParent(canvas.transform);
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
            DraggedItem = null;

            //  if this item is still in the canvas transform
            //  that means the drop was unsuccessful- trigger
            //  the ItemDidDrop event
            if (transform.parent == canvas.transform) {

                transform.position = oldSlot.transform.position;
                transform.SetParent(oldSlot.transform);
                oldSlot.item = this;

                ItemDropEventManager.TriggerItemDidDrop(gameObject, oldSlot, eventData);
            }

            oldSlot = null;
            canvasGroup.blocksRaycasts = true;
        }

        /**
         *  Get the parent Canvas of this object
         **/
        private Canvas getParentCanvas() {
            return GetComponentInParent<Canvas>();
        }
    }
}
