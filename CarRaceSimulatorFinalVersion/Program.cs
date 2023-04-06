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
    Car winner = null;
    foreach (Car car in cars)
    {
        if (car.Finished)
        {
            finishedCount++;
            if (winner==null || car.Distance>winner.Distance)
            {
                winner = car;

            }
        }

    }
    if (finishedCount==cars.Count)
    {
        Console.WriteLine($"All cars have finished the race. {winner.Name} is the winner with a distance of {winner.Distance} meters");

        break;
    }

}