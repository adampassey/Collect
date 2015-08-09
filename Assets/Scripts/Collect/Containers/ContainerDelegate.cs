using UnityEngine;
using System.Collections;

namespace Collect.Containers {

    public interface ContainerDelegate {

        void ItemWasAdded(GameObject item);

        void ItemWasRemoved(GameObject item);

    }
}
