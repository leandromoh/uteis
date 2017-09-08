    using System.Collections.Generic;

    /// <summary>
    /// Collection that removes each item after yield it
    /// </summary>
    sealed class OnceEnumerationCollection<T> : List<T>, IEnumerable<T>
    {
        public OnceEnumerationCollection(IEnumerable<T> source) : base(source) { }

        public new IEnumerator<T> GetEnumerator()
        {
            for(int i = 0, leng = Count; i < leng; i++)
            {
                var temp = this[0];
                this.RemoveAt(0);
                yield return temp;
            }
        }
    }
