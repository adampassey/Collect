using UnityEngine;
using System.Collections;

using EasyInventory.Slots;

namespace EasyInventory.Containers {

    public interface Container {

        void Open();

        void Close();

        void Toggle();

        void Add(GameObject item);

        GameObject Remove(Slot slot);

        GameObject Remove(GameObject item);
    }
}
