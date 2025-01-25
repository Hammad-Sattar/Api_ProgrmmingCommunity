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

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WinnerBoard> WinnerBoards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=haadi123_;User Id=haadi123_;Password=db123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.CompetitionId).HasName("PK__Competit__BB383B58D8DE8D79");

            entity.Property(e => e.CompetitionId)
                .ValueGeneratedNever()
                .HasColumnName("competition_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaxLevel).HasColumnName("max_level");
            entity.Property(e => e.MinLevel).HasColumnName("min_level");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.User).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Competiti__user___0E04126B");
        });

        modelBuilder.Entity<CompetitionAttemptedQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F35B5FCBD");

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
                .HasConstraintName("FK__Competiti__compe__23F3538A");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionAttemptedQuestions)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__team___24E777C3");
        });

        modelBuilder.Entity<CompetitionRound>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FE0520461");

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
                .HasConstraintName("FK__Competiti__compe__11D4A34F");
        });

        modelBuilder.Entity<CompetitionRoundQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F46ECF492");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionRoundId).HasColumnName("competition_round_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__Competiti__compe__15A53433");

            entity.HasOne(d => d.Question).WithMany(p => p.CompetitionRoundQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Competiti__quest__1699586C");
        });

        modelBuilder.Entity<CompetitionTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83FBA09CCB7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__compe__1A69E950");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__team___1B5E0D89");
        });

        modelBuilder.Entity<ExpertSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertSu__3213E83FBE361930");

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
                .HasConstraintName("FK__ExpertSub__exper__69C6B1F5");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.ExpertSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__ExpertSub__subje__6ABAD62E");
        });

        modelBuilder.Entity<ExpertTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertTo__3213E83F5A57DDDF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK__ExpertTop__exper__73501C2F");

            entity.HasOne(d => d.Topic).WithMany(p => p.ExpertTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__ExpertTop__topic__74444068");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F9224BC40");

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
                .HasConstraintName("FK__Questions__subje__7814D14C");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Questions__topic__7908F585");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Questions__user___79FD19BE");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F66050DFB");

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
                .HasConstraintName("FK__QuestionO__quest__7DCDAAA2");
        });

        modelBuilder.Entity<RoundQualificationCriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundQua__3213E83FF3E05F24");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromRoundId).HasColumnName("from_round_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.ToRoundId).HasColumnName("to_round_id");
            entity.Property(e => e.TopTeams).HasColumnName("top_teams");

            entity.HasOne(d => d.FromRound).WithMany(p => p.RoundQualificationCriterionFromRounds)
                .HasForeignKey(d => d.FromRoundId)
                .HasConstraintName("FK__RoundQual__from___1F2E9E6D");

            entity.HasOne(d => d.ToRound).WithMany(p => p.RoundQualificationCriterionToRounds)
                .HasForeignKey(d => d.ToRoundId)
                .HasConstraintName("FK__RoundQual__to_ro__2022C2A6");
        });

        modelBuilder.Entity<RoundResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoundRes__3213E83F62212F2E");

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
                .HasConstraintName("FK__RoundResu__compe__2AA05119");

            entity.HasOne(d => d.CompetitionRound).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.CompetitionRoundId)
                .HasConstraintName("FK__RoundResu__compe__28B808A7");

            entity.HasOne(d => d.Team).WithMany(p => p.RoundResults)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__RoundResu__team___29AC2CE0");
        });

        modelBuilder.Entity<StudentSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentS__3213E83F503304AE");

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
                .HasConstraintName("FK__StudentSu__stude__6E8B6712");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .HasConstraintName("FK__StudentSu__subje__6F7F8B4B");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Subjects__357D4CF8556A79F3");

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
            entity.HasKey(e => e.Id).HasName("PK__Submitte__3213E83FA87B58BE");

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
                .HasConstraintName("FK__Submitted__taskq__093F5D4E");

            entity.HasOne(d => d.User).WithMany(p => p.SubmittedTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Submitted__user___0A338187");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3213E83FB034C472");

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
            entity.HasKey(e => e.Id).HasName("PK__TaskQues__3213E83F78DA9290");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__TaskQuest__quest__056ECC6A");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskQuestions)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskQuest__task___047AA831");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__F82DEDBC714262F6");

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Member1Id).HasColumnName("member1_id");
            entity.Property(e => e.Member2Id).HasColumnName("member2_id");
            entity.Property(e => e.Member3Id).HasColumnName("member3_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("team_name");

            entity.HasOne(d => d.Member1).WithMany(p => p.TeamMember1s)
                .HasForeignKey(d => d.Member1Id)
                .HasConstraintName("FK__Teams__member1_i__5D60DB10");

            entity.HasOne(d => d.Member2).WithMany(p => p.TeamMember2s)
                .HasForeignKey(d => d.Member2Id)
                .HasConstraintName("FK__Teams__member2_i__5E54FF49");

            entity.HasOne(d => d.Member3).WithMany(p => p.TeamMember3s)
                .HasForeignKey(d => d.Member3Id)
                .HasConstraintName("FK__Teams__member3_i__5F492382");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3213E83F4156889E");

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
                .HasConstraintName("FK__Topics__subject___65F62111");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FAD0DF587");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Empid)
                .HasMaxLength(255)
                .IsUnicode(false);
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
            entity.HasKey(e => e.Id).HasName("PK__WinnerBo__3213E83FA8401300");

            entity.ToTable("WinnerBoard");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Competition).WithMany(p => p.WinnerBoards)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__WinnerBoa__compe__3429BB53");

            entity.HasOne(d => d.Team).WithMany(p => p.WinnerBoards)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__WinnerBoa__team___351DDF8C");
        });

        OnModelCreatingPartial(modelBuilder);
        }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
