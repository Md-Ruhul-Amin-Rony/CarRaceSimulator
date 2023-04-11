using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceSimulatorFinalVersion.Models
{
    public class Car
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public int Speed { get; set; } = 120; //km/h

        public bool Finished { get; set; } = false;

        public Thread thread { get; set; }
        public DateTime FinishTime { get; set; } //To detect the winner car.

        public Car(string name)
        {
            Name = name;
            Distance = 0;

            thread = new Thread(new ThreadStart(Drive));// each car will Drive on its own thread


        }
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Car Racing Competition::-");
            Console.ResetColor();
            Console.WriteLine($"\n\n{" ",28}\u001b[1m\u001b[36m {Name} \u001b[0m\u001b[1mstarts the race! \u001b[0m\n\n");
            thread.Start();//starts the execution of the Drive() method on a separate thread.

        }


        public void Drive()
        {
            Random random = new Random();
            int incidentTimer = 0;// to generate event with 30 seconds interval



            while (!Finished)
            {

                int distanceTravelled;
                int currentSpeed;

                lock (this)
                {
                    distanceTravelled = Speed * 1000 / 3600; // convert from km/h to m/s
                    Distance += distanceTravelled;
                    currentSpeed = Speed;


                    if (Distance >= 10000)
                    {
                        Distance = 10000;
                        Finished = true;
                        FinishTime = DateTime.Now;
                    }
                }
                Console.WriteLine($"Car {Name} is at {Distance} meters, speed is {currentSpeed} km/h");//comment this code if you do not want to see every seconds update of the car.
                if (Distance >= 10000)
                {
                    Finished = true;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"WOW!!! {Name} has finished the Race");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please press 'Enter' to see the Winner!!");
                    Console.ResetColor();
                    return;

                }
                incidentTimer++;
                if (incidentTimer >= 30) // to check if the incidentTimer has exceed 30 seconds
                {
                    int incidentProbability = random.Next(1, 51);// from 1 to 50 corresponds to a specific incident that the car may encounter during the race.
                    lock (this)// to ensure that only one thread can execute the incident code at a time.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" {Name} has encountered an incident");// every 30 seconds car will meet an incident and depend on the probabilty effect incident will take place.
                        Console.ResetColor();

                        /*  Incident 1: Out of gas - refuelling stops for 30 seconds (Probability: 1/50)
                            Incident 2-3: Puncture - repairing stops for 20 seconds (Probability: 2/50)
                            Incident 4-8: Bird on the windshield - washing stops for 10 seconds (Probability: 5/50)
                            Incident 9-18: Engine failure - speed reduced by 1 km/h (Probability: 5/50)*/


                        switch (incidentProbability)
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Name} is out of gas and refulling stops for 30 seconds");
                                Console.ResetColor();
                                Thread.Sleep(30000);
                                break;
                            case 2:
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Name} has a puncture & repairing stops for 20 seconds");
                                Console.ResetColor();
                                Thread.Sleep(20000);
                                break;
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"{Name} has a bird on the WindSheild & washing stops for 10 seconds");
                                Console.ResetColor();
                                Thread.Sleep(10000);
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:

                            case 15:
                            case 16:
                            case 17:

                            case 18:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Name} has an engine failure and reduced speed by 1km/h.");
                                Console.ResetColor();
                                lock (this)
                                {
                                    Speed -= 1;

                                }

                                break;
                            default:
                                Console.ForegroundColor= ConsoleColor.DarkRed;
                                Console.WriteLine($"{Name} Car runs smoothly and did not meet any incident because of incident probability effect");
                                Console.ResetColor();
                                break;

                        }
                    }
                    incidentTimer = 0;

                }
                Thread.Sleep(1000); // use to delay the execution of the current loop by 1 seconds. if we want to show on console more faster then just need to reduce the time amount.



            }
        }







    }
}
