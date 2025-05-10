using UnityEngine;

public class RechargeMaterial : MonoBehaviour
{
    [SerializeField] private SpendingResourceManager.ResourcesType _typeRecharged;
    [SerializeField] private ResourcesManager.ResourcesType _typeSpended;

    private GameManager _gm;
    private ResourcesManager _rm;
    private SpendingResourceManager _srm;
    private SpendMaterialHUDController _smhc;

    public void Recharge(int _amount)
    {
        if (_smhc.DepositAmount(_amount))
        {
            _srm.addResources(_typeRecharged, _amount);
        }
    }

    public void RechargeMax()
    {

        float cantidad = _smhc.DepositMaxAmount();

        cantidad = _srm.addMaxResources(_typeRecharged, cantidad);

        _rm.addResources(_typeSpended, cantidad);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = GameManager.getInstance();
        _srm = _gm.GetComponent<SpendingResourceManager>();
        _smhc = GetComponent<SpendMaterialHUDController>();
    }

}
