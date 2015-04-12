using System.Collections.Generic;

namespace NFSM{
   internal class Program{
      private static void Main(){
         var Q = new List<string>{"q0", "q1", "q2", "q3"};
         var Sigma = new List<char>{'0', '1'};
         var Delta = new List<Transition>{
            new Transition("q0", '0', "q0"),
            new Transition("q0", '1', "q0"),
            new Transition("q0", '1', "q1"),
            new Transition("q1", '0', "q2"),
            new Transition("q1", '1', "q2"),
            new Transition("q2", '0', "q3"),
            new Transition("q2", '1', "q3"),
         };
         var Q0 = "q0";
         var F = new List<string>{"q3"};
         var nFSM = new NonDeterministicFSM(Q, Sigma, Delta, Q0, F);

         nFSM.Accepts("10");
         nFSM.Accepts("00000100");
         nFSM.Accepts("101010101001010100");
         nFSM.Accepts("0000010");
         nFSM.Accepts("000001");
         nFSM.Accepts("0110101010000");
      }
   }
}