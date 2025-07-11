namespace DesktopProject.Components
{
    using System.Collections.Generic;
    using DesktopProject.Pages;
    using DesktopProject.Proxies;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    /// <summary>
    /// Represents a user control for displaying a follower in the social app.
    /// </summary>
    public sealed partial class Follower : UserControl
    {
        private readonly User user;

        private readonly AppController controller;

        private readonly Frame navigationFrame;

        private IUserService userServiceProxy;

        private IPostRepository postRepository;

        private IPostService postService;

        private IGroupService groupServiceProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="Follower"/> class.
        /// </summary>
        /// <param name="username">The username of the follower.</param>
        /// <param name="isFollowing">Indicates whether the current user is following this user.</param>
        /// <param name="user">The user object associated with the follower.</param>
        /// <param name="frame">The navigation frame for navigating to user pages.</param>
        public Follower(string username, bool isFollowing, User user, Frame frame = null)
        {
            this.InitializeComponent();

            this.userServiceProxy = new UserServiceProxy();
            this.groupServiceProxy = new GroupServiceProxy(); //ar trebui sa fie Service si cu dependency injection nu cu new trebuie schimbat
            this.postService = App.Services.GetService<IPostService>();

            this.user = user;
            this.controller = App.Services.GetService<AppController>();
            this.navigationFrame = frame ?? Window.Current.Content as Frame; // Fallback to app-level Frame if not provided
            this.Name.Text = username;
            this.Button.Content = this.IsFollowed() ? "Unfollow" : "Follow";
            this.PointerPressed += this.Follower_Click; // Add click event to the entire control
        }

        /// <summary>
        /// Determines whether the current user is following this user.
        /// </summary>
        /// <returns>True if the user is followed; otherwise, false.</returns>
        private bool IsFollowed()
        {
            List<User> following = this.userServiceProxy.GetUserFollowing(this.controller.CurrentUser.Id);
            foreach (User user in following)
            {
                if (user.Id == this.user.Id)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Handles the click event for the follower control.
        /// </summary>
        private void Follower_Click(object sender, RoutedEventArgs e)
        {
            if (this.navigationFrame != null)
            {
                this.navigationFrame.Navigate(typeof(UserPage), new UserPageNavigationArgs(this.controller, this.user));
            }
        }

        /// <summary>
        /// Handles the click event for the follow/unfollow button.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Button.Content = this.Button.Content.ToString() == "Follow" ? "Unfollow" : "Follow";
            if (!this.IsFollowed())
            {
                this.userServiceProxy.FollowUserById(this.controller.CurrentUser.Id, this.user.Id);
            }
            else
            {
                this.userServiceProxy.UnfollowUserById(controller.CurrentUser.Id, user.Id);
            }
        }
    }

    /// <summary>
    /// Helper class to pass both controller and user for navigation.
    /// </summary>
    public class UserPageNavigationArgs
    {
        /// <summary>
        /// Gets the application controller.
        /// </summary>
        public AppController Controller { get; }

        /// <summary>
        /// Gets the selected user.
        /// </summary>
        public User SelectedUser { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPageNavigationArgs"/> class.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        /// <param name="selectedUser">The selected user.</param>
        public UserPageNavigationArgs(AppController controller, User selectedUser)
        {
            Controller = controller;
            SelectedUser = selectedUser;
        }
    }
}