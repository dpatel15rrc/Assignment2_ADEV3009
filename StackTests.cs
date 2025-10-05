using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TestLibrary
{
    /// <summary>
    /// StackTest - A class for testing the Stack class
    /// Stack - A class that is linked list of Nodes.
    ///         Contains the methods to treat the linked list as a Stack
    /// Assignment:     #2
    /// Course:         ADEV-3001
    /// </summary>
    [TestFixture]
    public class StackTests
    {
        #region Constructor Tests
        /// <summary>
        /// Test the constructor to ensure the default values are set properly.
        /// </summary>
        [Test]
        public void Constructor_head_is_null_Test()
        {
            Stack<Point> stack = new Stack<Point>();
            ClassicAssert.That(stack.Head, Is.Null);
        }
        #endregion

        #region Public Methods Test
        #region GetSize()
        /// <summary>
        /// Test GetSize() to ensure it returns zero on empty stack.
        /// </summary>
        [Test]
        public void GetSizeOnEmptyStackTest()
        {
            Stack<Point> stack = new Stack<Point>();
            ClassicAssert.That(stack.Size, Is.EqualTo(0));
        }
        #endregion

        #region Push()
        /// <summary>
        /// Test Push() to ensure node is added to stack and as the head
        /// </summary>
        [Test]
        public void Push_increases_size_by_1_Test()
        {
            Point newPoint = new Point(1, 2);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(newPoint);
            ClassicAssert.That(stack.Size, Is.EqualTo(1));
        }

        /// <summary>
        /// Test Push() to ensure node is added to head of stack.
        /// </summary>
        [Test]
        public void Push_Inserts_To_Head_Test()
        {
            Point point = new Point(1, 2);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point);
            ClassicAssert.That(stack.Head.Element.Equals(point));
            ClassicAssert.That(stack.Head.Next, Is.Null);
        }

        /// <summary>
        /// Test Push() to ensure node is added to head of stack.
        /// </summary>
        [Test]
        public void Push_Inserts_To_Head_when_list_is_larger_Test()
        {
            Stack<Point> stack = CreateListWithoutMethods(3, out _);
            Node<Point> oldHead = stack.Head;
            Point point = new Point(3, 3);
            stack.Push(point);

            ClassicAssert.That(stack.Head.Element.Equals(point));
            ClassicAssert.That(oldHead.Equals(stack.Head.Next));
            ClassicAssert.That(stack.Size, Is.EqualTo(4));
        }
        #endregion

        #region IsEmpty()
        /// <summary>
        /// Test IsEmpty() returns true on empty stack.
        /// </summary>
        [Test]
        public void IsEmptyOnEmptyStackTest()
        {
            Stack<Point> stack = new Stack<Point>();

            ClassicAssert.That(stack.IsEmpty(), Is.True);
        }

        /// <summary>
        /// Test IsEmpty() returns false on a stack with elements.
        /// </summary>
        [Test]
        public void IsEmptyOnStackWithElements()
        {
            Point point01 = new Point(1, 2);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point01);

            ClassicAssert.That(stack.IsEmpty(), Is.False);
        }
        #endregion

        #region Top()
        /// <summary>
        /// Test Top() throws an exception when called on an empty stack.
        /// </summary>
        [Test]
        public void Top_Throws_Exception_On_EmptyStack_Test()
        {
            Stack<Point> stack = new Stack<Point>();
            ClassicAssert.That(() => stack.Top(), Throws.Exception.TypeOf<ApplicationException>());
        }
        /// <summary>
        /// Test Top() to ensure it returns the top node.
        /// </summary>
        [Test]
        public void Top_returns_head_in_list_of_1_Test()
        {
            Point point01 = new Point(3, 5);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point01);

            Point returnedPoint = stack.Top();
            Point headPoint = stack.Head.Element;

            ClassicAssert.That(returnedPoint.Equals(point01));
            ClassicAssert.That(headPoint.Equals(returnedPoint));
        }

        /// <summary>
        /// Test Top() to ensure it returns the top node.
        /// </summary>
        [Test]
        public void Top_returns_head_in_larger_list_Test()
        {
            Point point01 = new Point(3, 5);
            Point point02 = new Point(3, 5);
            Point point03 = new Point(3, 5);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point03);
            stack.Push(point02);
            stack.Push(point01);

            Point returnedPoint = stack.Top();
            Point headPoint = stack.Head.Element;
            Point secondPoint = stack.Head.Next.Element;
            Point thirdPoint = stack.Head.Next.Next.Element;
            Node<Point> lastNode = stack.Head.Next.Next;

            ClassicAssert.That(returnedPoint, Is.EqualTo(point01));
            ClassicAssert.That(headPoint, Is.EqualTo(returnedPoint));
            ClassicAssert.That(secondPoint, Is.EqualTo(point02));
            ClassicAssert.That(thirdPoint, Is.EqualTo(point03));

            // check that the last node still points to null!
            ClassicAssert.That(lastNode.Next, Is.Null);
        }

        /// <summary>
        /// Test Top() to make sure it only returns the element and does not remove the element.
        /// </summary>
        [Test]
        public void Top_Does_Not_Remove_an_Element_Test()
        {
            Point newPoint = new Point(3, 5);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(newPoint);

            Point returnedPoint = stack.Top();

            ClassicAssert.That(stack.Size, Is.EqualTo(1));
        }

        #endregion

        #region Pop()
        /// <summary>
        /// Test Pop() to ensure it throws and exception when called on an empty stack.
        /// </summary>
        [Test]
        public void Pop_Throws_Exception_On_EmptyStack_Test()
        {
            Stack<Point> stack = new Stack<Point>();

            ClassicAssert.That(() => stack.Pop(), Throws.Exception.TypeOf<ApplicationException>());
        }

        /// <summary>
        /// Test Pop() to ensure it reduces the size by 1
        /// </summary>
        [Test]
        public void Pop_decreases_size_by_1_Test()
        {
            Point point01 = new Point(3, 5);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point01);

            Point returnedPoint = stack.Pop();

            ClassicAssert.That(stack.Size, Is.EqualTo(0));
        }

        /// <summary>
        /// Test Pop() to ensure it returns the top element.
        /// </summary>
        [Test]
        public void Pop_returns_top_element_in_list_of_1_Test()
        {
            Point point01 = new Point(3, 5);
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point01);

            Node<Point> oldHead = stack.Head;
            Point oldHeadPoint = oldHead.Element;
            Point returnedPoint = stack.Pop();
            Node<Point> newHead = stack.Head;

            ClassicAssert.That(oldHeadPoint, Is.EqualTo(returnedPoint));
            ClassicAssert.That(returnedPoint, Is.EqualTo(point01));

            // list of 1 after a remove is an empty list
            ClassicAssert.That(stack.IsEmpty());
        }

        /// <summary>
        /// Test Pop() to ensure it returns the top element, in a bigger list.
        /// </summary>
        [Test]
        public void Pop_returns_top_element_in_larger_list_Test()
        {
            Point point01 = new Point(3, 5);
            Point point02 = new Point(2, 4);
            Point point03 = new Point(1, 3);

            Stack<Point> stack = new Stack<Point>();
            stack.Push(point03);
            stack.Push(point02);
            stack.Push(point01);

            Node<Point> oldHead = stack.Head;
            Point oldHeadPoint = oldHead.Element;
            Point returnedPoint = stack.Pop();
            Node<Point> newHead = stack.Head;
            Node<Point> lastNode = newHead.Next;

            ClassicAssert.That(oldHeadPoint, Is.EqualTo(returnedPoint));
            ClassicAssert.That(returnedPoint, Is.EqualTo(point01));
            ClassicAssert.That(newHead.Element, Is.EqualTo(point02));
            ClassicAssert.That(lastNode.Element, Is.EqualTo(point03));
            ClassicAssert.That(lastNode.Next, Is.Null);

            ClassicAssert.That(stack.Size, Is.EqualTo(2));
        }
        #endregion

        #region Clear()
        /// <summary>
        /// Test Clear() to ensure it returns size of zero and null head.
        /// </summary>
        [Test]
        public void Clear_on_populated_stack_sets_size_to_0_head_becomes_null_Test()
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(3, 5));
            stack.Push(new Point(2, 4));
            stack.Clear();
            ClassicAssert.That(stack.Head, Is.Null);
            ClassicAssert.That(stack.IsEmpty());
        }
        #endregion
        #endregion

        #region Helpers
        /* HELPER METHODS */
        /// <summary>
        /// Ensures:
        /// Head points Next to null
        /// The head is in the first spot
        /// The tail is in the last spot
        /// Each inner node (not head/tail) is mutually pointing at it's neighbours on next.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool CheckIntegrityBetweenAllNodes(Stack<Point> stack)
        {
            bool isValid = true;
            Node<Point>[] array = ConvertToNodeArray(stack);
            isValid &= array[0] == stack.Head;
            isValid &= stack.Size == array.Length;
            for (int i = 0; i <= array.Length - 2; i++)
            {
                Node<Point> before = array[i];
                Node<Point> after = array[i + 1];
                isValid &= before.Next == after;
            }
            isValid &= array[array.Length - 1].Next == null; // the bottom of the stack points Next to null
            return isValid;
        }


        /// <summary>
        /// For testing purposes, make an array out of the LinkedList
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Node<Point>[] ConvertToNodeArray(Stack<Point> stack)
        {
            Node<Point>[] array = new Node<Point>[stack.Size];
            int index = 0;
            for (Node<Point> node = stack.Head; node != null; node = node.Next)
            {
                array[index] = node;
                index++;
            }

            if (index != stack.Size) throw new Exception("Your stack is broken. The number of nodes does not equal the size of your Stack");

            return array;
        }

        private Stack<Point> CreateListWithoutMethods(int size, out Node<Point>[] nodeArray)
        {
            Node<Point> beforeNode = null, newNode = null;
            Stack<Point> stack = new Stack<Point>();
            nodeArray = new Node<Point>[size];

            for (int i = 1, index = 0; i <= size; i++, index++)
            {
                newNode = new Node<Point>(new Point(i,i));
                newNode.Next = beforeNode;
                nodeArray[index] = newNode;
                stack.Head = newNode;
                stack.Size++;
                beforeNode = newNode;
            }
            return stack;
        }
        #endregion helpers
    }
}
