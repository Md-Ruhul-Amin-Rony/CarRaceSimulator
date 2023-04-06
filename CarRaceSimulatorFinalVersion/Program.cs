using CarRaceSimulatorFinalVersion.Models;

List<Car> cars = new List<Car>();
Console.WriteLine("Select how many cars do you want to run the race:");
int numCars = int.Parse(Console.ReadLine());
for (int i = 0; i < numCars; i++)
{
    Console.WriteLine($"Select the name of the car {i+1} : ");
    string name = Console.ReadLine();

    cars.Add(new Car(name));

}
foreach (Car car in cars)
{
    car.Start();

}
while (true)
{
    Console.WriteLine("Please type 'status' to see the current status of the cars:-");
    string input = Console.ReadLine();
    if (input.ToLower()=="status")
    {
        Console.WriteLine(" Car\tDistance\tSpeed");
        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Name}\t{car.Distance} meters\t{car.Speed} km/h");

        }
    }
    int finishedCount = 0;
    DateTime earliestFinishTime = DateTime.MaxValue;
    Car winner = null;
    foreach (Car car in cars)
    {
        if (car.Finished)
        {
            finishedCount++;
            if (car.FinishTime < earliestFinishTime)
            {
               // winner = cars.OrderByDescending(c => c.Distance).FirstOrDefault();
                winner = car;
                earliestFinishTime = car.FinishTime;
                //Console.ForegroundColor = ConsoleColor.DarkMagenta;
                //Console.WriteLine($"Winner is {winner.Name}");
                //Console.ResetColor();

            }
        }

    }
    if (finishedCount==cars.Count)
    {
        Console.WriteLine($"All cars have finished the race. ");
        Console.WriteLine($"Car {winner.Name} finished first and is the winner!");
        Console.WriteLine("Please press any key to exit.");

        break;
    }

}