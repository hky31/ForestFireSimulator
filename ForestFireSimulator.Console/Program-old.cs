// using System;
// using System.Threading;
// using ForestFireSimulator.Console.Simulator;

// public class Program
// {
//     /// <summary>
//     /// methode principale appelant les fonctions du simulateurs de feu de foret
//     /// </summary>
//     public static void Main()
//     {
//         var simulator = new ForestFireSimulatorSystem();
//         int counter = 0;

//         while (counter < 10)
//         {
//             var forest = simulator.GetForest();

//             // Affichage externe
//             PrintForest(forest);
//             counter++;

//             simulator.Step();
//             Thread.Sleep(1000);
//         }
//     }

//     /// <summary>
//     /// methode pour afficher l'etat de la foret dans la console
//     /// </summary>
//     /// <param name="forest"></param>
//     private static void PrintForest(TreeState[,] forest)
//     {
//         int size = forest.GetLength(0);

//         for (int i = 0; i < size; i++)
//         {
//             for (int j = 0; j < size; j++)
//             {
//                 switch (forest[i, j])
//                 {
//                     case TreeState.Empty: Console.Write("⬜️"); break;
//                     case TreeState.Tree: Console.Write("🌳"); break;
//                     case TreeState.Fire: Console.Write("🔥"); break;
//                     case TreeState.Ash: Console.Write("⬛️"); break;
//                 }
//             }
//             Console.WriteLine();
//         }
//         Console.WriteLine();
//         Console.WriteLine();
//     }
// }
