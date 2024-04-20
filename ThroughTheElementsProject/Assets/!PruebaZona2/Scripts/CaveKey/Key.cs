using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public UnityEvent OnCollectedKey;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactiva la llave al ser recogida
            //gameObject.SetActive(false);

            // Notifica a la puerta que se ha recolectado una llave
            transform.parent.GetComponent<CaveDoor>().CollectKey();
            OnCollectedKey.Invoke();
}
    }
}
