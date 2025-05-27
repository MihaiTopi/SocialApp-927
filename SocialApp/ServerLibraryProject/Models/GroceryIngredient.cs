namespace ServerLibraryProject.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;
    using CommunityToolkit.Mvvm.ComponentModel;

    [Table("grocery_lists")]
    public class GroceryIngredient : ObservableObject
    {
        [Column("u_id")]
        [JsonIgnore]
        public long Id { get; set; }

        [Column("i_id")]
        public int IngredientId { get; set; }

        private string _name = string.Empty;

        [NotMapped]
        public string Name
        {
            get => this._name;
            set => this.SetProperty(ref this._name, value);
        }

        private bool _isChecked;

        [Column("is_checked")]
        public bool IsChecked
        {
            get => this._isChecked;
            set => this.SetProperty(ref this._isChecked, value);
        }

        public static readonly GroceryIngredient defaultIngredient = new GroceryIngredient() { Id = -1, _name = string.Empty, IsChecked = false };
    }
}