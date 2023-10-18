﻿namespace BusinessPortal2.Models
{
    public class LeaveRequest
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequest { get; set; }
        public string ApprovalState { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveType? leavetype { get; set; }
        public int PersonalId { get; set; }
        public Personal personal { get; set; }


    }
}
