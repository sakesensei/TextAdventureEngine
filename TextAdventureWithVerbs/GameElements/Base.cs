namespace GameElements
{
	public class Base
	{
		private string name;
		private string description;

		public string Name
		{
			get { return name; }
			private set { name = value; }
		}
		public string Description
		{
			get { return description; }
			private set { description = value; }
		}

		public Base(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}
	}
}