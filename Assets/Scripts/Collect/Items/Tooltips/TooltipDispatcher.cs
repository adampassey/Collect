using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

using Collect.Utils;

namespace Collect.Items.Tooltips {

    public class TooltipDispatcher {

        public static Tooltip Dispatch(GameObject prefab, string text, RectTransform trigger) {

            Canvas canvas = CanvasHelper.GetCanvas();
            GameObject tooltipObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            tooltipObject.transform.SetParent(canvas.transform);

            //  Move the tooltip
            RectTransform rectTransform = tooltipObject.GetComponent<RectTransform>();

            //  TODO: if the tooltip is off the screen, reposition
            //  should this be moved elsewhere? In the tooltip itself?
            Vector3 newPosition = trigger.position;
            newPosition.x += (trigger.rect.width * canvas.scaleFactor) / 2;
            newPosition.y += (trigger.rect.height * canvas.scaleFactor) / 2;
            tooltipObject.transform.position = newPosition;

            Tooltip t = tooltipObject.GetComponent<Tooltip>();
            t.Display(text);

            return t;
        }
        
    }
}
