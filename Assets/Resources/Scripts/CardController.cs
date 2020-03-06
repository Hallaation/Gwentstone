using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardController : MonoBehaviour
{
    public Dictionary<string , List<int>>
    m_cDictionary = new Dictionary<string , List<int>>()
   {
               {"0_0", new List<int> {7, 5 } },
               {"0_1", new List<int> {5, 7 } },
               {"1_0", new List<int> {4, 6 } },
               {"1_1", new List<int> {5, 5 } },
               {"2_0", new List<int> {6, 4 } },
               {"2_1", new List<int> {6, 4 } },
               {"3_0", new List<int> {4, 6 } },
               {"3_1", new List<int> {5, 5 } },
               {"4_0", new List<int> {7, 5 } },
               {"4_1", new List<int> {5, 7 } }
   };

    public class_Card cardClass;
    int m_iAttack;
    int m_iDefense;

    public Text txtAttack;
    public Text txtDefense;
    public bool isAttack = true;

    public int Attack { get { return m_iAttack; } set { m_iAttack = value; } }
    public int Defense { get { return m_iDefense; } set { m_iDefense = value; } }


    void Start()
    {
        Attack = 10;
    }

    public void SetCard(class_Card a_card)
    {
        Debug.Log(a_card.Attack);
        m_iAttack = a_card.Attack;
    }

    void Update()
    {
        Debug.Log(m_iAttack);


    }
}
