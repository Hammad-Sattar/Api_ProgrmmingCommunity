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

    public virtual DbSet<CompetitionRound> CompetitionRounds { get; set; }

    public virtual DbSet<CompetitionRoundQuestion> CompetitionRoundQuestions { get; set; }

    public virtual DbSet<CompetitionTeam> CompetitionTeams { get; set; }

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

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WinnerBoard> WinnerBoards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PROGRAMMER;Database=ProgrammingCommunity;User ID=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.CompetitionId).HasName("PK__Competit__BB383B58E54E3292");

            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.User).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___6D0D32F4");
        });

        modelBuilder.Entity<CompetitionAttemptedQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FC0FE5C95");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.CompetitionRoundQuestionId).HasColumnName("competition_round_question_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SubmissionTime).HasColumnName("submission_time");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.CompetitionRoundQuestion).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.CompetitionRoundQuestionId)
                .HasConstraintName("FK__Competiti__compe__02FC7413");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__team___03F0984C");
        });

        modelBuilder.Entity<CompetitionRound>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F2750876F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.RoundType).HasColumnName("round_type");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionRounds)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__70DDC3D8");
        });

        modelBuilder.Entity<CompetitionRoundQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F873890F1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionRoundId).HasColumnName("competition_round_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__Competiti__compe__74AE54BC");

            entity.HasOne(d => d.Question).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Competiti__quest__75A278F5");
        });

        modelBuilder.Entity<CompetitionTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F149C34DD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__797309D9");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__team___7A672E12");
        });

        modelBuilder.Entity<ExpertSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertSu__3213E83FAB07BFE9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertSub__exper__48CFD27E");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__ExpertSub__subje__49C3F6B7");
        });

        modelBuilder.Entity<ExpertTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertTo__3213E83F0DDEA096");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertTop__exper__52593CB8");

            entity.HasOne(d => d.Topic).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__ExpertTop__topic__534D60F1");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F1C5D4A65");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Repeated).HasColumnName("repeated");
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
            entity.Property(e => e.YearlyRepeated).HasColumnName("yearly_repeated");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__Questions__subje__571DF1D5");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Questions__topic__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Questions__user___59063A47");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83FC7515D56");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Option)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__quest__5CD6CB2B");
        });

        modelBuilder.Entity<RoundQualificationCriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundQua__3213E83F7306A83D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromRoundId).HasColumnName("from_round_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.ToRoundId).HasColumnName("to_round_id");
            entity.Property(e => e.TopTeams).HasColumnName("top_teams");

            entity.HasOne(d => d.FromRound).WithMany(p => p.RoundQualificationCriterionFromRounds)
                .HasForeignKey(d => d.FromRoundId)
                .HasConstraintName("FK__RoundQual__from___7E37BEF6");

            entity.HasOne(d => d.ToRound).WithMany(p => p.RoundQualificationCriterionToRounds)
                .HasForeignKey(d => d.ToRoundId)
                .HasConstraintName("FK__RoundQual__to_ro__7F2BE32F");
        });

        modelBuilder.Entity<RoundResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundRes__3213E83F53528C29");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.CompetitionRoundId).HasColumnName("competition_round_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.IsQualified).HasColumnName("isQualified");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__RoundResu__compe__09A971A2");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__RoundResu__compe__07C12930");

            entity.HasOne(d => d.Team).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__RoundResu__team___08B54D69");
        });

        modelBuilder.Entity<StudentSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentS__3213E83F2A17C1DD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__StudentSu__stude__4D94879B");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__StudentSu__subje__4E88ABD4");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Subjects__357D4CF8A570BE0F");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Title)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SubmittedTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Submitte__3213E83F4C25006C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SubmissionDate).HasColumnName("submission_date");
            entity.Property(e => e.SubmissionTime).HasColumnName("submission_time");
            entity.Property(e => e.TaskquestionId).HasColumnName("taskquestion_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Taskquestion).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.TaskquestionId)
                .HasConstraintName("FK__Submitted__taskq__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Submitted__user___693CA210");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3213E83F7E5DFC9F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
        });

        modelBuilder.Entity<TaskQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskQues__3213E83FBA7ACE16");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__TaskQuest__quest__6477ECF3");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskQuest__task___6383C8BA");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__F82DEDBCC42B42C0");

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TeamName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("team_name");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamMemb__3213E83F5BCAF99F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamMembe__team___3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TeamMembe__user___3E52440B");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3213E83F99EC93DF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
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
                .HasConstraintName("FK__Topics__subject___44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F68885B5B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
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

        modelBuilder.Entity<WinnerBoard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WinnerBo__3213E83F0BA43E97");

            entity.ToTable("WinnerBoard");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.WinnerBoards)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__WinnerBoa__compe__0D7A0286");

            entity.HasOne(d => d.Team).WithMany(p => p.WinnerBoards)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__WinnerBoa__team___0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
