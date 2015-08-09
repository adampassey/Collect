using UnityEngine;
using System.Collections;

namespace Collect.Slots {

    /**
     *  Extend this interface to receive events
     *  from slots. When an item is removed
     *  or added, will notify the delegate- this
     *  object will _need_ to be set as the
     *  slot's delegate
     *
     **/
    public interface SlotDelegate {

        void ItemWasAdded(GameObject item);

        void ItemWasRemoved(GameObject item);
        
    }
}
