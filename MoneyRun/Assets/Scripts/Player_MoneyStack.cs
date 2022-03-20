using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_MoneyStack : MonoBehaviour
{
    // WOMAN CHARACTERS ADDED

    #region Fields

    [Header("Woman Money Settings")]
    public List<GameObject> _listWomanMoney;
    [SerializeField] private int _upForce,_fallForce;
    private int _totalStackCount,_totalTakeMoney = 0;
    private int _currentAddCount, _currentTakeOutCount = 0;
    private float _time, _falseTime,_time2,_falseTime2;
    private GameObject _womanMoney;
    private GameObject _bridgeSpawn;
    private GameObject _player;
    private bool _isGround,_oneTime,_bridge,_haveMoney,_end = false;
    private BridgeMoneyPooling _bridgeMoneyPooling;

    [Header("Money Settings")]
    [SerializeField] private GameObject _moneyTextObject;
    [SerializeField] private TextMeshProUGUI _moneyTextMesh;

    [Header("Other Settings")]
    [SerializeField] private GameManagers _gameManagers;

    #endregion

    private void Start()
    {
        _bridgeMoneyPooling = GameObject.FindGameObjectWithTag("PoolMoney").gameObject.GetComponent<BridgeMoneyPooling>();
        _bridgeSpawn = GameObject.FindGameObjectWithTag("BridgeSpawn").gameObject;
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;

        _womanMoney = GameObject.FindGameObjectWithTag("WomanMoney").gameObject;
        for (int i = 0; i < _womanMoney.transform.childCount; i++)
        {
            _listWomanMoney.Add(_womanMoney.transform.GetChild(i).gameObject);
        }

        _totalStackCount = 0;
        _falseTime = 0.3f;
        _falseTime2 = 0.1f;
    }
    private void Update()
    {
        ControlHaveMoney();

        if ((Input.GetKey(KeyCode.Space) || _end ) && _isGround && _haveMoney)
        {
            _time = 0;
            _oneTime = true;
            _bridge = true;
        }
        else if (_oneTime)
        {
            _time += Time.deltaTime;
            if (_time >= _falseTime)
            {
                _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _player.GetComponent<Rigidbody>().AddForce((_player.transform.up * -1) * _fallForce);
                _isGround = false;
                _oneTime = false;
                _bridge = false;
            }
        }
        if (_bridge && _haveMoney)
        {
            _time2 += Time.deltaTime;
            if (_time2 >= _falseTime2)
            {
                _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                MoneyRoad(3);
                _time2 = 0;
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Money")
        {
            other.gameObject.SetActive(false);
            _totalTakeMoney += 100;
            _moneyTextObject.SetActive(true);
            _moneyTextMesh.text = "+"+_totalTakeMoney.ToString() + "$";
            MoneyAdded(3);
        }
        if (other.gameObject.tag == "Ground" && !_isGround)
        {
            _isGround = true;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            _gameManagers.RestartGame();
        }
        if (other.gameObject.tag == "End")
        {
            _end = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag ==  "Ground")
        {
            _isGround = true;
        }
    }

    #region Methods

    public void TextClosed()
    {
        _moneyTextObject.SetActive(false);
        _totalTakeMoney=0;
    }

    private void MoneyAdded(int addcount)
    {
        foreach (var item in _listWomanMoney)
        {
            if (_currentAddCount >= addcount)
            {
                _currentAddCount = 0;
                break;
            }
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                _totalStackCount++;
                _currentAddCount++;
            }
        }
    }

    private void MoneyRoad(int takeoutcount)
    {
        _totalStackCount--;
        _bridgeMoneyPooling.TakeMoney(_bridgeSpawn.transform);
        _player.GetComponent<Rigidbody>().AddForce(_player.transform.up * _upForce);

        for (int i = _listWomanMoney.Count-1; i <= _listWomanMoney.Count; i--)
        {
            if (_currentTakeOutCount >= takeoutcount)
            {
                _currentTakeOutCount = 0;
                break;             
            }
            if (_listWomanMoney[i].activeInHierarchy)
            {
                _currentTakeOutCount++;             
                _listWomanMoney[i].SetActive(false);
            }
            
        }

    }
    private void ControlHaveMoney()
    {
        if (_listWomanMoney[2].activeInHierarchy)
        {
            _haveMoney = true;
        }
        else
        {
            _haveMoney = false;
        }
    }

    #endregion
}
