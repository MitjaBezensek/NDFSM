using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSM{
   public class NonDeterministicFSM{
      private readonly List<string> Q = new List<string>();
      private readonly List<char> Sigma = new List<char>();
      private readonly List<Transition> Delta = new List<Transition>();
      private string Q0;
      private readonly List<string> F = new List<string>();

      public NonDeterministicFSM(IEnumerable<string> q, IEnumerable<char> sigma,
         IEnumerable<Transition> delta, string q0, IEnumerable<string> f){
         Q = q.ToList();
         Sigma = sigma.ToList();
         AddTransitions(delta);
         AddInitialState(q0);
         AddFinalStates(f);
      }

      private void AddTransitions(IEnumerable<Transition> transitions){
         foreach (var transition in transitions.Where(ValidTransition)){
            Delta.Add(transition);
         }
      }

      private bool ValidTransition(Transition transition){
         return Q.Contains(transition.StartState) &&
                Q.Contains(transition.EndState) &&
                Sigma.Contains(transition.Symbol);
      }

      private void AddInitialState(string q0){
         if (q0 != null && Q.Contains(q0)){
            Q0 = q0;
         }
      }

      private void AddFinalStates(IEnumerable<string> finalStates){
         foreach (var finalState in finalStates.Where(finalState => Q.Contains(finalState))){
            F.Add(finalState);
         }
      }

      public void Accepts(string input){
         ConsoleWriter.Success("Trying to accept: " + input);

         if (Accepts(Q0, input, new StringBuilder())){
            return;
         }
         ConsoleWriter.Failure("Could not accept the input: " + input);
      }

      private bool Accepts(string currentState, string input, StringBuilder steps){
         if (input.Length > 0){
            var transitions = GetAllTransitions(currentState, input[0]);
            foreach (var transition in transitions){
               var currentSteps = new StringBuilder(steps.ToString() + transition);
               if (Accepts(transition.EndState, input.Substring(1), currentSteps)){
                  return true;
               }
            }
            return false;
         }
         if (F.Contains(currentState)){
            ConsoleWriter.Success("Successfully accepted the input " + input + " " +
                                  "in the final state " + currentState +
                                  " with steps:\n" + steps);
            return true;
         }
         return false;
      }

      private IEnumerable<Transition> GetAllTransitions(string currentState, char symbol){
         return Delta.FindAll(t => t.StartState == currentState &&
                                   t.Symbol == symbol);
      }
   }
}