using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalSystem.Models
{
	public class Company
	{
		public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual ApplicationUser Admin { get; set; }
		public virtual IEnumerable<Vacancy> Vacancies { get; set; }
		public virtual IEnumerable<News> News { get; set; }
	}

	public class Department
	{
		public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public virtual IEnumerable<Group> Grouper { get; set; }
	}

	public class Group
	{
		public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Department Department { get; set; }
		public virtual Schedule Schedule { get; set; }
		public virtual IEnumerable<ApplicationUser> Users { get; set; }
	}

	public class Schedule
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
        public string JobTitle { get; set; }
        public virtual IEnumerable<Application> Applications { get; set; }
	}

	public class Application
	{
		public int Id { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
        public string CoverLetter { get; set; }     // Personligt brev

        [Required]
		public virtual Vacancy Vacancy { get; set; }

        [Required]
        public virtual ApplicationUser Applicant { get; set; }
	}

}