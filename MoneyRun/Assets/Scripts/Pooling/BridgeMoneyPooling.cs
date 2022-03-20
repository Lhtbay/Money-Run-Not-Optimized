using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMoneyPooling : MonoBehaviour
{
    [SerializeField] private GameObject _bridgeMoney;
    [SerializeField] private int _poolingCount;
    [SerializeField] private List<GameObject> _moneyList;

    private void Start()
    {
        for (int i = 0; i < _poolingCount; i++)
        {
            GameObject obj = Instantiate(_bridgeMoney, transform.position,_bridgeMoney.transform.rotation);
            obj.SetActive(false);
            _moneyList.Add(obj);
            obj.transform.parent = this.transform;
        }
    }

    public void TakeMoney(Transform spawnTrans)
    {
        foreach (var item in _moneyList)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = spawnTrans.position;
                item.SetActive(true);
                break;
            }
        }
    }

}
