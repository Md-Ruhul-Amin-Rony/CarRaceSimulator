using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceSimulatorFinalVersion.Models
{
    public class Car
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public int Speed { get; set; }
        public Thread thread { get; set; }
        public Random random { get; set; }
        public bool Finished { get; set; }
        public DateTime LastEventTime { get; set; }

        public Car( string name )
        {
            this.Name= name;    
            this.Distance = 0;  
            this.Speed = 120;
            this.thread = new Thread(new ThreadStart(this.Drive));
            this.random = new Random();
            this.Finished= false;
            this.LastEventTime= DateTime.MinValue;
                
        }
        public void Start() 
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Car Racing Competition::-");
            Console.ResetColor(); // Reset console colors to default

            Console.WriteLine($"\n\n{this.Name} starts the race!");
            Console.WriteLine("\n\n" + new string('-', 60));
            Console.WriteLine($"\n\n{" ",28}\u001b[1m\u001b[36m {this.Name} \u001b[0m\u001b[1mstarts the race! \u001b[0m\n\n");

        }
        public void Drive()
        {
            DateTime LastEventTime = DateTime.Now;
            while (!Finished)
            {
                int distanceTravelled = this.Speed * 1000 / 3600;// convert from km/h to m/s
                this.Distance += distanceTravelled;
                int currentSpeed= this.Speed;
                Thread.Sleep(1000);

                DateTime now= DateTime.Now;

                if((now-LastEventTime).TotalSeconds>=30)
                {
                    int eventProbability = this.random.Next(1, 51);
                    if( eventProbability == 1 ) 
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine($"{this.Name} is out of gas!! Refuelling...... Wait for 30 seconds");
                        Console.ResetColor();
                        currentSpeed = 0;
                        Thread.Sleep(30000);
                        currentSpeed= this.Speed;
                    }
                    else if (eventProbability<=3)
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine($"{this.Name} has a puncture!! Changign tire.... wait for 20 seconds");
                        Console.ResetColor();
                        currentSpeed = 0;
                        Thread.Sleep(20000);
                        currentSpeed = this.Speed;

                    }
                    else if (eventProbability<=8)
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine($"{this.Name} has a bird on the windsheild! Cleaning it.. wait for 10 seconds");

                    }

                }

            }
        }


    }
}
