﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QuizService.DataAccess;
using QuizService.Model;
using System;

namespace QuizService.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20180128184808_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("QuizService.Model.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCorrect");

                    b.Property<int?>("QuestionId");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("QuizService.Model.AnswerTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("Order");

                    b.Property<int?>("QuestionTemplateId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTemplateId");

                    b.ToTable("AnswerTemplate");
                });

            modelBuilder.Entity("QuizService.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateEnd");

                    b.Property<DateTime?>("DateStart");

                    b.Property<int>("Order");

                    b.Property<int?>("QuizId");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizService.Model.QuestionTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionType");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("QuestionTemplates");
                });

            modelBuilder.Entity("QuizService.Model.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateEnd");

                    b.Property<DateTime?>("DateStart");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizService.Model.QuizQuestionTemplate", b =>
                {
                    b.Property<int>("QuizTemplateId");

                    b.Property<int>("Order");

                    b.Property<int>("QuestionTemplateId");

                    b.Property<bool>("Enabled");

                    b.HasKey("QuizTemplateId", "Order", "QuestionTemplateId");

                    b.HasIndex("QuestionTemplateId");

                    b.ToTable("QuizQuestionTemplates");
                });

            modelBuilder.Entity("QuizService.Model.QuizTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("QuizTemplates");
                });

            modelBuilder.Entity("QuizService.Model.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuizId");

                    b.Property<decimal>("ScoresAmount");

                    b.HasKey("Id");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("QuizService.Model.Answer", b =>
                {
                    b.HasOne("QuizService.Model.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("QuizService.Model.AnswerTemplate", b =>
                {
                    b.HasOne("QuizService.Model.QuestionTemplate")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionTemplateId");
                });

            modelBuilder.Entity("QuizService.Model.Question", b =>
                {
                    b.HasOne("QuizService.Model.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("QuizService.Model.QuizQuestionTemplate", b =>
                {
                    b.HasOne("QuizService.Model.QuestionTemplate", "QuestionTemplate")
                        .WithMany()
                        .HasForeignKey("QuestionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuizService.Model.QuizTemplate", "QuizTemplate")
                        .WithMany()
                        .HasForeignKey("QuizTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
