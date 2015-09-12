using UnityEngine;
using System.Collections;
using System;

using UnityEngine.UI;

namespace Collect.Items.Tooltips {

    [AddComponentMenu("Collect/Items/Tooltips/Standard Tooltip")]
    public class StandardTooltip : MonoBehaviour, Tooltip {

        private string textValue;
        private Text text;

        // Use this for initialization
        void Start() {
            Text text = GetComponentInChildren<Text>();

            if (text == null) {
                throw new MissingComponentException("Tooltip requires a `Text` component");
            }

            text.text = textValue;
        }

        public void Display(string text) {
            textValue = text;
        }

        public void Hide() {
            Destroy(gameObject);
        }
    }
}
