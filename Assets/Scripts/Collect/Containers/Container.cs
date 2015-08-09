using UnityEngine;
using System.Collections;

using Collect.Slots;

namespace Collect.Containers {

    public delegate void ItemAdded(GameObject item);
    public delegate void ItemRemoved(GameObject item);

    public interface Container {

        void Open();

        void Close();

        void Toggle();

        void Add(GameObject item);

        GameObject Remove(Slot slot);

        GameObject Remove(GameObject item);

        event ItemAdded ItemWasAdded;

        event ItemRemoved ItemWasRemoved;

    }
}
