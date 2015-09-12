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

        //  The Rect Transform of this object
        //  Used to determine bounds
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

        /**
         *  If an item is not being dragged (and necessary
         *  prefabs/components are set), this will use the
         *  `TooltipFactory` to create a new Tooltip
         *  at the location around this objects `RectTransform`
         **/
        public void OnPointerEnter(PointerEventData eventData) {
            if (tooltipPrefab == null || rectTransform == null) {
                return;
            }

            //  don't show tooltip if an item is being dragged
            if (Draggable.DraggedItem != null) {
                return;
            }

            tooltip = TooltipFactory.Create(tooltipPrefab, text, rectTransform);
        }

        /**
         *  Hide the tooltip (which automatically gets destroyed)
         *  and set the current tooltip to `null`.
         **/
        public void OnPointerExit(PointerEventData eventData) {
            if (tooltip == null) {
                return;
            }

            //  TODO: this is gross
            tooltip.Hide();
            tooltip = null;
        }
    }
}
