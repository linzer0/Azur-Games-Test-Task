using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ListExtension : MonoBehaviour
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
}

