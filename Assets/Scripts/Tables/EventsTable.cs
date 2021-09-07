using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
	[Serializable]
	public class EventsTable
	{
		public string name;

		private Dictionary<int, EventsTableItem> dics = null;
		private List<EventsTableItem> items = new List<EventsTableItem>();

		public List<EventsTableItem> Items
		{
			get {
				return items;
			}
		}

		public void Initialize()
		{
			dics = new Dictionary<int, EventsTableItem>();
			foreach (EventsTableItem item in items)
			{
				dics.Add(item.id, item);
			}
		}

		public List<EventsTableItem> GetItems()
		{
			return items;
		}

		public void AddItem(EventsTableItem item)
		{
			items.Add(item);
		}

		public EventsTableItem GetItemByKey(int key)
		{
			EventsTableItem item = null;
			if (dics.ContainsKey(key))
			{
				dics.TryGetValue(key, out item);
			}
			return item;
		}
	}

	[Serializable]
	public class EventsTableItem
	{
    	public int id;
    	public string Event;
    	public string postEvent;
    	public int CHR;
    	public int INT;
    	public int STR;
    	public int MNY;
    	public int SPR;
    	public int LIF;
    	public int NoRandom;
    	public string include;
    	public string exclude;
    	public string[] branchs;
	}
}