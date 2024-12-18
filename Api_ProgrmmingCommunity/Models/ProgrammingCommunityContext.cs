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

    public virtual DbSet<RoundResult> RoundResults { get; set; }

    public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubmittedTask> SubmittedTasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskQuestion> TaskQuestions { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=haadi123_;User Id=haadi123_;Password=db123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FBD05D5FC");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F7D923468");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.CompetitionRoundQuestionId).HasColumnName("competition_round_question_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SubmissionTime).HasColumnName("submission_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CompetitionRoundQuestion).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.CompetitionRoundQuestionId)
                .HasConstraintName("FK__Competiti__compe__0E6E26BF");

            entity.HasOne(d => d.User).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___0F624AF8");
        });

        modelBuilder.Entity<CompetitionMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F46FC5237");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionMembers)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.CompetitionMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___07C12930");
        });

        modelBuilder.Entity<CompetitionRound>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FDBB7C939");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.RoundType).HasColumnName("round_type");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionRounds)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__00200768");
        });

        modelBuilder.Entity<CompetitionRoundQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F64A45024");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionRoundId).HasColumnName("competition_round_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__Competiti__compe__02FC7413");

            entity.HasOne(d => d.Question).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Competiti__quest__03F0984C");
        });

        modelBuilder.Entity<ExpertSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertSu__3213E83FF8C802B0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertSub__exper__619B8048");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__ExpertSub__subje__628FA481");
        });

        modelBuilder.Entity<ExpertTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertTo__3213E83F1AD88AF8");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertTop__exper__693CA210");

            entity.HasOne(d => d.Topic).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__ExpertTop__topic__6A30C649");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F99886BCF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");
            entity.Property(e => e.Text)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__Questions__subje__6D0D32F4");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Questions__topic__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Questions__user___6EF57B66");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F763ADC50");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.Option)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__quest__71D1E811");
        });

        modelBuilder.Entity<RoundQualificationCriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundQua__3213E83FEDABEA27");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromRoundId).HasColumnName("from_round_id");
            entity.Property(e => e.ToRoundId).HasColumnName("to_round_id");
            entity.Property(e => e.TopTeams).HasColumnName("top_teams");

            entity.HasOne(d => d.FromRound).WithMany(p => p.RoundQualificationCriterionFromRounds)
                .HasForeignKey(d => d.FromRoundId)
                .HasConstraintName("FK__RoundQual__from___0A9D95DB");

            entity.HasOne(d => d.ToRound).WithMany(p => p.RoundQualificationCriterionToRounds)
                .HasForeignKey(d => d.ToRoundId)
                .HasConstraintName("FK__RoundQual__to_ro__0B91BA14");
        });

        modelBuilder.Entity<RoundResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundRes__3214EC078BAE7C2F");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__RoundResu__Compe__17036CC0");

            entity.HasOne(d => d.User).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RoundResu__UserI__17F790F9");
        });

        modelBuilder.Entity<StudentSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentS__3213E83FE7FD9C7B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__StudentSu__stude__656C112C");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__StudentSu__subje__66603565");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Subjects__357D4CF8688FB014");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Title)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SubmittedTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Submitte__3213E83F35B72B1A");

            entity.Property(e => e.Id).HasColumnName("id");
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
                .HasConstraintName("FK__Submitted__taskq__7A672E12");

            entity.HasOne(d => d.User).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Submitted__user___7B5B524B");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3213E83F0AB4331A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
        });

        modelBuilder.Entity<TaskQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskQues__3213E83FB8890FE0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__TaskQuest__quest__778AC167");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskQuest__task___76969D2E");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3213E83FA987266F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Topics)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__Topics__subject___5EBF139D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FBE435DBF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .IsUnicode(false)
                .HasColumnName("lastname");
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
