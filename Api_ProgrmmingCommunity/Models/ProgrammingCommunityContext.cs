using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api_ProgrmmingCommunity.Models;

public partial class ProgrammingCommunityContext : DbContext
{
    public ProgrammingCommunityContext()
    {
    }

    public ProgrammingCommunityContext(DbContextOptions<ProgrammingCommunityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionAttemptedQuestion> CompetitionAttemptedQuestions { get; set; }

    public virtual DbSet<CompetitionMember> CompetitionMembers { get; set; }

    public virtual DbSet<CompetitionRound> CompetitionRounds { get; set; }

    public virtual DbSet<CompetitionRoundQuestion> CompetitionRoundQuestions { get; set; }

    public virtual DbSet<ExpertSubject> ExpertSubjects { get; set; }

    public virtual DbSet<ExpertTopic> ExpertTopics { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<RoundQualificationCriterion> RoundQualificationCriteria { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubmittedTask> SubmittedTasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskQuestion> TaskQuestions { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=PROGRAMMER;Database=ProgrammingCommunity;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F22CDB11B");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<CompetitionAttemptedQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F8A10BB9C");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Answer)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.CompetitionRoundQuestionId).HasColumnName("competition_round_question_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SubmissionTime).HasColumnName("submission_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CompetitionRoundQuestion).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.CompetitionRoundQuestionId)
                .HasConstraintName("FK__Competiti__compe__6754599E");

            entity.HasOne(d => d.User).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___68487DD7");
        });

        modelBuilder.Entity<CompetitionMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FCD1B3DF3");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionMembers)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.CompetitionMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___60A75C0F");
        });

        modelBuilder.Entity<CompetitionRound>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FBA467330");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.RoundType).HasColumnName("round_type");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionRounds)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__59063A47");
        });

        modelBuilder.Entity<CompetitionRoundQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F8CB9EFFD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompetitionRoundId).HasColumnName("competition_round_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__Competiti__compe__5BE2A6F2");

            entity.HasOne(d => d.Question).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Competiti__quest__5CD6CB2B");
        });

        modelBuilder.Entity<ExpertSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertSu__3213E83F76334420");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.SubjectCode).HasColumnName("subject_code");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertSub__exper__3E52440B");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__ExpertSub__subje__3F466844");
        });

        modelBuilder.Entity<ExpertTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertTo__3213E83FA27CC8ED");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertTop__exper__4222D4EF");

            entity.HasOne(d => d.Topic).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__ExpertTop__topic__4316F928");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F151A3242");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.SubjectCode).HasColumnName("subject_code");
            entity.Property(e => e.Text)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__Questions__subje__45F365D3");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Questions__topic__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Questions__user___47DBAE45");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F4BE9B103");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.Option)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__quest__4AB81AF0");
        });

        modelBuilder.Entity<RoundQualificationCriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundQua__3213E83F3370D580");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FromRoundId).HasColumnName("from_round_id");
            entity.Property(e => e.ToRoundId).HasColumnName("to_round_id");
            entity.Property(e => e.TopTeams).HasColumnName("top_teams");

            entity.HasOne(d => d.FromRound).WithMany(p => p.RoundQualificationCriterionFromRounds)
                .HasForeignKey(d => d.FromRoundId)
                .HasConstraintName("FK__RoundQual__from___6383C8BA");

            entity.HasOne(d => d.ToRound).WithMany(p => p.RoundQualificationCriterionToRounds)
                .HasForeignKey(d => d.ToRoundId)
                .HasConstraintName("FK__RoundQual__to_ro__6477ECF3");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Subjects__357D4CF8D1FB508F");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Title)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SubmittedTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Submitte__3213E83F6D426798");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Answer)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SubmissionDate).HasColumnName("submission_date");
            entity.Property(e => e.SubmissionTime).HasColumnName("submission_time");
            entity.Property(e => e.TaskquestionId).HasColumnName("taskquestion_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Taskquestion).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.TaskquestionId)
                .HasConstraintName("FK__Submitted__taskq__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Submitted__user___5441852A");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3213E83FFBC03264");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
        });

        modelBuilder.Entity<TaskQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskQues__3213E83FBC5C8F10");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__TaskQuest__quest__5070F446");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskQuest__task___4F7CD00D");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3213E83F25040EEE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.SubjectCode).HasColumnName("subject_code");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Topics)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__Topics__subject___3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F2341640B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phonenum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phonenum");
            entity.Property(e => e.Profimage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profimage");
            entity.Property(e => e.RegNum)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reg_num");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Section)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("section");
            entity.Property(e => e.Semester).HasColumnName("semester");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
