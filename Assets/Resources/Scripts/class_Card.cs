using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class class_Card : MonoBehaviour { 

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

    public enum AttackOrDefense
    {
        Attack,
        Defense
    }

    public int Slot;

    public int m_iAttack;
    public int m_iDefense;

    public string m_sName;
    string m_DictIndex;
    public GameObject m_gCard;
    public Text txtAttack;
    public Text txtDefense;
    public AttackOrDefense m_state = AttackOrDefense.Attack;
    public Vector3 oldPos;
    public bool m_bIsDraggable = true;
    public class_Card()
    {

    }
    public class_Card(class_Card a_card)
    {
        Name = a_card.Name;
        m_iAttack = a_card.m_iAttack;
        m_iDefense = a_card.m_iDefense;
        m_gCard = a_card.m_gCard;
        m_gCard.name = a_card.m_gCard.name;

    }
    public string Name { get { return m_sName; } set { m_sName = value; } }
    public int Attack { get { return m_iAttack; } set { m_iAttack = value; } }
    public int Defense { get { return m_iDefense; } set { m_iDefense = value; } }



    public class_Card(int suit, int value)
    {
        m_sName = string.Format("CardImages/{0}_{1}", suit, value);
        //Debug.Log(m_sName);
        m_DictIndex= string.Format("{0}_{1}" , suit , value);
        if (m_cDictionary.ContainsKey(m_DictIndex))
        {
            m_iAttack = m_cDictionary[m_DictIndex][0];
            m_iDefense = m_cDictionary[m_DictIndex][1];
        }
        m_gCard = Resources.Load("2DCard") as GameObject;
        //m_gCard.GetComponent<SpriteRenderer>().sprite = Resources.Load("m_sName" , typeof(Sprite)) as Sprite;
        //m_gCard.GetComponentInChildren<Renderer>().material = Resources.Load(m_sName , typeof(Material)) as Material;
        m_gCard.name = this.m_sName;
        //Debug.Log(m_sName);
    } 

    void Update()
    {
        txtAttack.text = Attack.ToString();
        txtDefense.text = Defense.ToString();

        if (Defense <= 0)
        {
            this.transform.SetParent(GameObject.Find("DiscardPile").transform);
            this.transform.position = new Vector2(70, 70);


        }
        Debug.DrawLine(this.oldPos, this.transform.position);

    }

    public void TakeDamage(int a_damage)
    {
        Defense -= a_damage;
    }
}
