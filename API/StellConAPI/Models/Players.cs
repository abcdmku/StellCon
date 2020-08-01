using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StellConAPI.Models
{
    [Serializable]
    public class Players : System.Collections.IEnumerable
    {
        private Player[] _people;
        public Players(Player[] pArray)
        {
            _people = new Player[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PlayerEnum GetEnumerator()
        {
            return new PlayerEnum(_people);
        }
    }

    public class PlayerEnum : IEnumerator
    {
        public Player[] _people;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public PlayerEnum(Player[] list)
        {
            _people = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Player Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}