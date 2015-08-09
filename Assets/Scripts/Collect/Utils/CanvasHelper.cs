using UnityEngine;
using System.Collections;

namespace Collect.Utils {

    public class CanvasHelper {

        private static Canvas CurrentCanvas;
        private static readonly string canvasName = "Canvas";

        public static Canvas GetCanvas() {
            if (CurrentCanvas != null) {
                return CurrentCanvas;
            }

            GameObject canvasObject = GameObject.Find(canvasName);
            CurrentCanvas = canvasObject.GetComponent<Canvas>();
            return CurrentCanvas;
        }
    }
}
