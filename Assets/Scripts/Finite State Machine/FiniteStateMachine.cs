using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace AIHW2
{
    public class FiniteStateMachine
    {
        #region Fields

        // The current state of the FSM
        protected IState currentState;

        // The Dictionary of all of the states and their transitions
        private Dictionary<Type, List<Transition>> possibleTransitions = new Dictionary<Type, List<Transition>>();

        // The list of transitions that can go from any state
        private List<Transition> anyTransitions = new List<Transition>();
        
        // The list of current transitions from the current state
        private List<Transition> currentTransitions = new List<Transition>();

        // Used for the 
        private static List<Transition> emptyTransitions = new List<Transition>(0);

        #endregion

        // Default Constructor
        public FiniteStateMachine() { }

        #region Methods

        /// <summary>
        /// Sets the current state of the FSM
        /// </summary>
        /// <param name="state">Takes in the class of the state</param>
        public void SetCurrentState(IState state)
        {
            if (state == currentState)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = state;
            Debug.Log(currentState);

            possibleTransitions.TryGetValue(currentState.GetType(), out currentTransitions);
            if (currentTransitions == null)
            {
                currentTransitions = emptyTransitions;
            }

            currentState.OnEnter();


        }

        /// <summary>
        /// Gets the current state
        /// </summary>
        /// <returns></returns>
        public IState GetCurrentState()
        {
            return currentState;

        }

        /// <summary>
        /// The 'Update' method for the FSM
        /// Anything here will be ran each Update tick
        /// </summary>
        public void Tick()
        {
            Transition transition = GetTransition();
            if (transition != null)
            {
                SetCurrentState(transition.To);
            }

            if (currentState != null)
            {
                currentState.Tick();
            }

        }

        /// <summary>
        /// Gets the transition between the current state and the next state
        /// </summary>
        /// <returns></returns>
        public Transition GetTransition()
        {
            foreach (Transition transition in anyTransitions)
            {
                if (transition.Condition())
                {
                    return transition;

                }
            }

            foreach (Transition transition in currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;

                }
            }

            return null;

        }

        /// <summary>
        /// Adds a transition that can go from any state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="predicate"></param>
        public virtual void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));

        }

        /// <summary>
        /// Adds the transition to the dictionary of possible transitions for each state
        /// </summary>
        /// <param name="from">The state that the FSM is currently in</param>
        /// <param name="to">The state that the FSM wants to go to</param>
        /// <param name="predicate">How to get from the 'from' state to the 'to' state</param>
        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (possibleTransitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                possibleTransitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, from, predicate));

        }

        

        #endregion


    }
}
