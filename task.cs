using System;
using System.Collections.Generic;
using System.IO;

public class ADD {
    public string name { get; set; }
    public string description { get; set; }

    public ADD(string name, string description) {
        this.name = name;
        this.description = description;
    }
}

class Program {
    static string taskDirectory = "tasks";

    public static void Main(string[] args) {
        List<ADD> list = new List<ADD>();

        // Load existing tasks from files
        LoadTasks(list);

        while (true) {
            detail();

            Console.WriteLine("Enter the number: ");
            if (int.TryParse(Console.ReadLine(), out int num)) {
                switch (num) {
                    case 1:
                        AddTask(list);
                        break;
                    case 2:
                        seeTask(list);
                        break;
                    case 3:
                        DeleteTask(list);
                        break;
                    case 4:
                        exite();
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } else {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    public static void detail() {
        Console.WriteLine("1 : Add Task");
        Console.WriteLine("2 : View Tasks");
        Console.WriteLine("3 : Delete Task");
        Console.WriteLine("4 : Exit");
    }

    public static void AddTask(List<ADD> list) {
        Console.WriteLine("Enter the task description: ");
        string description = Console.ReadLine();
        Console.WriteLine("Enter the task name: ");
        string name = Console.ReadLine();

        ADD task = new ADD(name, description);
        list.Add(task);

        // Save task to file
        SaveTaskToFile(task);

        Console.WriteLine("Task added successfully.");
    }

    public static void seeTask(List<ADD> list) {
        Console.WriteLine("Tasks:");
        foreach (ADD task in list) {
            Console.WriteLine($"Name: {task.name}, Description: {task.description}");
        }
    }

    public static void DeleteTask(List<ADD> list) {
        Console.WriteLine("Enter the name of the task to delete: ");
        string name = Console.ReadLine();
        int removedCount = list.RemoveAll(item => item.name == name);

        if (removedCount > 0) {
            // Delete task file
            DeleteTaskFile(name);
            Console.WriteLine("Task deleted successfully.");
        } else {
            Console.WriteLine("No task found with the given name.");
        }
    }

    public static void exite() {
        Console.WriteLine("Thank you for using the task manager.");
    }

    public static void SaveTaskToFile(ADD task) {
        if (!Directory.Exists(taskDirectory)) {
            Directory.CreateDirectory(taskDirectory);
        }
        string filePath = Path.Combine(taskDirectory, $"{task.name}.txt");
        File.WriteAllText(filePath, task.description);
    }

    public static void DeleteTaskFile(string name) {
        string filePath = Path.Combine(taskDirectory, $"{name}.txt");
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }
    }

    public static void LoadTasks(List<ADD> list) {
        if (Directory.Exists(taskDirectory)) {
            string[] files = Directory.GetFiles(taskDirectory, "*.txt");
            foreach (string file in files) {
                string name = Path.GetFileNameWithoutExtension(file);
                string description = File.ReadAllText(file);
                list.Add(new ADD(name, description));
            }
        }
    }
}
