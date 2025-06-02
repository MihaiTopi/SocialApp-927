namespace DesktopProject.Components
{
    using DesktopProject.Proxies;
    using DesktopProject.ViewModels;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;

    public sealed partial class GroupsFeed : UserControl
    {
        private const int ItemsPerPage = 5;
        private int currentPage = 1;
        private PostViewModel postViewModel;

        public GroupsFeed()
        {
            this.InitializeComponent();

            var userRepository = new UserServiceProxy();
            var postRepository = new PostServiceProxy();
            var groupRepository = new GroupServiceProxy();//ar trebui sa fie Service si cu dependency injection nu cu new trebuie schimbat
            var postService = new PostServiceProxy();
            this.postViewModel = new PostViewModel(postService);

            this.LoadItems();
            this.DisplayCurrentPage();
        }

        private void LoadItems()
        {
            var controller = App.Services.GetService<AppController>();
            //this.postViewModel.PopulatePostsGroupsFeed(controller.CurrentUser.Id);
        }

        private void DisplayCurrentPage()
        {
            this.GroupsStackPanel.Children.Clear();
            int startIndex = (this.currentPage - 1) * ItemsPerPage;
            int endIndex = startIndex + ItemsPerPage;
            var allItems = this.postViewModel.GetCurrentPosts();
            for (int i = startIndex; i < endIndex && i < allItems.Count; i++)
            {
                this.GroupsStackPanel.Children.Add(allItems[i]);
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentPage > 1)
            {
                this.currentPage--;
                this.DisplayCurrentPage();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            var listCount = this.postViewModel.GetCurrentPosts().Count;
            if (this.currentPage * ItemsPerPage < listCount)
            {
                this.currentPage++;
                this.DisplayCurrentPage();
            }
        }
    }
}
