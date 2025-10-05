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
    /// The Node class.
    /// </summary>
    /// <typeparam name="T"> Generic value. </typeparam>
    public class Node<T>
    {
        public T Element { get; }
        public Node<T> Next { get; set; }

        /// <summary>
        /// Initializes a Node with an element, and next reference pointer.
        /// </summary>
        /// <param name="element"> Value element for each node. </param>
        /// <param name="next"> pointer that references next node. </param>
        public Node(T element, Node<T> next = null)
        {
            Element = element;
            Next = next;
        }
    }
}