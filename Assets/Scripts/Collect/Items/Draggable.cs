using UnityEngine;
using UnityEngine.EventSystems;

using Collect.Slots;
using Collect.Events;
using Collect.Static.Inputs;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Draggable Item")]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

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

        //  event that gets fired when a drag begins on this item
        public delegate void OnDragBegin(PointerEventData eventData);
        public event OnDragBegin OnBeginDragCallback;

        //  event that gets fired when a drag ends on this item
        public delegate void OnDragEnd(PointerEventData eventData);
        public event OnDragEnd OnEndDragCallback;

        private CanvasGroup canvasGroup;
        private Canvas canvas;
        private bool beingDragged = false;

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

            if (canvas == null) {
                canvas = getParentCanvas();
            }
        }

        /**
         *  If `beingDragged` is (bool) true, we'll continue
         *  to follow mouse position.
         **/
        public void Update() {
            if (beingDragged) {
                followMouse(Input.mousePosition);

                //  this is for invalid location drops
                if (Input.GetButtonDown(InputName.General.FIRE) &&
                    !EventSystem.current.IsPointerOverGameObject()) {
                    PointerEventData data = new PointerEventData(EventSystem.current);
                    OnEndDrag(data);
                }
            }
        }

        /**
         *  This event is fired when the item starts being
         *  dragged. Sets up this specific item to be dragged
         *  and turns off raycasts on this object.
         *
         **/
        public void OnBeginDrag(PointerEventData eventData) {
            if ((eventData != null && eventData.used) || DraggedItem != null) {
                return;
            }

            if (OnBeginDragCallback != null) {
                OnBeginDragCallback(eventData);
            }

            DraggedItem = this;
            canvasGroup.blocksRaycasts = false;

            oldSlot = GetComponentInParent<Slot>();
            oldSlot.RemoveItem();

            //  place it in the parent canvas so
            //  it renders above everything else
            transform.SetParent(canvas.transform);

            beingDragged = true;

            //  trigger the item did get picked up event
            ItemEventManager.TriggerItemDidPickup(gameObject, eventData);
        }

        /**
         *  When this draggable is clicked, we'll either
         *  initiate dragging with `OnBeginDrag` or we'll
         *  notify the parent slot that something is being
         *  dropped on it.
         **/
        public void OnPointerClick(PointerEventData eventData) {
            if (eventData != null && eventData.used) {
                return;
            }

            if (DraggedItem == null) {
                OnBeginDrag(eventData);
            } else {
                Slot slot = GetComponentInParent<Slot>();
                slot.OnDrop(eventData);
            }
        }

        /**
         *  While this object is being dragged it will
         *  follow the position of the event. Will only
         *  follow the mouse if this is the item that is
         *  registered as being dragged.
         *
         **/
        public void OnDrag(PointerEventData eventData) {
            if (DraggedItem == this) {
                followMouse(eventData.position);
            }
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
            //  OnEndDrag can be triggered via mouse down
            //  or mouse up, if there is no currently dragged
            //  item the event has already been fired
            if (DraggedItem == null) {
                return;
            }

            DraggedItem = null;

            //  if this item is still in the canvas transform
            //  that means the drop was unsuccessful- trigger
            //  the ItemDidDrop event
            if (transform.parent == canvas.transform) {

                transform.position = oldSlot.transform.position;
                transform.SetParent(oldSlot.transform);
                oldSlot.AddItem(this);

                if (!eventData.used) {
                    ItemEventManager.TriggerItemDidInvalidDrop(gameObject, oldSlot, eventData);
                }
            }

            oldSlot = null;
            canvasGroup.blocksRaycasts = true;

            beingDragged = false;

            //  trigger necessary events
            if (OnEndDragCallback != null) {
                OnEndDragCallback(eventData);
            }

            ItemEventManager.TriggerItemDidDrop(gameObject, eventData);
        }

        /**
         *  Convenience method to begin drag without having
         *  to initiate the `OnBeginDrag` event
         **/
        public void BeginDrag() {
            beingDragged = true;
        }

        /**
         *  Convenience method to end a drag without
         *  having to initiate the `OnEndDrag` event
         **/
        public void EndDrag() {
            beingDragged = false;
        }

        /**
         *  Whether or not this item is
         *  currently being dragged
         **/
        public bool IsBeingDragged() {
            return beingDragged;
        }

        /**
         *  Reposition this object to the specified
         *  `Vector3`. Convenient for both manual and
         *  event-driven following of position
         **/
        private void followMouse(Vector3 position) {
            transform.position = position;
        }

        /**
         *  Get the parent Canvas of this object
         **/
        private Canvas getParentCanvas() {
            return GetComponentInParent<Canvas>();
        }
    }
}
