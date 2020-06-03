using Dna;
using System;
using Android.OS;
using Android.App;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Dailylight.Relational;
using System.Threading.Tasks;
using static Dailylight.Droid.DI;
using Java.IO;
using System.IO;
using Dailylight.Core;

namespace Dailylight.Droid
{
    [Activity(Label = "Daily Light", 
        Theme = "@style/AppTheme.NoActionBar", 
        MainLauncher = true, 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set the content view
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            await ApplicationSetupAsync();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();

            // Get the login credentials
            await GetLoginCredentialsAsync();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Set database file name
            string fileName = "Dailylight.db";

            // Set database path
            string dbPath = Path.Combine(System
                .Environment
                .GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);

            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
            .AddClientDataStore(dbPath)
            .Build();

            // Ensure the client data store
            await ClientDataStore.EnsureDataStoreAsync();

            // Set the first page
            SetContentView(Resource.Layout.user_setup);

            // TODO: Monitor for server connection status
        }


        /// <summary>
        /// Save the users login credentials to data store
        /// </summary>
        /// <returns></returns>
        private async Task SaveLoginCredentials()
        {
            var loginCredentials = new LoginCredentialsDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Email = "peter.ayobami@outlook.com",
                FirstName = "Peter",
                LastName = "Ayobami",
                Username = "Petrus",
                Token = Guid.NewGuid().ToString()
            };

            await ClientDataStore.SaveLoginCredentialsAsync(loginCredentials);
        }


        /// <summary>
        /// Return the first row of the users login credentials
        /// </summary>
        /// <returns></returns>
        private async Task GetLoginCredentialsAsync()
        {
            var result = await ClientDataStore.GetLoginCredentialsAsync();
        }

        private void CopyDatabaseFile(string dbPath)
        {
            var destPath = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "database.db");

            // Destination
            var destFile = new Java.IO.File(destPath);
            var canWrite = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).CreateNewFile();
            var canRead = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).CanRead();
            if (!destFile.Exists())
            {
                try
                {
                    var result = destFile.CanWrite();
                    destFile.CreateNewFile();
                }
                catch (Java.IO.IOException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error Message: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Error Source: {ex.Source}");
                }
            }
        }
    }
}

