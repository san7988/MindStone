using System;
using Android.Widget;
using Java.Lang;

namespace MindStone.Adapters
{
    public class TestAdapter : ArrayAdapter
    {
        public override bool IsEnabled(int position)
        {
            if (position == 0) return false;
            return true;
        }

        public TestAdapter()
        {

        }
    }
}
