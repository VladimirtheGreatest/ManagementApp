﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TranslationManagement.Data;

#nullable disable

namespace TranslationManagement.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230718142732_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslationJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("TranslatedContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TranslatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslatorId");

                    b.ToTable("TranslationJobs");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslationJobStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TranslationJobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslationJobId")
                        .IsUnique()
                        .HasFilter("[TranslationJobId] IS NOT NULL");

                    b.ToTable("TranslatorJobStatuses");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslatorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HourlyRate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Translators");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslatorStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TranslatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslatorId")
                        .IsUnique()
                        .HasFilter("[TranslatorId] IS NOT NULL");

                    b.ToTable("TranslatorStatuses");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslationJob", b =>
                {
                    b.HasOne("TranslationManagement.Data.Entities.TranslatorModel", "Translator")
                        .WithMany("TranslatorJobs")
                        .HasForeignKey("TranslatorId");

                    b.Navigation("Translator");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslationJobStatus", b =>
                {
                    b.HasOne("TranslationManagement.Data.Entities.TranslationJob", "TranslatorJob")
                        .WithOne("TranslationJobStatus")
                        .HasForeignKey("TranslationManagement.Data.Entities.TranslationJobStatus", "TranslationJobId");

                    b.Navigation("TranslatorJob");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslatorStatus", b =>
                {
                    b.HasOne("TranslationManagement.Data.Entities.TranslatorModel", "Translator")
                        .WithOne("TranslatorStatus")
                        .HasForeignKey("TranslationManagement.Data.Entities.TranslatorStatus", "TranslatorId");

                    b.Navigation("Translator");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslationJob", b =>
                {
                    b.Navigation("TranslationJobStatus");
                });

            modelBuilder.Entity("TranslationManagement.Data.Entities.TranslatorModel", b =>
                {
                    b.Navigation("TranslatorJobs");

                    b.Navigation("TranslatorStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
