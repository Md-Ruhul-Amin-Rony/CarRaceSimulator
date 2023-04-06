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
                                              // public Random random { get; set; }
        public bool Finished { get; set; }=false;
        //public DateTime LastEventTime { get; set; }
        public Thread thread { get; set; }
        public DateTime FinishTime { get; set; }

        public Car(string name)
        {
            Name = name;
            Distance = 0;
            //Speed = 120;
            thread = new Thread(new ThreadStart(Drive));
            //random = new Random();
            //Finished = false;
            // LastEventTime = DateTime.MinValue;

        }
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Car Racing Competition::-");
            Console.ResetColor(); // Reset console colors to default

           // Console.WriteLine($"\n\n{Name} starts the race!");
            //Console.WriteLine("\n\n" + new string('-', 60));
            Console.WriteLine($"\n\n{" ",28}\u001b[1m\u001b[36m {Name} \u001b[0m\u001b[1mstarts the race! \u001b[0m\n\n");
            thread.Start();

        }
        //public void Drive()
        //{
        //    DateTime LastEventTime = DateTime.Now;
        //    while (!Finished)
        //    {
        //        int distanceTravelled = this.Speed * 1000 / 3600;// convert from km/h to m/s
        //        this.Distance += distanceTravelled;
        //        int currentSpeed= this.Speed;
        //        Thread.Sleep(1000);

        //        DateTime now= DateTime.Now;

        //        if((now-LastEventTime).TotalSeconds>=30)
        //        {
        //            int eventProbability = this.random.Next(1, 51);
        //            if( eventProbability == 1 ) 
        //            {
        //                Console.ForegroundColor= ConsoleColor.Red;
        //                Console.WriteLine($"{this.Name} is out of gas!! Refuelling...... Wait for 30 seconds");
        //                Console.ResetColor();
        //                currentSpeed = 0;
        //                Thread.Sleep(30000);
        //                currentSpeed= this.Speed;
        //            }
        //            else if (eventProbability<=2)
        //            {
        //                Console.ForegroundColor= ConsoleColor.Red;
        //                Console.WriteLine($"{this.Name} has a puncture!! Changign tire.... wait for 20 seconds");
        //                Console.ResetColor();
        //                currentSpeed = 0;
        //                Thread.Sleep(20000);
        //                currentSpeed = this.Speed;

        //            }
        //            else if (eventProbability<=5)
        //            {
        //                Console.ForegroundColor= ConsoleColor.Red;
        //                Console.WriteLine($"{this.Name} has a bird on the windsheild! Cleaning it.. wait for 10 seconds");
        //                Console.ResetColor();
        //                currentSpeed= 0;
        //                Thread.Sleep(10000);
        //                currentSpeed= this.Speed;
        //            }
        //            else if (eventProbability<=10)
        //            {
        //                Console.ForegroundColor=ConsoleColor.Red;
        //                Console.WriteLine($"{this.Name} has an Engine failure!! Reducing speed--- by 1km/h");
        //                Console.ResetColor();
        //                this.Speed--;
        //                //currentSpeed--;

        //            }
        //            LastEventTime= now; 

        //        }
        //        if (this.Distance>=10000)
        //        {
        //            Console.ForegroundColor = ConsoleColor.DarkCyan;
        //            Console.WriteLine($"WOW!!! {this.Name} has finished the Race");
        //            Console.ResetColor();
        //            this.Finished = true;   
        //        }

        //    }
        //}

        public void Drive()
        {
            Random random = new Random();
            int eventTimer = 0;



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
                        Distance = 10000    ;
                        Finished = true;
                        FinishTime = DateTime.Now;
                    }
                }
                Console.WriteLine($"Car {Name} is at {Distance} meters, speed is {currentSpeed} km/h");
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
                eventTimer++;
                if (eventTimer >= 30)
                {
                    int eventProbability = random.Next(1, 11);
                    lock (this)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" {Name} has encountered an event");
                        Console.ResetColor();

                        switch (eventProbability)
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Name} has a bird on the WindSheild & washing stops for 10 seconds");
                                Console.ResetColor();
                                Thread.Sleep(10000);
                                break;
                                case 9:
                            case 10:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Name} has an engine failure and reduced speed by 1km/h.");
                                Console.ResetColor();
                                lock (this)
                                {
                                    Speed -= 1;

                                }

                                break;

                        }
                    }
                    eventTimer = 0;

                }
                Thread.Sleep(100); 


                //int distanceTravelled = Speed * 1000 / 3600; // convert from km/h to m/s
                //Distance += distanceTravelled;
                //int currentSpeed = Speed;

                //DateTime now = DateTime.Now;

                // Check if 30 seconds have passed since the last event for this car
                // if ((now - LastEventTime).TotalSeconds >= 30)
                //if (DateTime.Now.Second % 30 == 0)



                //{
                //    int eventProbability = random.Next(1, 51);
                //    if (eventProbability == 1)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine($"{Name} is out of gas!! Refuelling...... Wait for 30 seconds");
                //        Console.ResetColor();
                //        currentSpeed = 0;
                //        Thread.Sleep(30000);
                //        currentSpeed = Speed;
                //    }
                //    else if (eventProbability <= 2)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine($"{Name} has a puncture!! Changing tire.... wait for 20 seconds");
                //        Console.ResetColor();
                //        currentSpeed = 0;
                //        Thread.Sleep(20000);
                //        currentSpeed = Speed;
                //    }
                //    else if (eventProbability <= 5)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine($"{Name} has a bird on the windshield! Cleaning it.. wait for 10 seconds");
                //        Console.ResetColor();
                //        currentSpeed = 0;
                //        Thread.Sleep(10000);
                //        currentSpeed = Speed;
                //    }
                //    else if (eventProbability <= 10)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine($"{Name} has an Engine failure!! Reducing speed--- by 1km/h");
                //        Console.ResetColor();
                //        Speed -= 1;
                //    }
                //    LastEventTime = now; // update the last event time for this car
                //}

                //if (Distance >= 10000)
                //{
                //    Console.ForegroundColor = ConsoleColor.DarkCyan;
                //    Console.WriteLine($"WOW!!! {Name} has finished the Race");
                //    Console.ResetColor();
                //    Finished = true;
                //}

                //Thread.Sleep(1000); // wait for 1 second before continuing the loop
            }
        }







    }
}
