using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionGame
{
    public class HitArea : MonoBehaviour
    {
        // Start is called before the first frame update
        void Damage(AttackArea.AttackInfo attackInfo)
        {
            transform.root.SendMessage("Damage", attackInfo);
        }

    }
}
