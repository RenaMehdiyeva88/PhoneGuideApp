# PhoneGuideApp

A C# (.NET 10) console application for managing a simple phone contact list, built to demonstrate different low-level file I/O approaches in C# (raw `FileStream` vs. `StreamWriter`/`StreamReader`). Contacts and log files are stored as plain text files on disk.

## Features

- Add a new contact (name, surname, phone number)
- View all saved contacts
- Search for a contact by name
- Attempt to update a contact (looks up the contact; note: the actual update logic is not yet implemented)
- Create a large log file using raw `FileStream` writes
- Create a large log file using `StreamWriter`
- Read a log file in chunks using raw `FileStream` reads
- Read a log file line by line using `StreamReader`

## Technologies

- C#
- .NET 10.0
- Console application
- File I/O (FileStream, StreamWriter, StreamReader)

## Requirements

- .NET SDK 10.0 or higher installed (https://dotnet.microsoft.com/download)
- Important: the app uses hardcoded folder paths (`C:\PB501Files\PhoneContactList` and `...\Logs`). You'll need to create these folders on your machine (or update the paths in `Program.cs`) before running the app, especially if you're not on Windows. The log-reading options also expect a specific existing log file name (`log_20260201_183144.txt`) — update this to match a log file you've actually generated.

## Installation and Run

git clone https://github.com/RenaMehdiyeva88/PhoneGuideApp.git
cd PhoneGuideApp

# Create the folders the app expects (Windows example):
mkdir C:\PB501Files\PhoneContactList
mkdir C:\PB501Files\PhoneContactList\Logs

dotnet run

## Usage Example

After running the program, a menu will appear:

1. Add Contact
2. View Contacts
3. Search Contact
4. Delete Contact
5. Create Log
6. Create Log with StreamReader
7. Read Log
8. Read Log with StreamReader
9. Exit
Please select option (1-9):

Choose an option by entering the corresponding number and follow the on-screen instructions. Note: creating a log file writes up to 1 GB of data, so it may take a while.

## Project Structure

PhoneGuideApp/
├── Program.cs               # Entry point: contacts management and file I/O demos
├── PhoneGuideApp.csproj
└── README.md

## Author

RenaMehdiyeva88 (https://github.com/RenaMehdiyeva88)
