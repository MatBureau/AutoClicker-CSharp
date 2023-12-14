using System;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace AutoClicker
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulator = new InputSimulator();
            bool stop = false;

            // Lire le fichier de configuration
            int delayBetweenClicks, executionTime;
            bool isDelayed = true;
            try
            {
                string[] configLines = File.ReadAllLines("config.txt");
                delayBetweenClicks = int.Parse(configLines[0]); //Délai entre les clics en millisecondes
                executionTime = int.Parse(configLines[1]);     // Temps d'exécution en secondes
                isDelayed = bool.Parse(configLines[2]);       //  Si oui ou non on doit appliquer un délai entre les clics

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier de configuration: {ex.Message}");
                return;
            }

            Console.WriteLine("Appuyez sur 'Entrée' pour démarrer l'AutoClicker, ou sur 'ESC' pour quitter.");

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("AutoClicker démarré. Appuyez sur 'ESC' pour arrêter.");

                    var startTime = DateTime.Now;
                    var compteur = 0;
                    // Cliquer pendant le temps d'exécution défini
                    while ((DateTime.Now - startTime).TotalSeconds < executionTime && !stop)
                    {
                        if (simulator.InputDeviceState.IsKeyDown(VirtualKeyCode.ESCAPE))
                        {
                            break;
                        }

                        simulator.Mouse.LeftButtonClick();
                        compteur++;
                        if(isDelayed) Thread.Sleep(delayBetweenClicks);

                    }

                    Console.WriteLine($"Cycle terminé. nombre de clics: {compteur}.  Appuyez sur 'Entrée' pour redémarrer ou sur 'ESC' pour quitter.");
                }
                else if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            Console.WriteLine("AutoClicker arrêté.");
        }
    }
}
