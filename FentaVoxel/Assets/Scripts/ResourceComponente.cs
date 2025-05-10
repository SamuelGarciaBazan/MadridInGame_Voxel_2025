using UnityEngine;


/*
este componente lo llevan todos los obejtos que son recursos, tienen su vida, y cada cuanto deben dropear un item de los que dan recursos
*/
public class ResourceComponente : MonoBehaviour
{

    [SerializeField]
    float _maxLife;

    [SerializeField]
    [Tooltip("Cada cuanta cantidad extraida de este recurso se suelta un item del recurso")]
    float _packageDropRate;

    [SerializeField]
    ResourcesManager.ResourcesType resourceType;

    float _currentLife;

    private void Awake()
    {
        _currentLife = _maxLife;
    }
    public float getPackageDropRate()
    {
        return _packageDropRate;
    }
    public ResourcesManager.ResourcesType GetResourcesType() { return resourceType; }

    public void subtractResources(float amount)
    {
        _currentLife -= amount;

        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
