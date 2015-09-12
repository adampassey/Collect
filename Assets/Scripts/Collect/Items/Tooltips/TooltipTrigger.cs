using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Collect.Exceptions;
using Collect.Items.Tooltips;
using Collect.Utils;

namespace Collect.Items.Tooltips {

    [AddComponentMenu("Collect/Items/Tooltips/Tooltip Trigger")]
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [Tooltip("Tooltip prefab")]
        public GameObject tooltipPrefab;

        [TextArea(1, 100)]
        [Tooltip("Content of the tooltip")]
        public string text;

        private Tooltip tooltip;

        private RectTransform rectTransform;

        public void Start() {
            if (tooltipPrefab == null) {
                throw new MissingPrefabException("Must have a tooltip prefab attached to a `TooltipTrigger`");
            }

            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null) {
                throw new MissingComponentException("Must have a `RectTransform` attached to this object");
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (tooltipPrefab == null || rectTransform == null) {
                return;
            }

            //  don't show tooltip if an item is being dragged
            if (Draggable.DraggedItem != null) {
                return;
            }

            tooltip = TooltipDispatcher.Dispatch(tooltipPrefab, text, rectTransform);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (tooltip == null) {
                return;
            }

            tooltip.Hide();
            tooltip = null;
        }
    }
}
