using System;
using System.Collections.Generic;

namespace TweakBot
{
    class FiniteStateMachine
    {
        public struct GameState
        {
            public bool DoPlaceArmies;
            public int[] Regions;
                    
            public GameState(bool placeArmies, int[] regions) 
            {   
                DoPlaceArmies = placeArmies;
                Regions = regions;
            }
        
            public override string ToString()
            {
                return String.Concat(DoPlaceArmies ? "P":"M", Regions);
            }
        } // struct

        public Dictionary<GameState, double> states;

        private void AddChanceToState(GameState state, double addChance)
        {
            if (states.ContainsKey(state))
            {
                states[state] = states[state] + addChance;
            }
            else
            {
                states.Add(state, addChance);
            }
        }

        public FiniteStateMachine()
        { 
            states = new Dictionary<GameState, double>();
            // -2 = neutral, 2 = ME, Chance=100 = 100% 
            states.Add(new GameState(true, new int[] { -2, 2, -2 }),100);

            // dit gaat niet goed: dictionary wordt uitgebreid tijdens analyse en daar houdt dictionary.foreach niet van
            foreach (KeyValuePair<GameState, double> state in states)
            {
                if (state.Key.DoPlaceArmies)
                {
                    // 1 tactic : place on mid spot
                    AddChanceToState(new GameState(false, new int[] { state.Key.Regions[0], state.Key.Regions[1] + 5, state.Key.Regions[2] }), state.Value);
                }
                else
                {
                    // end state if none is neutral
                    if (state.Key.Regions[0] < 0 || state.Key.Regions[2] < 0)
                    {
                        // no move
                        if (state.Key.Regions[1] < 4) 
                        {
                            AddChanceToState(new GameState(true, new int[] { state.Key.Regions[0], state.Key.Regions[1], state.Key.Regions[2] }), state.Value);
                        } 
                        // attack 1
                        if (state.Key.Regions[1] < 7)
                        {

                        }
                    }
                }
            }
        }
    }
    
}
