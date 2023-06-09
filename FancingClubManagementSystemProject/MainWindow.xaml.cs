﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FancingClubManagementSystemProject.View;
using Npgsql;

namespace FancingClubManagementSystemProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
       * Database Connection & Operation
       * 
       * The working steps of the Database Connection are:
       * 
       * 1. Database connection with the Connection String (Need to Form)
       * 2. Establish the DB Connection
       * -- Open the Connection
       * 3. Generate the SQL Command for performing SQL Operations
       * 4. Execute the Command
       * 5. Close the Connection to Save the Results
       * 
       */

        //Step> 1. Database Connection Creation

        private static string getConnectionString()
        {
            // in this method, we are going to create a connection string for the PostGreSQL server..
            string host = "Host=localhost;";
            string port = "Port=5432;";
            string dbName = "Database=FencingClubManagementSystem;";
            string userName = "Username=postgres;";
            string password = "Password=1111;";
            // Now we need to add all these values/strings to one string, means we are going to merge
            // the strings together and going to create our string
            string connectionString = string.Format("{0}{1}{2}{3}{4}", host, port, dbName, userName, password);
            return connectionString;
        }

        //Step> 2. Establishing DB Connection
        /*
         * to perform any database connection, we need an adapter. For the adapter, we can consider
         * it as a library and that library we need to add in our program. 
         * 
         * for any library addition/installation, we need to search for the library in NuGet package
         * manager.
         */

        // connection adapter
        public static NpgsqlConnection con;

        // sql command adapter
        public static NpgsqlCommand cmd;
        private static void establishConnection()
        {
            // in establishing database connection, you need to use exception handling, because it helps
            // to detect if somehow the connection fails or your database server is not going to be 
            // connected

            try
            {
                // create the connection
                con = new NpgsqlConnection(getConnectionString());

                /*
                 * we actually need to pass the connectionString inside the NpgsqlConnection adapter
                 * class constructor. To do so, we are calling the getConnectionString() method as 
                 * this method returns us the Database connection String we have created for 
                 * our work. 
                 */
                MessageBox.Show("Database Connection Successful");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public MainWindow()
        {
            InitializeComponent();
        }

        // step 3: SQL Command Gneration and Execution
        private void insert_Click(object sender, RoutedEventArgs e)
        {
            // establish connection
            establishConnection();
            try
            {
                // open the connection
                con.Open();

                // create the SQL query
                string Query = "insert into user values(default, @login, @pasword, default)";

                // create sql Command 
                cmd = new NpgsqlCommand(Query, con); // this is the command adapter, we need to pass
                                                     // two values in the adapter, one is the query,
                                                     // another one is the connection itself

                // we now need to add the values for the parameters in the sql query

                //cmd.Parameters.AddWithValue("@id", int.Parse("DEFAULT"));
                cmd.Parameters.AddWithValue("@login", "myLogin");
                cmd.Parameters.AddWithValue("@password", PasswordBox.TextInputEvent);
                cmd.Parameters.AddWithValue("@role", "role");

                // execute the Query
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Insertion Successful");
                // close the Connection
                con.Close();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }
    }
}
