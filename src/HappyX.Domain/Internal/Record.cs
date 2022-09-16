﻿namespace HappyX.Domain.Internal;

public class Record : BaseEntity
{
    public Record(int userId, int moodId)
    {
        UserId = userId;
        MoodId = moodId;
    }
    
    public Record(User user, Mood mood)
    {
        User = user;
        Mood = mood;
    }
    
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public int MoodId { get; set; }
    public User? User { get; set; }
    public Mood? Mood { get; set; }
}