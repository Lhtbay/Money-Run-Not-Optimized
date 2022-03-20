using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public bool FalseThis = false;
    public Player_MoneyStack _playerMoneyStack;
    private void Update()
    {
        if (FalseThis)
        {
            _playerMoneyStack.TextClosed();
            FalseThis = false;
        }
    }

}
