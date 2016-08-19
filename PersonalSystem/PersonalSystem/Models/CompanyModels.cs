using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalSystem.Models
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
	}
	public class Company : BaseEntity
	{
		//public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual ApplicationUser Admin { get; set; }
		public virtual ICollection<Department> Departments { get; set; }
		public virtual ICollection<Vacancy> Vacancies { get; set; }
		public virtual ICollection<News> News { get; set; }

		public Company()
		{
			Departments = new List<Department> { new Department() };
			Vacancies = new List<Vacancy>();
			News = new List<News>();
		}
	}

	public class Department : BaseEntity
	{
		//public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public virtual ICollection<Group> Groups { get; set; }

		public Department()
		{
			Groups = new List<Group> { new Group() };
		}
	}

	public class Group : BaseEntity
	{
		//public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
		public virtual Department Department { get; set; }
		public virtual Schedule Schedule { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}

	public class DailySchedule
	{
		[NotMapped]
		public DayOfWeek day { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}

	public class Schedule : BaseEntity
	{
		//public int Id { get; set; }
		public string Name { get; set; }
		public DailySchedule Monday { get; set; }
		public DailySchedule Tuesday { get; set; }
		public DailySchedule Wednesday { get; set; }
		public DailySchedule Thursday { get; set; }
		public DailySchedule Friday { get; set; }
		public DailySchedule Saturday { get; set; }
		public DailySchedule Sunday { get; set; }
		[NotMapped]
		public List<DailySchedule> FullSchedule
		{
			get
			{
				return GetFullSchedule();
			}
		}

		private List<DailySchedule> GetFullSchedule()
		{
			Monday.day = DayOfWeek.Monday;
			Tuesday.day = DayOfWeek.Tuesday;
			Wednesday.day = DayOfWeek.Wednesday;
			Thursday.day = DayOfWeek.Thursday;
			Friday.day = DayOfWeek.Friday;
			Saturday.day = DayOfWeek.Saturday;
			Sunday.day = DayOfWeek.Sunday;
			return new List<DailySchedule>
				{
					Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
				};
		}
	}

	public class News : BaseEntity
	{
		public string Name { get; set; }
		public virtual Company Company { get; set; }
		public bool IsInternal { get; set; }
		public string Text { get; set; }
	}

	public class Vacancy : BaseEntity
	{
		[Required]
		[StringLength(50, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string JobTitle { get; set; }
		public virtual ICollection<Application> Applications { get; set; }
	}

	public class Application : BaseEntity
	{
		[Required]
		[StringLength(5000, ErrorMessage = "{0} needs to be at least {1} characters", MinimumLength = 3)]
		public string CoverLetter { get; set; }     // Personligt brev

		[Required]
		public virtual Vacancy Vacancy { get; set; }

		[Required]
		public virtual ApplicationUser Applicant { get; set; }
	}

}
