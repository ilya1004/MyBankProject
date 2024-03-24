﻿using System.ComponentModel.DataAnnotations;

namespace MyBank.API.DataTransferObjects.AdminDtos;

public record RegisterAdminDto
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public string Nickname { get; set; } = string.Empty;
}
