using UnityEngine;

public class SpendMaterialHUDController : MonoBehaviour
{

    [SerializeField] private ResourcesManager.ResourcesType[] _typesSpended;
    [SerializeField] private int[] _amountCost;
    
    private GameManager _gm;
    private ResourcesManager _rm;

    public bool DepositAmount()
    {
        bool canDeposit = true;

        int i = 0;

        if(_typesSpended.Length == _amountCost.Length)
        {

            while (i < _typesSpended.Length && canDeposit)
            {
                if (!_rm.hasEnoughResource(_typesSpended[i], _amountCost[i]))
                {
                    canDeposit = false;
                }
                ++i;
            }

        }
        else
        {
            canDeposit = false;
        }

        i = 0;

        if (canDeposit)
        {
            while (i < _typesSpended.Length && canDeposit)
            {
                _rm.spendResource(_typesSpended[i], _amountCost[i]);
                ++i;
            }
        }

        return canDeposit;
        
    }

    public bool DepositAmount(int _amount)
    {
        bool canDeposit = true;

        int i = 0;

        while (i < _typesSpended.Length && canDeposit)
        {
            if (!_rm.hasEnoughResource(_typesSpended[i], _amount))
            {
                canDeposit = false;
            }
            ++i;
        }

        i = 0;

        if (canDeposit)
        {
            while (i < _typesSpended.Length && canDeposit)
            {
                _rm.spendResource(_typesSpended[i], _amount);
                ++i;
            }
        }

        return canDeposit;

    }

    //Solo valido para cuando hay gasto de 1 elemento
    public float DepositMaxAmount()
    {
        float originalAmount = 0;

        if(_typesSpended.Length <= 1)
        {
            originalAmount = _rm.getResourceAmount(_typesSpended[0]);

            _rm.spendResource(_typesSpended[0], _rm.getResourceAmount(_typesSpended[0]));

        }

        return originalAmount;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = GameManager.getInstance();
        _rm = _gm.GetComponent<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
