using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;


public class AttackTarget : MonoBehaviour
{
    CoinToss m_coinToss;
    public class_Deck Player1;
    public class_Deck Player2;

    public GameObject button;

    private int attackOrder = 0;
    private int coinFlip;

    public float m_fTimer;
    private float m_fTime;

    public float m_fTimerFade;
    private float m_fTimeFade;

    public GameObject m_drawMessage;
    public GameObject m_player1Win;
    public GameObject m_player2Win;

    public GameObject HandBlock1;
    public GameObject HandBlock2;

    public MenuController aMenuController;
    void Start()
    {
        m_fTime = 4;
        coinFlip = UnityEngine.Random.Range(0, 1);
    }
    public void AttacK()
    {
        if (coinFlip == 1)
        {
            //player 2 attacking player 1
            //Player 2's target
            class_Card player2Target = null;

            //check if any cards in player1's defense lane or attack lane. make the target defene > attack otherwise null.

            if (Player1.DefenseLane.Count >= 1)
            {
                //pick a defense target from player 1's defense lane

                player2Target = Player1.DefenseLane[UnityEngine.Random.Range(0, Player1.DefenseLane.Count - 1)].GetComponent<class_Card>();
            }
            else if (Player1.AttackLane.Count >= 1)
            {
                //pick a random target from player1's attck lane
                int targetRandom = UnityEngine.Random.Range(0, Player1.AttackLane.Count + 1);
                if (targetRandom <= Player1.AttackLane.Count - 1)
                {

                    player2Target = Player1.AttackLane[UnityEngine.Random.Range(0, Player1.AttackLane.Count - 1)].GetComponent<class_Card>();
                }
                else
                {

                    //if the rng is higher than attackCount attack the player
                    player2Target = null;
                }
            }
            else
            {

                player2Target = null;
            }

            if (attackOrder < Player2.AttackLane.Count)
            {
                if (player2Target != null)
                {

                    player2Target.TakeDamage(Player2.AttackLane[attackOrder].GetComponent<class_Card>().Attack);
                    Player2.AttackLane[attackOrder].GetComponent<class_Card>().Defense -= player2Target.Attack;
                    attackOrder++;
                }

                if (player2Target == null)
                {

                    Player1.Health -= Player2.AttackLane[attackOrder].GetComponent<class_Card>().Attack;

                    attackOrder++;
                }
            }
        }

        else if (coinFlip == 0)
        {
            //attacking Player 2
            //player1's target
            class_Card player1Target = null;
            if (Player2.DefenseLane.Count >= 1)
            {

                player1Target = Player2.DefenseLane[UnityEngine.Random.Range(0, Player1.DefenseLane.Count -1 )].GetComponent<class_Card>();
            }
            //else attack the player's attack lane
            else if (Player2.AttackLane.Count >= 1)
            {
                int targetRandom = UnityEngine.Random.Range(0, Player2.AttackLane.Count + 1);
                if (targetRandom <= Player2.AttackLane.Count - 1)
                {
                    player1Target = Player2.AttackLane[UnityEngine.Random.Range(0, Player2.AttackLane.Count -1 )].GetComponent<class_Card>();
                }
                else
                {

                    player1Target = null;
                }
            }
            else
            {

                player1Target = null;
            }

            //if the attack order isnt greater than our current attack lane, you may attack
            if (attackOrder < Player1.AttackLane.Count)
            {
                if (player1Target != null)
                {

                    player1Target.TakeDamage(Player1.AttackLane[attackOrder].GetComponent<class_Card>().Attack);
                    Player1.AttackLane[attackOrder].GetComponent<class_Card>().Defense -= player1Target.Attack;
                    attackOrder++;

                }
                else
                {

                    Player2.Health -= Player1.AttackLane[attackOrder].GetComponent<class_Card>().Attack;
                    attackOrder++;
                }
            }

        }

    }

    //Depending on coin toss (done in engine) the players attack each other, for each of their attack lanes. this will appear instantanious
    public void autoAttack()
    {
        for (int i = 0; i < Player1.AttackLane.Count; i++)
        {

            AttacK();
            CheckDefense();
        }
        changeFlip();
        for (int i = 0; i < Player2.AttackLane.Count; i++)
        {

            AttacK();
            CheckDefense();
        }
        for (int i = 0; i < 2; ++i)
        {
            Player1.Draw();
            Player2.Draw();
            Player1.turnEnded = false;
            Player2.turnEnded = false;
            HandBlock1.SetActive(false);
            HandBlock2.SetActive(false);

            aMenuController.P1 = false;
            aMenuController.P2 = false;

            aMenuController.Fader1.SetActive(false);
            aMenuController.Fader2.SetActive(true);
            attackOrder = 0;
        }
    }

    //updates every frame
    void Update()
    {




        //once both players have ended their turn, a big ATTACK! button shows up indicating attacking is happening. during this time both players hands are blocked.
        if (Player1.turnEnded && Player2.turnEnded)
        {
            button.SetActive(true);
            HandBlock1.SetActive(true);
            HandBlock2.SetActive(true);
            m_fTimer += Time.deltaTime;
            if (m_fTimer >= m_fTime)
            {
                autoAttack();
                m_fTimer = 0;
                HandBlock1.SetActive(false);
                HandBlock2.SetActive(false);

            }
        }
        else
        {
            button.SetActive(false);

        }

        //If both players have 0 cards left AND both their attack lanes are empty AND their health are equal, the game will end in a draw.
        if (Player1.m_iNextCard >= 10 && Player2.m_iNextCard >= 10 && Player1.Health == Player2.Health && Player1.AttackLane.Count == 0 && Player2.AttackLane.Count == 0)

        {
            m_drawMessage.SetActive(true);
        }
        //similiar conditions above however if player1's health is greater than player2, a message will indicate player 1 has won
        if (Player1.m_iNextCard >= 10 && Player2.m_iNextCard >= 10 && Player1.Health > Player2.Health && Player1.AttackLane.Count == 0 && Player2.AttackLane.Count == 0)
        {
            m_player1Win.SetActive(true);
        }

        //same as above except for player 2 > player 1
        if (Player1.m_iNextCard >= 10 && Player2.m_iNextCard >= 10 && Player1.Health < Player2.Health && Player1.AttackLane.Count == 0 && Player2.AttackLane.Count == 0)
        {
            m_player2Win.SetActive(true);
        }
        CheckDefense();
    }

    void CheckDefense()
    {
        //pasted 4 times. checks each player's card's defense and attack lane. 
        //if any of their defense is 0 or below it removes it from the list.
        if (Player1.DefenseLane.Count >= 1)
        {
            for (int i = Player1.DefenseLane.Count - 1; i >= 0; --i)
            {
                if (Player1.DefenseLane[i].GetComponent<class_Card>().Defense <= 0)
                {
                    Player1.DefenseLane.RemoveAt(i);
                }
            }
        }
        if (Player2.DefenseLane.Count >= 1)
        {
            for (int i = Player2.DefenseLane.Count - 1; i >= 0; --i)
            {
                if (Player2.DefenseLane[i].GetComponent<class_Card>().Defense <= 0)
                {
                    Player2.DefenseLane.RemoveAt(i);
                }
            }
        }
        if (Player1.AttackLane.Count >= 1)
        {
            for (int i = Player1.AttackLane.Count - 1; i >= 0; --i)
            {
                if (Player1.AttackLane[i].GetComponent<class_Card>().Defense <= 0)
                {
                    Player1.AttackLane.RemoveAt(i);
                }
            }

        }
        if (Player2.AttackLane.Count >= 1)
        {
            for (int i = Player2.AttackLane.Count - 1; i >= 0; --i)
            {
                if (Player2.AttackLane[i].GetComponent<class_Card>().Defense <= 0)
                {
                    Player2.AttackLane.RemoveAt(i);
                }
            }
        }

    }


    void changeFlip()
    {
        //flips the coin around used for attack order.
        if (coinFlip == 0)
        {
            if (attackOrder == Player1.AttackLane.Count)
            {
                attackOrder = 0;
                coinFlip = (coinFlip == 1) ? 0 : 1;

            }
        }
        else if (coinFlip == 1)
        {
            if (attackOrder == Player2.AttackLane.Count)
            {
                attackOrder = 0;
                coinFlip = (coinFlip == 1) ? 0 : 1;

            }
        }
    }
}

