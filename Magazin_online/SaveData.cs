using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProgramStateManagement
{
    // Model de date pentru a fi salvat/incarcat
    public class ProgramData
    {
        public List<string> Items { get; set; } = new List<string>();
    }

    // Serviciul pentru salvare/incarcare
    public class PersistenceService
    {
        private readonly string _filePath;

        public PersistenceService(string filePath)
        {
            _filePath = filePath;
        }

        // Metoda de salvare
        public void SaveData(ProgramData data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        // Metoda de incarcare
        public ProgramData LoadData()
        {
            if (!File.Exists(_filePath))
                return new ProgramData();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<ProgramData>(json) ?? new ProgramData();
        }
    }

    // Clasa principala a programului
    public class ProgramCore
    {
        private readonly ProgramData _data;
        private readonly PersistenceService _persistenceService;

        public ProgramCore(PersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
            _data = _persistenceService.LoadData();
        }

        public void AddItem(string item)
        {
            _data.Items.Add(item);
        }

        public void DisplayItems()
        {
            Console.WriteLine("Current Items:");
            foreach (var item in _data.Items)
            {
                Console.WriteLine($"- {item}");
            }
        }

        public void SaveAndExit()
        {
            _persistenceService.SaveData(_data);
            Console.WriteLine("Program state saved successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "programData.json";
            var persistenceService = new PersistenceService(filePath);
            var program = new ProgramCore(persistenceService);

            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display Items");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter an item to add: ");
                        var item = Console.ReadLine();
                        program.AddItem(item);
                        break;
                    case "2":
                        program.DisplayItems();
                        break;
                    case "3":
                        program.SaveAndExit();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
