using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace EasyInventory.Handler {


    [AddComponentMenu("Easy Inventory/Handler/Drag Handler")]
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [Tooltip("The item currently being dragged")]
        public static GameObject item;

        private Vector2 oldPosition;
        private Transform oldParent;
        private CanvasGroup canvasGroup;

        public void Start() {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            item = gameObject;
            oldPosition = gameObject.transform.position;
            oldParent = transform.parent;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            item = null;
            if (transform.parent == oldParent) {
                transform.position = oldPosition;
            }
            canvasGroup.blocksRaycasts = true;
        }
    }
}
