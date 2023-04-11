using CarRaceSimulatorFinalVersion.Models;

List<Car> cars = new List<Car>();
Console.ForegroundColor= ConsoleColor.Green;
Console.WriteLine("Select how many cars do you want to run the race:");
Console.ResetColor();
int numCars = int.Parse(Console.ReadLine());
for (int i = 0; i < numCars; i++)
{
    Console.ForegroundColor= ConsoleColor.Green;    
    Console.WriteLine($"Select the name of the car {i+1} : ");
    Console.ResetColor();
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
              
                winner = car;
                earliestFinishTime = car.FinishTime;
              
            }
        }

    }
    if (finishedCount==cars.Count)
    {
        Console.WriteLine($"All cars have finished the race. ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Car {winner.Name} finished first and is the winner!");
        Console.ResetColor();
        Console.WriteLine("Please press any key to exit.");

        break;
    }

}