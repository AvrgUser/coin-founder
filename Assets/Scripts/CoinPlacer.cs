using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlacer : MonoBehaviour
{
    [SerializeField] List<Coin> prefabsCoins = new List<Coin>();
    [SerializeField] GameObject player;
    [SerializeField] float maxDist = 50, minDist=20;
    List<Coin> coins = new List<Coin>();

    private void Update()
    {
        float closest = coins.Count>0?Vector2.Distance(player.transform.position, coins[0].transform.position):maxDist+1;
        foreach (Coin go in coins)
        {
            float dist = Vector2.Distance(player.transform.position, go.transform.position);
            if (dist < closest)
            {
                closest = dist;
            }
        }
        if (closest > maxDist)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        int type = Random.Range(0, prefabsCoins.Count);
        Coin newCoin = Instantiate(prefabsCoins[type]);
        newCoin.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        do {
            newCoin.transform.position = player.transform.position + (Vector3)Random.insideUnitCircle * maxDist;
        }
        while(Vector2.Distance(newCoin.transform.position,player.transform.position) < minDist);
        coins.Add(newCoin);
        newCoin.OnCollected=()=>coins.Remove(newCoin);
    }
}
