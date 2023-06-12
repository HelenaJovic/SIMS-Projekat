﻿using InitialProject.Domain.Model;
using InitialProject.WPF.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for GuideFrame.xaml
    /// </summary>
    public partial class GuideFrame : Window
    {
        public GuideFrame(User user)
        {
            InitializeComponent();

            Frame mainFrame = (Frame)FindName("MainFrame");
            DataContext = new GuideFrameViewModel(user, mainFrame);
        }
    }
}
