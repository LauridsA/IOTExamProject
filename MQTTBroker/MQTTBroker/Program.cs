using System;

namespace MQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunStuff();
            }
            catch (Exception e)
            {
                Main(args);
            }
            Console.WriteLine(Environment.NewLine + "BROKER SHUTDOWN");
        }

        public static void RunStuff()
        {
            try
            {
                int port = 12393;
                Console.WriteLine("Broker");
                Console.WriteLine("...........................................");
                Console.WriteLine("Press A to add client to authorization system.");
                Console.WriteLine("Press C to see connected clients.");
                Console.WriteLine("Press Q to kill the broker.");
                BrokerService bs = new BrokerService();
                bs.Setup(port);
                Console.WriteLine("Broker running on port " + port);

                while (true)
                {
                    var input = Console.ReadKey().Key;
                    if (input == ConsoleKey.A)
                    {
                        Console.WriteLine($"{Environment.NewLine} Create user mode. Enter clientId {Environment.NewLine}");
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
                    if (input == ConsoleKey.C)
                    {
                        Console.WriteLine(Environment.NewLine + "Total connections: " + bs.GetServer().ActiveConnections + Environment.NewLine + "Connections: ");

                        foreach (var o in bs.GetServer().ActiveClients)
                            Console.WriteLine("- " + o + " is connected");
                    }

                    if (input == ConsoleKey.Q)
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("_________________EXCEPTION CAUGHT. MESSAGE:");
                Console.WriteLine(e.Message);
                Console.WriteLine("_________________STACK:");
                Console.WriteLine(e);
                Console.WriteLine("_________________");
            }
        }
    }
}
