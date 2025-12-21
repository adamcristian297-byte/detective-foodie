using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            // Try to find ANY script with Interact()
            MonoBehaviour[] scripts = hit.collider.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                var method = script.GetType().GetMethod("Interact");
                if (method != null)
                {
                    method.Invoke(script, null);
                    return;
                }
            }
        }
    }
}
