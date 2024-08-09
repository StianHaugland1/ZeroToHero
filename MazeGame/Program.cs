// See https://aka.ms/new-console-template for more information

var maze = new char[,] {};
var score = 0;
var amountOfTresures = 5;
string? message = null;
var defaultMessageColor = ConsoleColor.Blue;
var messageColor = defaultMessageColor;
var scoreColor = ConsoleColor.Green;
var playerColor = ConsoleColor.Blue;
var tresureColor = ConsoleColor.Yellow;
var stillPlaying = true;
var winningMessage = "Wohooo you won!";

void Run()
{
    maze = GenerateMaze();

    while(stillPlaying){
        DrawMaze(maze);
        DisplayScore();
        HandleMessage();
        HandleInput();
        CheckIfPlayerWon();
    }
    Console.Clear();
    DrawWithColor(winningMessage, ConsoleColor.Green, true);
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

void CheckIfPlayerWon()
{
    if (score == amountOfTresures)
    {
        message = "You won!";
        messageColor = ConsoleColor.Green;
        stillPlaying = false;
    }
}

void HandleMessage()
{
    if (message == null) return;
 
    Console.SetCursorPosition(0, maze.GetLength(1) + 2);
    DrawWithColor(message, messageColor);
    message = null;
    messageColor = defaultMessageColor;
}

void DisplayScore()
{
    Console.SetCursorPosition(0, maze.GetLength(1) + 1);
    DrawWithColor($"Score: {score}", scoreColor);
}

void HandleInput()
{
    var key = Console.ReadKey().Key;
    switch (key)
    {
        case ConsoleKey.UpArrow:
            HandlePlayerMovement(0, -1);
            break;
        case ConsoleKey.DownArrow:
            HandlePlayerMovement(0, 1);
            break;
        case ConsoleKey.LeftArrow:
            HandlePlayerMovement(-1, 0);
            break;
        case ConsoleKey.RightArrow:
            HandlePlayerMovement(1, 0);
            break;
    }
}

void HandlePlayerMovement(int dx, int dy)
{
    var playerPosition = FindPlayerPosition();
    (int x, int y) newPlayerPosition = (playerPosition.x + dx, playerPosition.y + dy);
    
    switch (maze[newPlayerPosition.x, newPlayerPosition.y])
    {
        case ' ':
            MovePlayerToPosition(playerPosition, newPlayerPosition);
            break;
        case '#':
            message = "You hit a wall!";
            messageColor = ConsoleColor.Red;
            break;
        case '$':
            score++;
            MovePlayerToPosition(playerPosition, newPlayerPosition);
            message = "You found a tresure!";
            messageColor = ConsoleColor.Yellow;
            break;
        default:
            message = "You hit something!";
            break;
    }
}

void MovePlayerToPosition((int x, int y) oldPosition, (int x, int y) newPosition)
{
    maze[oldPosition.x, oldPosition.y] = ' ';
    maze[newPosition.x, newPosition.y] = '@';
}

(int x, int y) FindPlayerPosition()
{
    for (int x = 0; x < maze.GetLength(0); x++)
    {
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[x, y] == '@')
            {
                return (x, y);
            }
        }
    }
    return (-1, -1);
}

char[,] GenerateMaze(int width = 20, int height = 10)
{
    var maze = new char[width, height];
    AddEmptySpace(maze);
    AddWalls(maze);
    AddPlayer(maze);
    AddTresures(maze);
    return maze;
}

void AddTresures(char[,] maze)
{
    for (int i = 0; i < amountOfTresures; i++)
    {
        var x = new Random().Next(1, maze.GetLength(0) - 1);
        var y = new Random().Next(1, maze.GetLength(1) - 1);
        maze[x, y] = '$';
    }
}

void AddPlayer(char[,] maze)
{
    maze[1, 1] = '@';
}

void AddEmptySpace(char[,] maze)
{
    int width = maze.GetLength(0);
    int height = maze.GetLength(1);

    for (int x = 1; x < width - 1; x++)
    {
        for (int y = 1; y < height - 1; y++)
        {
            maze[x, y] = ' ';
        }
    }
}

void AddWalls(char[,] maze)
{
    int width = maze.GetLength(0);
    int height = maze.GetLength(1);

    for (int x = 0; x < width; x++)
    {
        maze[x, 0] = '#'; // Top wall
        maze[x, height - 1] = '#'; // Bottom wall
    }

    for (int y = 0; y < height; y++)
    {
        maze[0, y] = '#'; // Left wall
        maze[width - 1, y] = '#'; // Right wall
    }
}

void DrawWithColor(string s, ConsoleColor color, bool newLine = false)
{
    Console.ForegroundColor = color;
    if (newLine)
    {
        Console.WriteLine(s);
    }
    else
    {
        Console.Write(s);
    }
    Console.ResetColor();
}

void DrawMaze(char[,] maze)
{
    Console.Clear();
    for (int y = 0; y < maze.GetLength(1); y++)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            switch (maze[x, y])
            {
                case '@':
                    DrawWithColor(maze[x, y].ToString(), playerColor);
                    break;
                case '$':
                    DrawWithColor(maze[x, y].ToString(), tresureColor);
                    break;
                default:
                    Console.Write(maze[x, y]);
                    break;
            }
        }
        Console.WriteLine();
    }
}

Run();