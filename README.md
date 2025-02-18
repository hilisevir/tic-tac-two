# Tic-Tac-Two

Tic-Tac-Two is a university project written in C# that extends the classic Tic-Tac-Toe game with additional mechanics. The game supports both console-based and web-based gameplay and includes a fully functional persistence layer for saving game states and configurations. The project demonstrates skills in software architecture, database integration, and web development using Razor Pages.

## Features

- 🎮 **Multi-Platform Support** – Play the game in the console or in a web browser.
- 💾 **Game Persistence** – Save and load games, as well as create and store custom game configurations.
- 🔄 **Flexible Storage Options** – Choose between saving data in a database (SQLite) or using the file system.
- 🌐 **Multiplayer Web Mode** – Two players can join the same game from separate browser windows using a password.
- 🛠 **Built with Razor Pages** – The web interface is designed using ASP.NET Razor Pages.

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- A compatible IDE (e.g., Visual Studio, JetBrains Rider, or VS Code)

### 1. Clone the Repository
```sh
git clone https://github.com/hilisevir/tic-tac-two.git
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

### Switching the Storage Method
By default, the game uses an SQLite database for storing game states and configurations. If you prefer to store data in files instead:
1. Open the `RepositoryHelper` file.
2. Comment out the lines related to database storage.
3. Uncomment the lines related to file-based storage.
4. Rebuild and run the project.

## Database Setup
No additional setup is required for the database! 🎉 The necessary migrations and `app.db` file are included in the repository. The game will work out of the box without manual database configuration.

## Technologies Used
- **C#** – Primary programming language
- **ASP.NET Razor Pages** – Server-side web application model for building dynamic web interfaces
- **Entity Framework Core** – ORM for database interactions
- **SQLite** – Lightweight embedded database

## Why This Project?
This project showcases a structured and scalable approach to game development in C#. It demonstrates:
- Clean architecture principles
- Separation of concerns (UI, logic, and data layers)
- Proper use of Entity Framework for database management
- Console and WebApp integration

## License
This project is for educational purposes and does not have a formal license. Feel free to modify and experiment with it!
