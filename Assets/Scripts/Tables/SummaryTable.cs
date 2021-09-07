using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
	[Serializable]
	public class SummaryTable
	{
		public string name;

		private Dictionary<int, SummaryTableItem> dics = null;
		private List<SummaryTableItem> items = new List<SummaryTableItem>();

		public List<SummaryTableItem> Items
		{
			get {
				return items;
			}
		}

		public void Initialize()
		{
			dics = new Dictionary<int, SummaryTableItem>();
			foreach (SummaryTableItem item in items)
			{
				dics.Add(item.id, item);
			}
		}

		public List<SummaryTableItem> GetItems()
		{
			return items;
		}

		public void AddItem(SummaryTableItem item)
		{
			items.Add(item);
		}

		public SummaryTableItem GetItemByKey(int key)
		{
			SummaryTableItem item = null;
			if (dics.ContainsKey(key))
			{
				dics.TryGetValue(key, out item);
			}
			return item;
		}
	}

	[Serializable]
	public class SummaryTableItem
	{
    	public int id;
    	public string type;
    	public int min;
    	public string judge;
    	public int grade;
	}
}