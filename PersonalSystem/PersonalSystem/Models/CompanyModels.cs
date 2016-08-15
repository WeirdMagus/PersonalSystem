using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalSystem.Models
{
	public class Company
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual ApplicationUser Admin { get; set; }
		public virtual IEnumerable<Vacancy> Vacancies { get; set; }
		public virtual IEnumerable<News> Newser { get; set; }
	}

	public class Department
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public virtual IEnumerable<Group> Grouper { get; set; }
	}

	public class Group
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Department Department { get; set; }
		public virtual Schema Schema { get; set; }
		public virtual IEnumerable<ApplicationUser> Users { get; set; }
	}

	public class Schema
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class News
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public bool IsInternal { get; set; }
		public string Text { get; set; }
	}

	public class Vacancy
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual IEnumerable<Application> Applications { get; set; }
	}

	public class Application
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Vacancy Vacancy { get; set; }
	}

}