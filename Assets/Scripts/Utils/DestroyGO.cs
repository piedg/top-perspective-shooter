using UnityEngine;

public class DestroyGO : MonoBehaviour
{
    [SerializeField] float Time;

    void OnEnable()
    {
        Destroy(gameObject, Time);
    }
}
