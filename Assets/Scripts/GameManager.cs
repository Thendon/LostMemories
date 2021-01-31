using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager
{
    const int memoryWinCondition = 1;
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
    protected List<Memory> m_Memories = new List<Memory>();
    public UnityEvent memoryAdded = new UnityEvent();

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

    public bool IsGameover()
    {
        return m_Memories.Count >= memoryWinCondition;
    }

    public bool AddMemory(Memory memory)
    {
        if (m_Memories.Contains(memory))
            return false;

        m_Memories.Add(memory);
        memoryAdded.Invoke();
        return true;
    }

    public List<Memory> GetMemories()
    {
        return new List<Memory>(m_Memories);
    }
}
