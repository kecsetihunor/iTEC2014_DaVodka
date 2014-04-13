using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTEC.Storage.Entities
{
    [Table("PlaceOwnerships")]
    public class PlaceOwnership
    {
        public Int32 Id { get; set; }

        public virtual Account Account { get; set; }
        public virtual Place Place { get; set; }
    }

    [Table("Places")]
    public class Place
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Location Location { get; set; }

        public virtual Menu Menu { get; set; }
    }

    [Table("MenuEntries")]
    public class MenuEntry
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Double Price { get; set; }
    }

    [Table("MenuCategories")]
    public class MenuCategory
    {
        public Int32 Id { get; set; }

        public virtual IEnumerable<MenuEntry> Entries { get; set; }
    }

    [Table("Menus")]
    public class Menu
    {
        public Int32 Id { get; set; }

        public virtual IEnumerable<MenuCategory> Categories { get; set; }
    }

    [Table("Locations")]
    public class Location
    {
        public Int32 Id { get; set; }

        public String Address { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }
    }

    [Table("Contacts")]
    public class Contact
    {
        public Int32 Id { get; set; }

        public Schedule Schedule { get; set; }

        public virtual IEnumerable<Phone> Phones { get; set; }
    }

    [Table("Schedules")]
    public class Schedule
    {
        public Int32 Id { get; set; }

        public virtual ScheduleDay Monday { get; set; }
        public virtual ScheduleDay Tuesday { get; set; }
        public virtual ScheduleDay Wednesday { get; set; }
        public virtual ScheduleDay Thursday { get; set; }
        public virtual ScheduleDay Friday { get; set; }
        public virtual ScheduleDay Saturday { get; set; }
        public virtual ScheduleDay Sunday { get; set; }
    }

    public class ScheduleDay
    {
        public Int32 Id { get; set; }

        public TimeSpan Open { get; set; }
        public TimeSpan Close { get; set; }
    }

    [Table("Phones")]
    public class Phone
    {
        public Int32 Id { get; set; }

        public String Number { get; set; }
    }
}