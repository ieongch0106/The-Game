using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathSolve : MonoBehaviour
{
    // Start is called before the first frame update
    private int result;
    public TMP_Text showMath;

    void Start()
    {
        // CreateMath();
    }

    public void CreateMath()
    {
        int a = Random.Range(1, 100);
        int b = Random.Range(1, 100);

        result = a + b;
        showMath.text = a + " + " + b;
    }

    public void CheckAnswer(string ans)
    {
        if (int.Parse(ans) == result)
        {
            Debug.Log("correct");
        }
        else
        {

        }
    }
}
