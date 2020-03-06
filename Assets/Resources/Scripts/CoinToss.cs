using UnityEngine;
using System.Collections;
using System.IO;


public class CoinToss : MonoBehaviour
{

    public int Coinflip()
    {
        int toss = Random.Range(1, 2);

        if (toss == 1)
        {
            //heads
            return 0;
        }
        else
        {
            //tails
            return 1;
        }
    }
}
