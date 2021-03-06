﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrixiaTrie {
    class SimplePriorityQueue<P, V> {
        private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();
        private int size = 0;
        public void Enqueue(P priority, V value) {
            Queue<V> q;
            if (!list.TryGetValue(priority, out q)) {
                q = new Queue<V>();
                list.Add(priority, q);
            }
            q.Enqueue(value);
            size++;
        }
        public V Dequeue() {
            // will throw if there isn't any first element!
            var pair = list.First();
            var v = pair.Value.Dequeue();
            if (pair.Value.Count == 0) // nothing left of the top priority.
                list.Remove(pair.Key);
            size--;
            return v;
        }
        public bool IsEmpty {
            get { return !list.Any(); }
        }

        public int Count {
            get { return size; }
        }

        public bool HasPriorityOf(P i) {
            if (list.ContainsKey(i))
                return true;

            return false;
        }
    }
}
