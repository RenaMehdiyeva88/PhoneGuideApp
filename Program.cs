using System.Security.AccessControl;

namespace PhoneGuideApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Welcome to Phone Guide App!");

            int choice;

            do
            {
                ShowMenu();
                Console.WriteLine("Please select option (1-9): ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        ViewContacts();
                        break;
                    case 3:
                        SearchContact();
                        break;
                    case 4:
                        program.UpdateContact();
                        break;
                    case 5:
                        CreateLog();
                        break;
                    case 6:
                        program.CreateLogWithStreamWriter();
                        break;
                    case 7:
                        program.ReadLog(100);
                        break;
                    case 8:
                        program.ReadLogWithStreamReader();
                        break;
                    case 9:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                Console.WriteLine();

            } while (choice != 9);
        }
        public static void ShowMenu()
        {
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Search Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Create Log");
            Console.WriteLine("6. Create Log with StreamReader");
            Console.WriteLine("7. Read Log");
            Console.WriteLine("8. Read Log with StreamReader");
            Console.WriteLine("9. Exit");
        }

        public static void AddContact()
        {
            Console.WriteLine("Adding a new contact...");
            var directoryPath = @"C:\PB501Files\PhoneContactList";
            var fileName = "contacts.txt";
            var filePath = Path.Combine(directoryPath, fileName);

            Console.WriteLine("Enter contact name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter contact surname: ");
            var surname = Console.ReadLine();
            Console.WriteLine("Enter contact phone number: ");
            string phoneNumber = Console.ReadLine();
            string contactInfo = $"Name: {name}, Surname: {surname}, Phone number: {phoneNumber}";

            using FileStream fs2 = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            var bytes2 = System.Text.Encoding.UTF8.GetBytes(contactInfo + Environment.NewLine);
            fs2.Write(bytes2);
            Console.WriteLine("Contact added successfully.");
        }

        public static void ViewContacts()
        {
            using FileStream fs = new FileStream(@"C:\PB501Files\PhoneContactList\contacts.txt", FileMode.Open, FileAccess.Read);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            var contacts = System.Text.Encoding.UTF8.GetString(buffer);
            Console.WriteLine("Contact list: ");
            Console.WriteLine(contacts);
        }

        public static string SearchContact()
        {
            Console.WriteLine("Searching for a contact...");
            Console.WriteLine("Enter contact name to search: ");
            string searchName = Console.ReadLine();
            using FileStream fs = new FileStream(@"C:\PB501Files\PhoneContactList\contacts.txt", FileMode.Open, FileAccess.Read);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            var contacts = System.Text.Encoding.UTF8.GetString(buffer);
            var contactLines = contacts.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in contactLines)
            {
                if (line.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Contact found: ");
                    Console.WriteLine(line);
                    return line;
                }
            }
            return string.Empty;
        }

        public static void CreateLog()
        {
            var logDirectory = @"C:\PB501Files\PhoneContactList\Logs";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            var logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            using FileStream logFileStream = new FileStream(logFilePath, FileMode.Create, FileAccess.Write);


            long targetSize = 1024L * 1024L * 1024L;
            long written = 0;

            int lineNumber = 0;
            while (written < targetSize)
            {
                var logMessage = $"{lineNumber} Log created on {DateTime.Now} " + Environment.NewLine;
                var logBytes = System.Text.Encoding.UTF8.GetBytes(logMessage);
                logFileStream.Write(logBytes, 0, logBytes.Length);
                written += logBytes.Length;
                lineNumber++;
            }
            Console.WriteLine("Log file created successfully!");
        }
        public void CreateLogWithStreamWriter()
        {
            var logDirectory = @"C:\PB501Files\PhoneContactList\Logs";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            var logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            using StreamWriter writer = new StreamWriter(logFilePath);
            long targetSize = 1024L * 1024L * 1024L; 
            long written = 0;
            int lineNumber = 0;
            while (written < targetSize)
            {
                var logMessage = $"{lineNumber} Log created on {DateTime.Now} " + Environment.NewLine;
                writer.Write(logMessage);
                written += System.Text.Encoding.UTF8.GetByteCount(logMessage);
                lineNumber++;
            }
            Console.WriteLine("Log file created successfully with StreamWriter!");
        }
        public void ReadLog(int chunkSize)
        {
            var logFilePath = @"C:\PB501Files\PhoneContactList\Logs";
            var fileName = "log_20260201_183144.txt";
            var fullPath = Path.Combine(logFilePath, fileName);
            using FileStream logFileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[chunkSize];
            int bytesRead;
            while ((bytesRead = logFileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                var logChunk = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Thread.Sleep(1000);
                Console.WriteLine(logChunk);
            }

        }
        public void ReadLogWithStreamReader()
        {
            var logFilePath = @"C:\PB501Files\PhoneContactList\Logs";
            var fileName = "log_20260201_183144.txt";
            var fullPath = Path.Combine(logFilePath, fileName);
            using StreamReader reader = new StreamReader(fullPath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Thread.Sleep(500);
                Console.WriteLine(line);
            }

        }
        public void UpdateContact()
        {
            Console.WriteLine("Updating a contact...");
            var directoryPath = @"C:\PB501Files\PhoneContactList";
            var fileName = "contacts.txt";
            var filePath = Path.Combine(directoryPath, fileName);
            var updatedContact = SearchContact();
            if (string.IsNullOrEmpty(updatedContact))
            {
                Console.WriteLine("Contact not found. Cannot update.");
                return;
            }

        }
    }
}