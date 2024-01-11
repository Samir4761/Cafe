﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Cafe.Data
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the task name.")]
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsDone { get; set; }

        [Required(ErrorMessage = "Please provide a due date.")]
        public DateTime DueDate { get; set; } = DateTime.Today;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

