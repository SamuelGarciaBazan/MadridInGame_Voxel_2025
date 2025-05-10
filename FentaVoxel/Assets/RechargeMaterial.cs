using UnityEngine;

public class RechargeMaterial : MonoBehaviour
{
    [SerializeField] private SpendingResourceManager.ResourcesType _typeRecharged;

    private GameManager _gm;
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
        
        //_srm.addResources(_typeRecharged, 0);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = GameManager.getInstance();
        _srm = _gm.GetComponent<SpendingResourceManager>();
        _smhc = GetComponent<SpendMaterialHUDController>();
    }

}
