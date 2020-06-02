using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class Helper : MonoBehaviour
  {

        [Range(-1, 1)]
        public float vertical;
        [Range(-1, 1)]
        public float horizontal;

        public string animName;
        public bool playAnim;

        public bool enableRM;
        public bool lockon;

        Animator anim;

    void Start()
    {
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            enableRM = !anim.GetBool("canMove");
            anim.applyRootMotion = enableRM;

            if(lockon == false)
            {
                horizontal = 0;
                vertical = Mathf.Clamp01(vertical);
            }

            anim.SetBool("lockon", lockon);

            if (enableRM)
                return;

            if (playAnim)
            {
                vertical = 0;
                anim.CrossFade(animName, 0.2f);
               // anim.SetBool("canMove", false);
               // enableRM = true;
                playAnim = false;
            }
            anim.SetFloat("vertical", vertical);
            anim.SetFloat("horizontal", horizontal);
    }
  }
}
