using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    public Dice diceToThrow;
    public int amountofDice =1;
    public float throwForce = 5f;
    public float rollForce = 10f;

    private List<GameObject> spanedDice = new List<GameObject>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) RollDice();
    }

    private async void RollDice()
    {
        if(diceToThrow == null) return;

        foreach (var die in spanedDice)
        {
            Destroy(die);
        }

        for(int i = 0; i < amountofDice; i++)
        {
             float randomX = Random.Range(0f, 36f);
        float randomY = Random.Range(-110f, -60f);
        //float randomZ = Random.Range(0f, 360f);

        // Crea una nuova Quaternion con gli angoli casuali
        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, 0f);
            Dice dice = Instantiate(diceToThrow, transform.position, transform.rotation);
            spanedDice.Add(dice.gameObject);
            dice.RollDice(throwForce, rollForce, i);
            await Task.Yield();
        }
    }
}
