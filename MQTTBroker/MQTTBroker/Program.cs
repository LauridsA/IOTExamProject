using System;

namespace MQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 12393;
            Console.WriteLine("Broker");
            Console.WriteLine("...........................................");
            Console.WriteLine("Press A to add client to authorization system.");
            Console.WriteLine("Press Q to kill the broker.");
            BrokerService bs = new BrokerService();
            bs.Setup(port);
            Console.WriteLine("Broker running on port " + port);

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.A)
                {
                    Console.WriteLine("Create user mode. Enter clientId");
                    string cid = "";
                    string user = "";
                    string pass = "";
                    cid = Console.ReadLine();
                    Console.WriteLine($"Client id entered as '{cid}'. {Environment.NewLine} Enter username.");
                    user = Console.ReadLine();

                    Console.WriteLine($"Username entered as '{user}'. {Environment.NewLine} Enter password.");
                    pass = Console.ReadLine();

                    Console.WriteLine($"Password entered as '{pass}'. {Environment.NewLine}");
                    bs.AddClientToAuth(cid, user, pass);

                    Console.Write("User added." + Environment.NewLine);
                }
                if (Console.ReadKey().Key == ConsoleKey.Q)
                    break;

            }

            Console.WriteLine(Environment.NewLine + "BROKER SHUTDOWN");
        }
    }
}
