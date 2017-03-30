﻿using ActiveListExtensions.ValueModifiers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveListExtensions.ValueModifiers
{
	internal class ActiveCount<TSource> : ActiveListPredicateBase<TSource, int>
	{
		public ActiveCount(IActiveList<TSource> source, Func<TSource, bool> predicate, IEnumerable<string> propertiesToWatch = null)
			: base(source, predicate, propertiesToWatch)
		{
			Initialize();
		}

		protected override int GetValue(int count) => count;
	}
}