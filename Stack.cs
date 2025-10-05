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
    /// The Stack class.
    /// </summary>
    /// <typeparam name="T"> Generic type as data. </typeparam>
    public class Stack<T>
    {
        public Node<T> Head { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Initializes Size and Head.
        /// </summary>
        public Stack()
        {
            Clear();
        }

        /// <summary>
        /// Creates a new Node with the new element and adds it to the top of the stack.
        /// </summary>
        /// <param name="element"> Element value contained by the new node. </param>
        public void Push(T element)
        {
            Head = new Node<T>(element, Head);
            Size++;
        }

        /// <summary>
        /// Returns the top element on the stack without removing it from the data structure. 
        /// </summary>
        /// <returns> The top (head) element of the stack. </returns>
        public T Top()
        {
            ThrowExceptionIfListIsEmpty();
            return Head.Element;
        }

        /// <summary>
        /// Returns the top element on the stack, removing it from the data structure. 
        /// </summary>
        /// <returns> The top (head) element of the stack. </returns>
        public T Pop()
        {
            ThrowExceptionIfListIsEmpty();

            T element = Head.Element;
            Head = Head.Next;
            Size--;
            return element;
        }

        /// <summary>
        /// Ensures the Size and Head are reset to empty.
        /// </summary>
        public void Clear()
        {
            Head = null;
            Size = 0;
        }

        /// <summary>
        /// Return true if the list is empty, else returns false
        /// </summary>
        /// <returns> Current state of the stack - empty/not empty. </returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Checks if the list is empty.
        /// </summary>
        /// <exception cref="ApplicationException"> Throws an exception if the list is already empty when attempting to set the head. </exception>
        private void ThrowExceptionIfListIsEmpty()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("List is Empty");
            }
        }

    }
}