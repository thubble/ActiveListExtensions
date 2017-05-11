﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveListExtensions.Utilities
{
	internal class QuickList<T> : IReadOnlyList<T>
	{
		private static readonly T[] _emptyArray = new T[0];

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= _count)
					throw new ArgumentOutOfRangeException(nameof(index));
				return _items[index];
			}
		}

		private int _count;
		public int Count => _count;

		private int _version;
		public int Version => _version;

		private T[] _items = _emptyArray;

		public void Add(int index, T value)
		{
			AddCapacityIfNeeded();

			if (index < _count)
				Array.Copy(_items, index, _items, index + 1, _count - index);

			_items[index] = value;
			++_count;
			++_version;
		}

		public void Remove(int index)
		{
			if (index < _count)
				Array.Copy(_items, index + 1, _items, index, _count - index - 1);
			else
				_items[index] = default(T);

			--_count;
			++_version;
		}

		public void Replace(int index, T value)
		{
			_items[index] = value;

			++_version;
		}

		public void Move(int oldIndex, int newIndex)
		{
			if (oldIndex == newIndex)
				return;

			var value = _items[oldIndex];
			if (oldIndex < newIndex)
			{
				Array.Copy(_items, oldIndex + 1, _items, oldIndex, newIndex - oldIndex);
				_items[newIndex] = value;
			}
			else
			{
				Array.Copy(_items, newIndex, _items, newIndex + 1, oldIndex - newIndex);
				_items[newIndex] = value;
			}
			++_version;
		}

		public void Clear()
		{
			if (_count > 0)
			{
				Array.Clear(_items, 0, _count);
				_count = 0;
			}
			++_version;
		}

		private void AddCapacityIfNeeded()
		{
			if (_count == _items.Length)
			{
				if (_items.Length == 0)
					_items = new T[16];
				else
				{
					T[] newItems = new T[_items.Length * 2];
					Array.Copy(_items, 0, newItems, 0, _items.Length);
					_items = newItems;
				}
			}
		}

		public IEnumerator<T> GetEnumerator() => new QuickListEnumerator<T>(this);

		IEnumerator IEnumerable.GetEnumerator() => new QuickListEnumerator<T>(this);
	}
}
