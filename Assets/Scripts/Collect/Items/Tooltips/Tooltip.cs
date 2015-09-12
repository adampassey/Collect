using UnityEngine;
using System;

namespace Collect.Items.Tooltips {

    public interface Tooltip {

        void Display(String text);

        void Hide();
    }
}
