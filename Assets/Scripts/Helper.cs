using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Helper : MonoBehaviour
{
    private static Random random = new Random();  

    public static void Shuffle<T>(IList<T> list)  
    {  
        var listCount = list.Count;  
        while (listCount > 1) {  
            listCount--;  
            int randomIndex = random.Next(listCount + 1);  
            T value = list[randomIndex];  
            list[randomIndex] = list[listCount];  
            list[listCount] = value;  
        }  
    }
    public static int DistanceBetweenTwoDots((int, int) first, (int, int) second)
    {
        return Mathf.Abs(first.Item1 - second.Item1) + Mathf.Abs(first.Item2 - second.Item2);
    }
}

