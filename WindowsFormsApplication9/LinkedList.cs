using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    
        public class LinkedList<T>

        

        {
        
            Node head;
            private int length;

            private class Node
            {

                public Node next;
                public T value;

                public Node(T value)
                {
                    this.value = value;
                }


            }
                        

            public void Add(T value)
            {
                Node node = new Node(value);
                node.next = head;
                head = node;
                length++;

            }
            public void Remove(int index)
            {
                Node prev = head, current;
                
                if (head != null)
                {
                    if (index == 0)
                    {
                        head = head.next;
                    }
                    else
                    {
                        for (int i = 0; i < index - 1; i++)
                        {
                            prev = prev.next;
                        }
                        current = prev.next;
                        prev.next = current.next;
                    }

                }



            }
            public int GetValue(int value) //change to generic
            {
                int position = 1; //counter starts from head.next

                Node current = head.next;

                if (value.CompareTo(head.value) == 0)
                {
                    return 0;
                }

                while (value.CompareTo(current.value) != 0)
                {

                    current = current.next;
                    position++;
                }

                return position;
            }
        public int getLength()
        {
            Node temp = head;

            while (temp != null)
            {
                
                temp = temp.next;
                length++;
            }

            return length;
        }


            public T Get(int index)
            {

            
                Node current = head.next;

                if (index == 0)
                {
                    return head.value;
                }

                for (int i = 0; i < index - 1; i++)
                {
                    if (current.next == null)
                    {
                        return default(T);
                    }
                    else
                        current = current.next;
                }

                return current.value;
            
            }

            public void Print()
            {
                Node temp = head;

                while (temp != null)
                {
                    Console.WriteLine(temp.value);

                    temp = temp.next;
                }
            }
            public void Sort() { }
        }
    }
