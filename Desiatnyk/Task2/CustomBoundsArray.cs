using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class CustomBoundsArray<T> : ICloneable, ICollection<T>, IEnumerable<T>
    {
        private T[] _data;
        private int _startIndex;
        private int _endIndex;
        public int StartIndex => _startIndex;
        public int EndIndex => _endIndex;
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }

        public CustomBoundsArray()
        {
            _data = new T[0];
            _startIndex = 0;
            _endIndex = 0;
            Count = 0;
        }
        public CustomBoundsArray(T[] source, int startIndex)
        {
            //same array
            _data = source ?? throw new ArgumentNullException("Parameter " + nameof(source) + " is null");
            _startIndex = startIndex;
            _endIndex = _startIndex + _data.Length;
            Count = source.Length;
        }
        public T this[int index]
        {
            get
            {
                if (index < _startIndex || index >= _endIndex)
                {
                    throw new IndexOutOfRangeException("Index is out of array bounds");
                }
                return _data[index - _startIndex];
            }
            set
            {
                if (index < _startIndex || index >= _endIndex)
                {
                    throw new IndexOutOfRangeException("Index is out of array bounds");
                }
                _data[index - _startIndex] = value;
            }
        }
        
        public void Add(T item)
        {
            T[] newData = new T[_data.Length + 1];
            for (int i = 0; i < _data.Length; i++)
                newData[i] = _data[i];
            newData[newData.Length - 1] = item;
            _data = newData;
            Count++;
            _endIndex++;
        }

        public void Clear()
        {
            _data = new T[0];
            _startIndex = 0;
            _endIndex = 0;
            Count = 0;
        }

        public object Clone()
        {
            return new CustomBoundsArray<T>(_data, _startIndex);
        }

        public bool Contains(T item)
        {
            return _data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new NullReferenceException(nameof(array) + " parameter is null.");
            }
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException(nameof(arrayIndex) + " parameter is out of" + nameof(array) + " parameter`s range of values.");
            }
            for (int i = arrayIndex, j = StartIndex; i < array.Length; i++, j++)
            {
                array[i] = this[j];
            }
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;
            T[] newData = new T[_data.Length - 1];
            for (int i = _startIndex, j = 0; i < _endIndex; i++, j++)
            {
                if (EqualityComparer<T>.Default.Equals(this[i], item))
                    j--;
                else
                    newData[j] = this[i];
            }
            _data = newData;
            _endIndex--;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            string resultString = string.Empty;
            foreach (var item in _data)
            {
                resultString += item.ToString() + string.Format(" ", 3);
            }
            return resultString;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }
    }
}
