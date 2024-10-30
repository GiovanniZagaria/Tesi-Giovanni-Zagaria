using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public Rigidbody rb;
    public Transform[] dicefaces;

    private int diceIndex = -1;

    private bool hasStoppedRolling;
    private bool delayFinished;
    public static UnityAction<int, int> OnDiceResult;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!delayFinished) return;

        if(!hasStoppedRolling && rb.velocity.sqrMagnitude == 0f)
        {
            hasStoppedRolling = true;
            GetNumberOnTopFace();
        }
    }

    [ContextMenu("Get Top Face")]
    private int GetNumberOnTopFace()
    {
        if(dicefaces == null) return -1;
        
        var topFace = 0;
        var lastYPosition = dicefaces[0].position.y;

        for(int i=0; i<dicefaces.Length; i++)
        {
            if(dicefaces[i].position.y>lastYPosition)
            {
                lastYPosition = dicefaces[i].position.y;
                topFace = i;
            }
        }

        Debug.Log($"Dice result {topFace +1}");

        OnDiceResult?.Invoke(diceIndex, topFace + 1);

        return topFace + 1;
    }

    public void RollDice(float throwForce, float rollForce, int i){
        diceIndex = i;
        var randomVariance = Random.Range(-1f,1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);

        var randX = Random.Range(0f, 3f);
        var randY = Random.Range(0f, 3f);
        var randZ = Random.Range(0f, 3f);

        rb.AddTorque(new Vector3(randX, randY, randZ) * (rollForce + randomVariance), ForceMode.Impulse);

        DelayResult();
    }

    private async  void DelayResult()
    {
        await Task.Delay(1000);
        delayFinished = true;
    }
}
