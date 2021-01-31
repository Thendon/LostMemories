using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    protected int m_Coins;

    public int AddCoins(int Coins)
    {
        m_Coins += Coins;

        Debug.Log("Coins: "+m_Coins);

        return m_Coins;
    }

    public bool RemoveCoins(int Coins)
    {
        if(m_Coins > Coins)
        {
            m_Coins -= Coins;
            return true;
        }

        return false;
    }
}
