namespace DemoApplication.Areas.Admin.ViewModels.Author
{
    public class AddViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }



        public AddViewModel()
        {

        }

        public AddViewModel(int id, string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }
      
    }
}
