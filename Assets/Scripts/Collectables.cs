using System.Collections;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay = 3f;

    public virtual void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
