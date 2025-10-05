/*
 * Name: Dharmi Patel
 * Program: Business Information Technology
 * Course: ADEV-3009 Data Structures and Algorithms
 * Assignment 2
 * Created: 2025-02-14 
 * Updated: 2025-02-21 (documentation)
 */

using System.Diagnostics;
using System.Text;

namespace TestLibrary
{
    /// <summary>
    /// The Maze class.
    /// Will be used to keep track of the maze details
    /// </summary>
    public class Maze
    {
        private char[][] charMaze { get; }
        public Point StartingPoint { get; private set; }
        public int RowLength => charMaze.Length;
        public int ColumnLength => charMaze[0].Length;
        private Stack<Point> path;
        private bool dfsExecuted = false;

        /// <summary>
        /// reads a specified file while populating maze details.
        /// </summary>
        /// <param name="fileName"> file which contains the maze. </param>
        public Maze(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int rows = int.Parse(lines[0].Split(' ')[0]);
            int cols = int.Parse(lines[0].Split(' ')[1]);
            charMaze = new char[rows][];

            //read the starting point coordinates
            string[] startCoord = lines[1].Split(' ');
            StartingPoint = new Point(int.Parse(startCoord[0]), int.Parse(startCoord[1]));

            //populate the maze grid
            for (int i = 0; i < rows; i++)
            {
                charMaze[i] = lines[i + 2].ToCharArray();
            }

            path = new Stack<Point>();
        }

        /*
        public void ReadMaze(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int rows = int.Parse(lines[0].Split(' ')[0]);
            int cols = int.Parse(lines[0].Split(' ')[1]);
            charMaze = new char[rows][];

            string[] startCoord = lines[1].Split(' ');
            StartingPoint = new Point(int.Parse(startCoord[0]), int.Parse(startCoord[1]));

            for (int i = 0; i < rows; i++)
            {
                charMaze[i] = lines[i+2].ToCharArray();
            }
        }
        */

        /// <summary>
        /// creates the starting point and assigns the passed existingMaze to the internal charMaze property.
        /// </summary>
        /// <param name="startingRow"> Starting row position. </param>
        /// <param name="startingColumn"> Starting column position. </param>
        /// <param name="existingMaze"> Maze in 2D char array. </param>
        public Maze(int startingRow, int startingColumn, char[][] existingMaze)
        {
            ThrowExceptionIfInvalidMaze(startingRow, startingColumn, existingMaze);
            
            StartingPoint = new Point(startingRow, startingColumn);
            charMaze = existingMaze;
            path = new Stack<Point>();
        }

        /// <summary>
        /// Retrieves the maze as a 2D character array.
        /// </summary>
        /// <returns> Returns the private charMaze array. </returns>
        public char[][] GetMaze() => charMaze;

        /// <summary>
        /// Print the charMaze.
        /// </summary>
        /// <returns> returns the entire maze as a formatted string. </returns>
        public string PrintMaze()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char[] row in charMaze) 
            {
                sb.Append(new string(row) + "\n");
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Perform a depth-first search (DFS) to find the exit in the maze.
        /// </summary>
        /// <returns> A formatted string representation of the path if found, otherwise an error message. </returns>
        public string DepthFirstSearch()
        {
            path.Push(StartingPoint);
            charMaze[StartingPoint.Row][StartingPoint.Column] = 'V';
            dfsExecuted = true;

            // If no path to exit is found, return failure message.
            if (!Search())
                return "No exit found in maze!\n\n" + PrintMaze();

            Stack<Point> pathToFollow = GetPathToFollow();
            while (!pathToFollow.IsEmpty())
            {
                // Convert only valid path to '.'
                Point p = pathToFollow.Pop();
                if (charMaze[p.Row][p.Column] == 'V') 
                {
                    charMaze[p.Row][p.Column] = '.';
                }
            }

            return FormatPath();
        }

        /// <summary>
        /// Searches for the exit in the maze.
        /// </summary>
        /// <returns> true if the path is found, false if not. </returns>
        private bool Search()
        {
            while (!path.IsEmpty())
            {
                Point current = path.Top();

                // exit found.
                if (charMaze[current.Row][current.Column] == 'E')
                    return true;

                //mark current cell as visited
                charMaze[current.Row][current.Column] = 'V';
                
                //try moving in all four directions (S.E.W.N)
                if(TryMove(current.Row+1, current.Column) ||
                   TryMove(current.Row, current.Column+1) ||
                   TryMove(current.Row, current.Column-1) ||
                   TryMove(current.Row-1, current.Column))
                {
                    continue;
                }
                //backtrack if run into wall.
                path.Pop();
            }

            return false;
        }

        /// <summary>
        /// Attempts to move to a given position in the maze, push it onto the stack if valid.
        /// </summary>
        /// <param name="row"> row coordinate. </param>
        /// <param name="col"> column coordinate. </param>
        /// <returns> True is move is  valid, false otherwise. </returns>
        private bool TryMove(int row, int col)
        {
            //check validity - not a wall and within bounds.
            if (row >= 0 && row < RowLength && col >= 0 && col < ColumnLength &&
                (charMaze[row][col] == ' ' || charMaze[row][col] == 'E'))
            {
                path.Push(new Point(row, col));
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves a stack containing the path to follow from start to exit.
        /// </summary>
        /// <returns> stack with starting point (top) to exit. </returns>
        /// <exception cref="ApplicationException"> Thrown if the Depth First Search is not run. </exception>
        public Stack<Point> GetPathToFollow()
        {
            //Ensure DFS was run before getting the path
            if (!dfsExecuted) 
                throw new ApplicationException("Depth-first search has not been performed yet.");

            //temp stack to restore original path 
            Stack<Point> tempStack = new Stack<Point>();
            Stack<Point> reversedPath = new Stack<Point>();

            //pop all elements from the path stack and push them into tempStack and reversedPath.
            while (!path.IsEmpty())
            {
                Point point = path.Pop();
                reversedPath.Push(point); //correct path order to return
                tempStack.Push(point); //temp storage to restore the original path later
            }
            
            //restore original path so it can be used again later.
            while (!tempStack.IsEmpty())
            {
                path.Push(tempStack.Pop());
            }
            
            return reversedPath;
        }

        /// <summary>
        /// Formats the path from start to exit as a string.
        /// </summary>
        /// <returns> the formatted path in string form. </returns>
        private string FormatPath()
        {
            StringBuilder sb = new StringBuilder();
            Stack<Point> pathToFollow = GetPathToFollow(); // Get the path in correct order
            
            //append and build the string
            sb.Append($"Path to follow from Start {StartingPoint} to Exit {path.Top()} - {path.Size} steps:\n");
            
            while (!pathToFollow.IsEmpty())
            {
                sb.Append(pathToFollow.Pop().ToString() + "\n");
            }

            sb.Append(PrintMaze());
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Validates the maze bounds and starting position in the maze.
        /// </summary>
        /// <param name="row"> row value. </param>
        /// <param name="column"> column value. </param>
        /// <param name="maze"> char maze from the file. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown if the row and column values in the maze are beyond bounds. </exception>
        /// <exception cref="ApplicationException"> thrown if the start position is invalid. </exception>
        private void ThrowExceptionIfInvalidMaze(int row, int column, char[][] maze)
        {
            if (row < 0 || column < 0 || row >= maze.Length || column >= maze[0].Length)
                throw new IndexOutOfRangeException("Row and Column values must be within maze bounds.");
            if (maze[row][column] == 'W')
                throw new ApplicationException("Starting position cannot be inside a wall.");
            if (maze[row][column] == 'E')  
                throw new ApplicationException("Starting Position cannot be the Exit point.");
        }

    }
}




