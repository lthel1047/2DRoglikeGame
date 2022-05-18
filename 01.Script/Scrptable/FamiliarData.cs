using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName = "Familiar.asset", menuName = "Familiar/FamailiarObject")]

public class FamiliarData : ScriptableObject {
    public string famailiarType;
    public float speed;
    public float fireDelay;
    public GameObject bulletPrefab;
        
}