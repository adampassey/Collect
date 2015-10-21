using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Collect.Items {

    [RequireComponent(typeof(Draggable))]
    [AddComponentMenu("Collect/Items/Draggable Item Sound")]
    public class DraggableSound : MonoBehaviour {

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
            draggable.OnBeginDragCallback += BeginDragCallback;
            draggable.OnEndDragCallback += EndDragCallback;
        }

        public void OnDestroy() {
            //  unsubscribe
            draggable.OnBeginDragCallback -= BeginDragCallback;
            draggable.OnEndDragCallback -= EndDragCallback;
        }

        /**
         *  Subscribes to `Draggable.OnDraggableBeginDrag`
         **/
        public void BeginDragCallback(PointerEventData eventData) {
            playAudioClip(PickUpSound);
        }

        /**
         *  Subscribes to `Draggable.OnDraggableEndDrag`
         **/
        public void EndDragCallback(PointerEventData eventData) {
            playAudioClip(PutDownSound);
        }

        /**
         *  Play an `AudioClip` at this objects' location
         **/
        private void playAudioClip(AudioClip audioClip) {
            if (audioClip != null) {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}
