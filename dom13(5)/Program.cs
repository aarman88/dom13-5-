using System;
using System.Collections.Generic;
using System.Linq;

public class Client
{
    public int Id { get; }
    public string Name { get; set; }
    public string ServiceType { get; set; }
    public bool IsVIP { get; set; }

    public Client(int id)
    {
        Id = id;
    }
}

public class Bank
{
    private Queue<Client> clientQueue = new Queue<Client>();
    private List<Client> servedClients = new List<Client>();

    public void EnqueueClient(string name, string serviceType, bool isVIP = false)
    {
        Client newClient = new Client(clientQueue.Count + 1)
        {
            Name = name,
            ServiceType = serviceType,
            IsVIP = isVIP
        };

        clientQueue.Enqueue(newClient);

        Console.WriteLine($"Клиент {newClient.Name} добавлен в очередь для {newClient.ServiceType}.");
        DisplayQueueStatus();
    }

    public void ServeNextClient()
    {
        if (clientQueue.Count > 0)
        {
            Client currentClient = clientQueue.Dequeue();
            servedClients.Add(currentClient);

            Console.WriteLine($"Администратор обслуживает клиента {currentClient.Name} ({currentClient.ServiceType}).");
            DisplayQueueStatus();
        }
        else
        {
            Console.WriteLine("Очередь пуста. Нет клиентов для обслуживания.");
        }
    }

    private void DisplayQueueStatus()
    {
        if (clientQueue.Count > 0)
        {
            Console.WriteLine("Текущее состояние очереди:");
            foreach (var client in clientQueue)
            {
                Console.WriteLine($"Клиент {client.Name} ({client.ServiceType}){(client.IsVIP ? " - VIP" : "")}");
            }
        }
        else
        {
            Console.WriteLine("Очередь пуста.");
        }

        DisplayAverageWaitTime();
    }

    private void DisplayAverageWaitTime()
    {
        if (servedClients.Count > 0)
        {
            double totalWaitTime = servedClients.Sum(client => clientQueue.Count * 1000);
            double averageWaitTime = totalWaitTime / servedClients.Count;

            Console.WriteLine($"Среднее время ожидания: {averageWaitTime / 1000:F2} сек.");
        }
    }

    public void DisplayServedClientsHistory()
    {
        Console.WriteLine("История обслуженных клиентов:");
        foreach (var client in servedClients)
        {
            Console.WriteLine($"Клиент {client.Name} ({client.ServiceType}){(client.IsVIP ? " - VIP" : "")}");
        }
    }
}

class Program
{
    static void Main()
    {
        Bank bank = new Bank();

        while (true)
        {
            Console.WriteLine("1. Добавить клиента в очередь");
            Console.WriteLine("2. Обслужить следующего клиента");
            Console.WriteLine("3. Выйти");
            Console.WriteLine("4. Просмотреть историю обслуженных клиентов");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите ваше имя: ");
                    string name = Console.ReadLine();

                    Console.Write("Выберите услугу (Кредитование, Открытие вклада, Консультация): ");
                    string serviceType = Console.ReadLine();

                    Console.Write("Является ли клиент VIP? (yes/no): ");
                    bool isVIP = Console.ReadLine().ToLower() == "yes";

                    bank.EnqueueClient(name, serviceType, isVIP);
                    break;

                case "2":
                    bank.ServeNextClient();
                    break;

                case "3":
                    Console.WriteLine("Программа завершена.");
                    return;

                case "4":
                    bank.DisplayServedClientsHistory();
                    break;

                default:
                    Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие 1, 2, 3 или 4.");
                    break;
            }
        }
    }
}
