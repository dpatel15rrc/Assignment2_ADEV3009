/*
 * Name: Dharmi Patel
 * Program: Business Information Technology
 * Course: ADEV-3009 Data Structures and Algorithms
 * Assignment 2
 * Created: 2025-02-13
 * Updated: 
 */

namespace TestLibrary
{
    /// <summary>
    /// The Point class.
    /// Describes a point in the maze (numeric row and column coordinates).
    /// </summary>
    public class Point
    {
        public int Row { get; }
        public int Column { get; }

        /// <summary>
        /// Initializes row and column using parameter values.
        /// </summary>
        /// <param name="row"> The Row value of the point. </param>
        /// <param name="column"> The Column value of the point. </param>
        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Prints the coordinates of a point in the maze.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{Row}, {Column}]";
    }

}