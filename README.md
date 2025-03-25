
# Movement Grip

Move objects along an axis or a plane with VRC Pickups in a synced fashion with acceptable interpolation.

# Installing

Head to my [VCC Listing](https://jansharp.github.io/vrc/vcclisting.xhtml) and follow the instructions there.

# Features

- Optionally limiting the movement to any combination of the 3 axis
- Extra support for VR to use the hand position to more accurately move the target object
  - In desktop it's currently just using the position of the object with the VRC pickup, which with bigger objects can get pretty inaccurate
- Syncing, naturally including late joiners (This one is one of the easier ones to sync)
- Custom scripts can be added to a `Listeners` array in the inspector. These scripts must define **both** of these public methods:
  - `OnBeginMovement()`
  - `OnEndMovement()`
- `SetLocalPositionOfToMove` api function on the `MovementGrip` script to set the local position of the `toMove` object and sync it

Make sure that the VRC Pickup has its Orientation set to Grip or Gun while the Exact Grip/Gun are left empty. That way the invisible pickupable object will not move towards the hand, it simply stays at the exact relative position that it was at when you picked it up.

Also make sure to set the rigid body to not use gravity and to be kinematic.

# Ideas

In regards to the desktop issue mentioned above, I've thought about ray casting from the center of the screen to find where you're actually picking up the object and use that as a relative position. Theoretically this would fix the issue, but the chance for edge cases is not exactly zero, and is also non trivial because getting the center of the screen isn't easy. Or it is and I just don't know about it.
