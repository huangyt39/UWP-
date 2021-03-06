﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PasswordBox;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using PasswordBox.Model;
using Windows.Storage.Streams;
using Windows.Storage;
using PasswordBox.ViewModel;
using PasswordBox.Common;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace PasswordBox
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            NavMenuPrimaryListView.ItemsSource = MenuItems.navMenuPrimaryItem;
            NavMenuSecondaryListView.ItemsSource = MenuItems.navMenuSecondaryItem;
            // SplitView 开关
            PaneOpenButton.Click += (sender, args) =>
            {
                RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
            };
            // 导航事件
            NavMenuPrimaryListView.ItemClick += NavMenuListView_ItemClick;
            NavMenuSecondaryListView.ItemClick += NavMenuListView_ItemClick;
            // 默认页
            RootFrame.Navigate(typeof(Home));
        }

        private void NavMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 遍历，将选中Rectangle隐藏
            foreach (var np in MenuItems.navMenuPrimaryItem)
            {
                np.Selected = Visibility.Collapsed;
            }
            foreach (var ns in MenuItems.navMenuSecondaryItem)
            {
                ns.Selected = Visibility.Collapsed;
            }

            NavMenuItem item = e.ClickedItem as NavMenuItem;
            // Rectangle显示并导航
            item.Selected = Visibility.Collapsed;
            if (item.DestPage != null)
            {
                RootFrame.Navigate(item.DestPage);
            }

            RootSplitView.IsPaneOpen = false;
        }

        /// <summary>
        /// 汉堡菜单各按钮对应跳转页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(Home));
        }

        private void NavigateToAdd(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(NewOrUpdate));
        }

        private void NavigateToPersonalInfo(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(PersonalMessage));
        }

        private void NavigateToChangePassword(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(typeof(ChangePassword));
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RootFrame.Navigate(typeof(Home));
        }
    }
}
