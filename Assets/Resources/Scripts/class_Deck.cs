using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class class_Deck : MonoBehaviour
{
    public Dictionary<string, List<int>>
    m_cDictionary = new Dictionary<string, List<int>>()
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

    public Text txtHealth;
    int m_iHealth = 10;
    public GameObject DeathMessage;
    public int Health { get { return m_iHealth; } set { m_iHealth = value; } }
    const int NUM_SUITS = 5;
    const int NUM_VALUES = 2;
    const int NUM_CARDS = 10;
    private int SHUFFLE_SWAPS = 50;
    public int m_iNextCard = 0;
    public int CardsInHand = 0;
    public List<GameObject> m_Cards = new List<GameObject>();
    public List<GameObject> DefenseLane = new List<GameObject>();
    public List<GameObject> AttackLane = new List<GameObject>();
    public GameObject m_parentPanel;

    public Sprite temp;

    public string player = "1";

    bool cardsDrawn;
    public bool turnEnded;
    bool isFirstTurn;
    int m_iCardsDrawn;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        cardsDrawn = false;
        isFirstTurn = true;
        turnEnded = false;
        //need to loop through every card and add them to the deck.
        //2 of each.
        m_iHealth = 10;
        m_iNextCard = 0;
        //initiate all cards into the game world
        for (int iSuit = 0; iSuit < NUM_SUITS; ++iSuit)
        {
            for (int iValue = 0; iValue < NUM_VALUES; ++iValue)
            {
                m_Cards.Add(Instantiate(Resources.Load("2DCard")) as GameObject);
                m_Cards[m_Cards.Count - 1].transform.SetParent(GameObject.Find(player + "Deck").transform);
                m_Cards[m_Cards.Count - 1].GetComponent<Image>().sprite = Resources.Load(string.Format("CardImages/{0}_{1}", iSuit, iValue), typeof(Sprite)) as Sprite;
                if (m_cDictionary.ContainsKey(string.Format("{0}_{1}", iSuit, iValue)))
                {
                    m_Cards[m_Cards.Count - 1].GetComponent<class_Card>().Attack = m_cDictionary[string.Format("{0}_{1}", iSuit, iValue)][0];
                    m_Cards[m_Cards.Count - 1].GetComponent<class_Card>().Defense = m_cDictionary[string.Format("{0}_{1}", iSuit, iValue)][1];
                    m_Cards[m_Cards.Count - 1].name = string.Format("{0}_{1}", iSuit, iValue);
                }

                // m_Cards[m_Cards.Count - 1].transform.SetParent(GameObject.Find("Deck").transform);
            }
        }

        Shuffle();
        //draw 5 cards to begin with
        for(int i = 0; i < 5; ++i)
        {
            Draw();
        }
        //Debug.Log("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        //for each card draw them in screen space. make the parent the canvas

    }


    // Update is called once per frame
    void Update()
    {
        if (this != null)
        {
            txtHealth.text = m_iHealth.ToString();

            if (m_iHealth <= 0)
            {
                DeathMessage.SetActive(true);
            }
        }
    }

    //shuffle the deck
    public void Shuffle()
    {
        for (int i = 0; i < SHUFFLE_SWAPS; ++i)
        {
            int a = Random.Range(0, NUM_CARDS);
            int b = Random.Range(0, NUM_CARDS);
            GameObject temp = m_Cards[a];
            m_Cards[a] = m_Cards[b];
            m_Cards[b] = temp;
        }
    }

    //"Returns" all cards to the deck
    public void Refresh()
    {
        m_iNextCard = 0;
        Shuffle();
    }


    public void Draw()
    {
        if (!cardsDrawn && isFirstTurn)
        {
            if (!(m_iNextCard >= NUM_CARDS))
            {
                if (m_parentPanel.transform.childCount != 5 && m_iCardsDrawn != 5)
                {
                    m_Cards[m_iNextCard].transform.SetParent(GameObject.Find(player + "Hand").transform);
                    //Debug.Log(m_Cards[m_iNextCard].GetComponent<class_Card>().Defense);
                    ++m_iNextCard;
                    ++m_iCardsDrawn;
                }
            }
        }
        else if (!isFirstTurn)
        {
            if (!(m_iNextCard >= NUM_CARDS))
            {
                if (m_parentPanel.transform.childCount != 5 && m_iCardsDrawn != 2)
                {
                    m_Cards[m_iNextCard].transform.SetParent(GameObject.Find(player + "Hand").transform);
                    //Debug.Log(m_Cards[m_iNextCard].GetComponent<class_Card>().Defense);
                    ++m_iNextCard;
                    ++m_iCardsDrawn;
                }
            }
        }
    }

    public void FirstTurn()
    {
        isFirstTurn = false;
        turnEnded = true;
        m_iCardsDrawn = 0;
    }

}
