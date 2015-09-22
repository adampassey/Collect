using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Collect.Items {

    [RequireComponent(typeof(Draggable))]
    [AddComponentMenu("Collect/Items/Draggable Item Sound")]
    public class DraggableSound : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

        [Tooltip("The sound played when this item is picked up")]
        public AudioClip PickUpSound;

        [Tooltip("The sound played when this item is put down")]
        public AudioClip PutDownSound;

        private Draggable draggable;

        public void Start() {
            draggable = GetComponent<Draggable>();
            if (draggable == null) {
                throw new MissingComponentException("`DraggableSound` requires a `Draggable` component to play sounds for _most_ events.");
            }

            //  subscribe to events
            draggable.OnDraggableBeginDrag += DraggableBeginDrag;
            draggable.OnDraggableEndDrag += DraggableEndDrag;
        }

        public void OnDisable() {
            //  unsubscribe
            draggable.OnDraggableBeginDrag -= DraggableBeginDrag;
            draggable.OnDraggableEndDrag -= DraggableEndDrag;
        }

        /**
         *  When the item is picked up, it will automatically
         *  played the attached PickUpSound
         *
         **/
        public void OnBeginDrag(PointerEventData eventData) {
            if (PickUpSound != null) {
                AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            }
        }

        /**
         *  When an item is dropped (successful or not), it will
         *  automatically play the PutDownSound
         *
         **/
        public void OnEndDrag(PointerEventData eventData) {
            if (PutDownSound != null) {
                AudioSource.PlayClipAtPoint(PutDownSound, transform.position);
            }
        }

        /**
         *  Subscribes to `Draggable.OnDraggableBeginDrag`
         **/
        public void DraggableBeginDrag(PointerEventData eventData) {
            OnBeginDrag(eventData);
        }

        /**
         *  Subscribes to `Draggable.OnDraggableEndDrag`
         **/
        public void DraggableEndDrag(PointerEventData eventData) {
            OnEndDrag(eventData);
        }
    }
}
