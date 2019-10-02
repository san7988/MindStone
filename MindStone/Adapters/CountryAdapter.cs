using System;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MindStone.Adapters
{
    public class CountryAdapter : BaseAdapter, ISpinnerAdapter
    {
        public CountryAdapter()
        {
        }

        public override int Count => throw new NotImplementedException();

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return GetCustomView(position, convertView, parent, true);
        }
    }
}
