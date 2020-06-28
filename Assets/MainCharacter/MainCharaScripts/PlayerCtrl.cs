using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
namespace ActionGame
{
    public class PlayerCtrl : MonoBehaviour
    {
        const float RayCastMaxDistance = 100.0f;
        CharacterStatus status;
        Transform attackTarget;
        public float attackRange = 1.5f;


        enum State
        {
            Moving,
            Attacking,
            Died,
        };

        State state = State.Moving;
        State nextState = State.Moving;

        void Start()
        {
            status = GetComponent<CharacterStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case State.Moving:
                    Moving();
                    break;
                case State.Attacking:
                    Attacking();
                    break;

            }

            if (state != nextState)
            {
                state nextState;
                switch (state)
                {
                    case State.Moving:
                        MoveStart();
                        break;
                    case State.Attacking:
                        AttackStart();
                        break;
                    case State.Died:
                        Died();
                        break;
                }
            }
        }

        //ステートの変更
        void ChangeState(State nextState)
        {
            this.nextState = nextState;
        }

        void MoveStart()
        {
            StateStartCommon();
        }

        void Moving
        {

        }
    }
}
*/