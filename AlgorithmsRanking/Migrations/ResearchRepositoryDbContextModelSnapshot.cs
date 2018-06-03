﻿// <auto-generated />
using AlgorithmsRanking.DbContexts;
using AlgorithmsRanking.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AlgorithmsRanking.Migrations
{
    [DbContext(typeof(ResearchRepositoryDbContext))]
    partial class ResearchRepositoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlgorithmsRanking.Entities.Algorithm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Algorithms");
                });

            modelBuilder.Entity("AlgorithmsRanking.Entities.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("PostedAt");

                    b.Property<int>("ResearchId");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AlgorithmsRanking.Entities.DataSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("DataSets");
                });

            modelBuilder.Entity("AlgorithmsRanking.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("AlgorithmsRanking.Entities.Research", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float?>("AccuracyRate");

                    b.Property<int>("AlgorithmId");

                    b.Property<DateTime?>("AssignedAt");

                    b.Property<DateTime?>("ClosedAt");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatorId");

                    b.Property<int>("DataSetId");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<float?>("EfficiencyRate");

                    b.Property<DateTime?>("ExecutedAt");

                    b.Property<int?>("ExecutorId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("StartedAt");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DataSetId");

                    b.HasIndex("ExecutorId");

                    b.ToTable("Researches");
                });

            modelBuilder.Entity("AlgorithmsRanking.Entities.Research", b =>
                {
                    b.HasOne("AlgorithmsRanking.Entities.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlgorithmsRanking.Entities.Person", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlgorithmsRanking.Entities.DataSet", "DataSet")
                        .WithMany()
                        .HasForeignKey("DataSetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlgorithmsRanking.Entities.Person", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId");
                });
#pragma warning restore 612, 618
        }
    }
}
