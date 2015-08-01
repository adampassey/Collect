using UnityEngine;
using System.Collections;

namespace EasyInventory.Containers {

    public interface Container {

        void Open();

        void Close();

        void Toggle();

        void Add(GameObject item);
    }
}
