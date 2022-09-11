using System.Runtime.CompilerServices;

namespace Recon.Generic
{
    [Serializable]
    public class mega<T>
    {
        public override string ToString()
        {
            return m_items.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj == this;
        }

        T[] m_items;
        private static readonly T[] s_emptyArray = new T[0];
        int element = 0;
        private int _size;
        private int _version;

        public mega(int capacity)
        {
            if (capacity < 0)
            {
                throw new Exception("lower than 0 bro");
            }
            if (capacity == 0)
            {
                this.m_items = mega<T>.s_emptyArray;
                return;
            }
            this.m_items = new T[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            this._version++;
            T[] items = this.m_items;
            int size = this._size;
            if (size < items.Length)
            {
                this._size = size + 1;
                items[size] = item;
                return;
            }
            this.AddWithResize(item);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddWithResize(T item)
        {
            int size = this._size;
            this.Grow(size + 1);
            this._size = size + 1;
            this.m_items[size] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            this._version++;
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                int size = this._size;
                this._size = 0;
                if (size > 0)
                {
                    Array.Clear(this.m_items, 0, size);
                    return;
                }
            }
            else
            {
                this._size = 0;
            }
        }

        public bool Contains(T item)
        {
            return this._size != 0 && this.IndexOf(item) != -1;
        }

        private void Grow(int capacity)
        {
            int num = (this.m_items.Length == 0) ? 4 : (2 * this.m_items.Length);
            if ((ulong)num > (ulong)((long)Array.MaxLength))
            {
                num = Array.MaxLength;
            }
            if (num < capacity)
            {
                num = capacity;
            }
            this.Capacity = num;
        }

        public int Capacity
        {
            get
            {
                return this.m_items.Length;
            }
            set
            {
                if (value < this._size)
                {
                    throw new ArgumentOutOfRangeException("hkfa");
                }
                if (value != this.m_items.Length)
                {
                    if (value > 0)
                    {
                        T[] array = new T[value];
                        if (this._size > 0)
                        {
                            Array.Copy(this.m_items, array, this._size);
                        }
                        this.m_items = array;
                        return;
                    }
                    this.m_items = mega<T>.s_emptyArray;
                }
            }
        }

        public int IndexOf(T item, int index)
        {
            if (index > this._size)
            {
                throw new IndexOutOfRangeException("hasidh");
            }
            return Array.IndexOf<T>(this.m_items, item, index, this._size - index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return Array.IndexOf<T>(this.m_items, item, index, count);
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf<T>(this.m_items, item, 0, this._size);
        }

        public T[] ToArray()
        {
            if (this._size == 0)
            {
                return mega<T>.s_emptyArray;
            }
            T[] array = new T[this._size];
            Array.Copy(this.m_items, array, this._size);
            return array;
        }
        public List<T> ToList()
        {
            List<T> values = new List<T>();
            if(this._size == 0)
            {
                return values;
            }
            foreach(var val in this.m_items)
            {
                values.Add(val);
            }
            return values;
        }

        public void RemoveAt(int index)
        {
            if (index >= this._size)
            {
                throw new ArgumentOutOfRangeException("bro what????DS?");
            }
            this._size--;
            if (index < this._size)
            {
                Array.Copy(this.m_items, index + 1, this.m_items, index, this._size - index);
            }
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                this.m_items[this._size] = default(T);
            }
            this._version++;
        }

        public int Length()
        {
            return _size;
        }

        public T at(int index)
        {
            return m_items[index];
        }
            

        public mega()
        {
            m_items = mega<T>.s_emptyArray;
        }
    }
}
