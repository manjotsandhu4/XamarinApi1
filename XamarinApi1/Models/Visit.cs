using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XamarinApi1.Models
{
    public class Visit
    {
        
        public int Id { get; set; }
        [Required]
        public string OccurenceNo { get; set; }
        [Required]
        public string NameOfAccused { get; set; }
        [Required]
        public DateTime DateOfCourtAppearence { get; set; }
        [Required]
        public string CourtAppearenceTime { get; set; }
        [Required]
        public string DateOfOffence { get; set; }
        [Required]
        public string CourtHouseAddress { get; set; }
        [Required]
        public string ReasonForAppearence { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Testify { get; set; }
        public string TimeCalledIn { get; set; }
        public string NoTestifyReason { get; set; }
        public string LunchTimeStart { get; set; }
        public string LunchTimeEnd { get; set; }
    }
}