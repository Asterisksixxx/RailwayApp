using System;
using System.ComponentModel.DataAnnotations;

namespace RailwayApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(2, ErrorMessage = "Минимальная длина поля-2 символов")]
        [RegularExpression(@"(\S[^0-9])+", ErrorMessage = "Некорректное имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(2, ErrorMessage = "Минимальная длина поля-2 символов")]
        [RegularExpression(@"(\S[^0-9])+", ErrorMessage = "Некорректная фамилия")]
        public string Surname { get; set; }
        public DateTime DataBorn { get; set; }
        public int Year { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(9, ErrorMessage = "Минимальная длина поля-9 символов")]
        [MaxLength(9,ErrorMessage = "Максимальная длина поля-9 символов")]
        [RegularExpression(@"[A-Z]{2}[0-9]+", ErrorMessage = "Некорректный номер паспорта, пример: AA0000000")]
        public string PassportNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string EmailUser { get; set; }
        
    }
}
