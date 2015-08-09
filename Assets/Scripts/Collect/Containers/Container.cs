using UnityEngine;
using System.Collections;

using Collect.Slots;

namespace Collect.Containers {

    public interface Container {

        void Open();

        void Close();

        void Toggle();

        void Add(GameObject item);

        GameObject Remove(Slot slot);

        GameObject Remove(GameObject item);

    }
}
