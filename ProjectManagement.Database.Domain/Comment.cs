﻿namespace ProjectManagement.Database.Domain;

public class Comment
{
	public uint Id { get; set; }
	public uint UserId { get; set; }
	public User? User { get; set; }
	public uint TicketId { get; set; }
	public Ticket? Ticket { get; set; }
	public string Text { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
