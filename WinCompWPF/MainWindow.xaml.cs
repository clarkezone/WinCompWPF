﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WinCompWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Application app;
        Window myWindow;

        private ControlHost listControl;

        private Random random = new Random();
        private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private string[] customerFirstNames = new string[] { "Angel", "Josephine", "Wade", "Christie", "Whitney", "Ismael", "Alexandra", "Rhonda", "Dawn", "Roman", "Emanuel", "Evan", "Aaron", "Bill", "Margaret", "Mandy", "Carlton", "Cornelius", "Cora", "Alejandro", "Annette", "Bertha", "John", "Billy", "Randall" };
        private string[] customerLastNames = new string[] { "Murphy", "Swanson", "Sandoval", "Moore", "Adkins", "Tucker", "Cook", "Fernandez", "Schwartz", "Sharp", "Bryant", "Gross", "Spencer", "Powers", "Hunter", "Moreno", "Baldwin", "Stewart", "Rice", "Watkins", "Hawkins", "Dean", "Howard", "Bailey", "Gill" };

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * Generate customers and instantiate controls
         */ 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            app = System.Windows.Application.Current;
            myWindow = app.MainWindow;
            myWindow.SizeToContent = SizeToContent.WidthAndHeight;

            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < customerFirstNames.Length; i++)
            {
                string id = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
                customers.Add(new Customer(id, customerFirstNames[i], customerLastNames[random.Next(0, customerLastNames.Length - 1)], GenerateRandomDay(), random.NextDouble() >= 0.5, GenerateRandomData()));
            }

            CustomerGrid.ItemsSource = customers;

            listControl = new ControlHost(ControlHostElement.ActualHeight, ControlHostElement.ActualWidth);
            ControlHostElement.Child = listControl;
        }

        /*
         * Send customer info to the control on row double click
         */
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            listControl.UpdateGraph((Customer)CustomerGrid.SelectedItem);
        }

        /*
         * Generate random customer data
         */ 
        private float[] GenerateRandomData()
        {
            var numDataPoints = 6;//random.Next(2, 8);
            float[] data = new float[numDataPoints];

            for (int j = 0; j < numDataPoints; j++)
            {
                data[j] = random.Next(50, 300);
            }

            return data;
        }

        /*
         * Generate random date to use for the customer info
         */
        private DateTime GenerateRandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
