using UnityEngine;

public interface SizeState {

    // called when the small pickup is picked up
    void onSmallPickup();
    void onLargePickup();

    void eject();
}
