using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanManager : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> m_Cans;

    [SerializeField]
    protected List<GameObject> m_Balls;

    [SerializeField]
    protected GameObject m_CanPyramidPrefab;

    [SerializeField]
    protected GameObject m_BallRespawnPrefab;

    public void Reset()
    {
        for (int i = m_Cans.Count; i > 0; i--)
        {
            Destroy(m_Cans[i-1]);
        }

        for (int i = m_Balls.Count; i > 0; i--)
        {
            Destroy(m_Balls[i-1]);
        }

        GameObject cans = Instantiate(m_CanPyramidPrefab, gameObject.transform);
        GameObject balls = Instantiate(m_BallRespawnPrefab, gameObject.transform);

        foreach (Transform child in cans.transform)
        {
            m_Cans.Add(child.gameObject);
        }

        foreach (Transform child in balls.transform)
        {
            m_Balls.Add(child.gameObject);
        }
    }
}
