using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
	[Serializable]
	public class AgeTable
	{
		public string name;

		private Dictionary<int, AgeTableItem> dics = null;
		private List<AgeTableItem> items = new List<AgeTableItem>();

		public List<AgeTableItem> Items
		{
			get {
				return items;
			}
		}

		public void Initialize()
		{
			dics = new Dictionary<int, AgeTableItem>();
			foreach (AgeTableItem item in items)
			{
				dics.Add(item.id, item);
			}
		}

		public List<AgeTableItem> GetItems()
		{
			return items;
		}

		public void AddItem(AgeTableItem item)
		{
			items.Add(item);
		}

		public AgeTableItem GetItemByKey(int key)
		{
			AgeTableItem item = null;
			if (dics.ContainsKey(key))
			{
				dics.TryGetValue(key, out item);
			}
			return item;
		}
	}

	[Serializable]
	public class AgeTableItem
	{
    	public int id;
    	public string[] Events;
	}
}