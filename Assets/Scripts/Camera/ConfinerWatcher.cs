using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConfinerWatcher : MonoBehaviour
{
    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Bird>(out _))
            CameraManager.Instance.ResetToInitial();
    }
}
