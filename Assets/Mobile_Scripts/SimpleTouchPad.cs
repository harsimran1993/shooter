using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake () {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown (PointerEventData data) {
		//set a start point on touch.
        if (!touched) {
            touched = true;
            pointerID = data.pointerId;
            origin = data.position;
        }
    }

    public void OnDrag (PointerEventData data) {
		//compare dfference between our start point and current point
        if (data.pointerId == pointerID) {
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
    }

    public void OnPointerUp (PointerEventData data) {
        if (data.pointerId == pointerID) {
            direction = Vector3.zero;
            touched = false;
        }
    }

    public Vector2 GetDirection () {
        smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}