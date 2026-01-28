namespace ClassInsight.Application.DTOs;

public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string Email, string Role);