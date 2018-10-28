using UnityEngine;
using DragDirection = Enums.DragDirection;

public class GameUtils {
    public static DragDirection GetDragDirection(Vector2 dragVector) {
        dragVector = dragVector.normalized;

        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        DragDirection draggedDir;

        if (positiveX > positiveY) {
            draggedDir = dragVector.x > 0 ? DragDirection.Right : DragDirection.Left;
        }
        else {
            draggedDir = dragVector.y > 0 ? DragDirection.Up : DragDirection.Down;
        }

        return draggedDir;
    }
}