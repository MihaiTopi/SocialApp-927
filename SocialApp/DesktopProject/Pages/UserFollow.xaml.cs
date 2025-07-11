using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ServerLibraryProject.Interfaces;
using ServerLibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopProject.Pages
{
    public sealed partial class UserFollow : Page
    {
        private readonly IUserService userService;
        private readonly AppController controller;
        private List<User> allUsers = new();
        private HashSet<long> followingIds = new();

        public UserFollow()
        {
            this.InitializeComponent();

            userService = App.Services.GetService<IUserService>();
            controller = App.Services.GetService<AppController>();

            LoadUsers();
        }

        private void LoadUsers(string search = "")
        {
            UserListPanel.Children.Clear();

            var currentUserId = controller.CurrentUser.Id;

            // All other users except current
            allUsers = userService.GetAllUsers()
                .Where(u => u.Id != currentUserId &&
                            u.Username.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();

            followingIds = userService.GetUserFollowing(currentUserId)
                                      .Select(u => u.Id)
                                      .ToHashSet();

            foreach (var user in allUsers)
            {
                var rowGrid = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10)
                };
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

                var nameText = new TextBlock
                {
                    Text = user.Username,
                    FontSize = 16,
                    Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black),
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(nameText, 0);
                rowGrid.Children.Add(nameText);

                var button = new Button
                {
                    Content = followingIds.Contains(user.Id) ? "Unfollow" : "Follow",
                    Background = new SolidColorBrush(followingIds.Contains(user.Id)
                        ? Microsoft.UI.Colors.OrangeRed
                        : Microsoft.UI.Colors.Green),
                    Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                    Width = 80,
                    Height = 30,
                    Tag = user.Id
                };

                // Remove hover flicker by setting style directly
                button.Resources["ButtonBackgroundPointerOver"] = button.Background;
                button.Resources["ButtonForegroundPointerOver"] = button.Foreground;

                button.Click += ToggleFollow_Click;
                Grid.SetColumn(button, 1);
                rowGrid.Children.Add(button);

                UserListPanel.Children.Add(rowGrid);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Change this to the page you want to navigate to
            this.Frame.Navigate(typeof(HomeScreen));
        }

        private void ToggleFollow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            long targetId = (long)button.Tag;
            long currentUserId = controller.CurrentUser.Id;

            if (followingIds.Contains(targetId))
            {
                userService.UnfollowUserById(currentUserId, targetId);
            }
            else
            {
                userService.FollowUserById(currentUserId, targetId);
            }

            LoadUsers(SearchBox.Text); // Refresh UI
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUsers(SearchBox.Text);
        }
    }
}
