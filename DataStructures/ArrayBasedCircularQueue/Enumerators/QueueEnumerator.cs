using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.ArrayBasedCircularQueue.Enumerators
{
    /// <summary>
    /// Queue Enumerator to enumerate queue.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class QueueEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] queue;
        private bool disposedValue = false;
        private int front = 0;
        private int rear = 0;
        private int position;
        private Func<int, int> incrementer;
        private Func<int, int, bool> condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEnumerator{T}"/> class.
        /// </summary>
        public QueueEnumerator(T[] queue, int front, int rear, Func<int, int> incrementer, Func<int, int, bool> condition)
        {
            this.queue = queue;
            this.front = front;
            this.rear = rear;
            this.position = this.front - 1;
            this.incrementer = incrementer;
            this.condition = condition;
        }

        /// <summary>
        /// Gets current element from the queue.
        /// </summary>
        public T Current
        {
            get
            {
                return this.queue[this.position];
            }
        }

        /// <summary>
        /// Gets current element from the queue.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Advances the pointer to next element position.
        /// </summary>
        public bool MoveNext()
        {
            this.position = this.incrementer(this.position);
            return this.condition(this.position, this.rear);
        }

        /// <summary>
        /// Resets enumerator position.
        /// </summary>
        public void Reset()
        {
            this.position = this.front - 1;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~QueueEnumerator()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }
    }
}
