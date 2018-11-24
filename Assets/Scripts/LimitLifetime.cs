using UnityEngine;

public class LimitLifetime : MonoBehaviour {

    /// <summary>
    /// The lifetime of the gameobject in seconds.
    /// </summary>
    public float lifetime;

    // Use this for initialization
    private void Start() {
        Invoke("Die", lifetime);
    }

    /// <summary>
    /// Destroys this gameobjet.
    /// </summary>
    private void Die() {
        Destroy(gameObject);
    }
}
