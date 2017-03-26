﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveListExtensions.Tests.Helpers;
using Xunit;

namespace ActiveListExtensions.Tests.Modifiers
{
	public class ActiveIntersectTests
	{
		[Fact]
		public void RandomlyInsertItems() => CollectionTestHelpers.RandomlyInsertItemsIntoTwoCollections((l1, l2) => l1.ActiveIntersect(l2), (l1, l2) => l1.Intersect(l2), () => RandomGenerator.GenerateRandomInteger(0, 10), () => RandomGenerator.GenerateRandomInteger(0, 10), true);

		[Fact]
		public void RandomlyRemoveItems() => CollectionTestHelpers.RandomlyRemoveItemsFromTwoCollections((l1, l2) => l1.ActiveIntersect(l2), (l1, l2) => l1.Intersect(l2), () => RandomGenerator.GenerateRandomInteger(0, 10), () => RandomGenerator.GenerateRandomInteger(0, 10), true);

		[Fact]
		public void RandomlyReplaceItems() => CollectionTestHelpers.RandomlyReplaceItemsInTwoCollections((l1, l2) => l1.ActiveIntersect(l2), (l1, l2) => l1.Intersect(l2), () => RandomGenerator.GenerateRandomInteger(0, 10), () => RandomGenerator.GenerateRandomInteger(0, 10), true);

		[Fact]
		public void RandomlyMoveItems() => CollectionTestHelpers.RandomlyMoveItemsWithinTwoCollections((l1, l2) => l1.ActiveIntersect(l2), (l1, l2) => l1.Intersect(l2), () => RandomGenerator.GenerateRandomInteger(0, 10), () => RandomGenerator.GenerateRandomInteger(0, 10), true);

		[Fact]
		public void ResetWithRandomItems() => CollectionTestHelpers.ResetTwoCollectionsWithRandomItems((l1, l2) => l1.ActiveIntersect(l2), (l1, l2) => l1.Intersect(l2), () => RandomGenerator.GenerateRandomInteger(0, 10), () => RandomGenerator.GenerateRandomInteger(0, 10), true);

		[Fact]
		public void RandomlyChangePropertyValues() => CollectionTestHelpers.RandomlyChangePropertyValuesInTwoCollections((l1, l2) => l1.ActiveIntersect(l2, o => o.Property), (l1, l2) => l1.Intersect(l2, new KeyEqualityComparer<ActiveIntersectTestClass>(o => o.Property, null)), () => new ActiveIntersectTestClass() { Property = RandomGenerator.GenerateRandomInteger() }, () => new ActiveIntersectTestClass() { Property = RandomGenerator.GenerateRandomInteger() }, o => o.Property = RandomGenerator.GenerateRandomInteger(), o => o.Property = RandomGenerator.GenerateRandomInteger(), true, o => o.Property);
	}

	public class ActiveIntersectTestClass : INotifyPropertyChanged
	{
		private int _property;
		public int Property
		{
			get { return _property; }
			set
			{
				if (_property == value)
					return;
				_property = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Property)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}