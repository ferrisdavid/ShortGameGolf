using UnityEngine;


public interface Grabbable
{
    //Called when the user begins grab with this object. 
    void OnGrab(GameObject hand) { }

    //Called when the user releases this object. 
    void OnRelease() { }
}