# Tic-Tac-Two

Tic-Tac-Two is a university project written in C# that extends the classic Tic-Tac-Toe game with additional mechanics. The game supports both console-based and web-based gameplay and includes a fully functional persistence layer for saving game states and configurations.

## Features

- 🎮 **Cross-Platform Gameplay** – Play in the console or in a web-based UI.
- 💾 **Game State Persistence** – Automatically saves game progress and settings.
- 🛠 **Pre-Configured Database** – Migrations and database files are already included.
- 🔄 **Fully Functional Data Layer** – Built with SQLite and Entity Framework Core.

## Getting Started

### 1. Clone the Repository
```sh
git clone https://github.com/yourusername/tic-tac-two.git
cd tic-tac-two
```

### 2. Build the Project
```sh
dotnet build
```

### 3. Run the Console Version
```sh
dotnet run --project ConsoleApp
```

### 4. Run the Web Version
```sh
dotnet run --project WebApp
```

## Database Setup
No additional setup is required for the database! 🎉 The necessary migrations and `app.db` file are included in the repository. The game will work out of the box without manual database configuration.

## Technologies Used
- **C#** – Primary programming language
- **.NET** – Application framework
- **Entity Framework Core** – ORM for database interactions
- **SQLite** – Lightweight embedded database
- **ASP.NET Core** – Web application framework

## Why This Project?
This project showcases a structured and scalable approach to game development in C#. It demonstrates:
- Clean architecture principles
- Separation of concerns (UI, logic, and data layers)
- Proper use of Entity Framework for database management
- Console and WebApp integration

## Contact
If you have any questions or want to discuss this project, feel free to reach out via GitHub or LinkedIn.
