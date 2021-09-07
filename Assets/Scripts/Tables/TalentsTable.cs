using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
	[Serializable]
	public class TalentsTable
	{
		public string name;

		private Dictionary<int, TalentsTableItem> dics = null;
		private List<TalentsTableItem> items = new List<TalentsTableItem>();

		public List<TalentsTableItem> Items
		{
			get {
				return items;
			}
		}

		public void Initialize()
		{
			dics = new Dictionary<int, TalentsTableItem>();
			foreach (TalentsTableItem item in items)
			{
				dics.Add(item.id, item);
			}
		}

		public List<TalentsTableItem> GetItems()
		{
			return items;
		}

		public void AddItem(TalentsTableItem item)
		{
			items.Add(item);
		}

		public TalentsTableItem GetItemByKey(int key)
		{
			TalentsTableItem item = null;
			if (dics.ContainsKey(key))
			{
				dics.TryGetValue(key, out item);
			}
			return item;
		}
	}

	[Serializable]
	public class TalentsTableItem
	{
    	public int id;
    	public string name;
    	public string description;
    	public string condition;
    	public int grade;
    	public int status;
    	public int SPR;
    	public int MNY;
    	public int CHR;
    	public int STR;
    	public int INT;
    	public int[] exclusives;
	}
}