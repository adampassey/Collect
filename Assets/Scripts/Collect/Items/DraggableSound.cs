using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Draggable Item Sound")]
    public class DraggableSound : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

        [Tooltip("The sound played when this item is picked up")]
        public AudioClip PickUpSound;

        [Tooltip("The sound played when this item is put down")]
        public AudioClip PutDownSound;

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
    }
}
