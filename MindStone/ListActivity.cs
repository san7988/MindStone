
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MindStone
{
    [Activity(Label = "ListActivity", MainLauncher =true)]
    public class MyListActivity : ListActivity
    {
        static readonly string[] countries = new String[] {
    "Afghanistan","Albania","Algeria","American Samoa","Andorra",
    "Angola","Anguilla","Antarctica","Antigua and Barbuda","Argentina",
    "Armenia","Aruba","Australia","Austria" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, countries);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };
        }
    }
}
