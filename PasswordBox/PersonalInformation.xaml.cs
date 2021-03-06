﻿using PasswordBox.Common;
using PasswordBox.Model;
using PasswordBox.Services;
using PasswordBox.ViewModel;
using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PersonalMessage : Page
    {
        public PersonalMessage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 用于绑定页面上的个人信息
        /// </summary>
        PersonalInfo Info = new PersonalInfo();

        /// <summary>
        /// 用户设置个人信息
        /// 个人信息页面的"应用"按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SetInformation(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog;
            dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "确认",
                Content = "确定要修改信息?",
                FullSizeDesired = false
            };
            if (Info.Email != "" && CheckEmail(Info.Email) == false)
            {
                /// 判断答案是否不为空格式且格式错误
                dialog.Content = "邮箱格式错误";
                dialog.PrimaryButtonClick += (_s, _e) => { };
            }
            else
            {
                /// 确认个人信息无不合法填写时
                /// 提示用户是否保存修改信息
                /// 点击"确认"保存，点击"取消"不保存
                dialog.Content = "确定修改信息?";
                dialog.SecondaryButtonText = "取消";
                dialog.PrimaryButtonClick += (_s, _e) => {
                    UserInfo.SetInfo("UserName", Info.Name);
                    UserInfo.SetInfo("Birth", Info.Birth);
                    UserInfo.SetInfo("Email", Info.Email);
                    UserInfo.SaveImage(Info.Avator, "Avator.jpg");
                    LiveTile.LoadTile();
                };
                dialog.SecondaryButtonClick += (_s, _e) => { };
            }
            await dialog.ShowAsync();
        }


        /// <summary>
        /// 选择图片作为用户头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SelectPicture(object sender, RoutedEventArgs e)
        {
            /// 调用图片选择接口
            var p = await ImageHelper.Picker();
            /// 判断是否选择了图片,不为空时再对用户头像进行修改
            /// 避免选择图片接口返回的值为空时(即未选择图片时),直接赋值给用户头像造成异常
            if (p != null)
            {
                Info.Avator = p;
            }
        }

        /// <summary>
        /// 检查邮箱格式是否合法
        /// </summary>
        /// <returns></returns>
        private bool CheckEmail(string email)
        {
            string emailReg = @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$";
            Regex reg = new Regex(emailReg);
            return reg.IsMatch(email);
        }
    }
}
