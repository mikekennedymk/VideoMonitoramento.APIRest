﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoMonitoramento.APIRest.Context;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231209065424_FK-servidor")]
    partial class FKservidor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VideoMonitoramento.APIRest.Models.Servidor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EnderecoIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PortaIP")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Servidores");
                });

            modelBuilder.Entity("VideoMonitoramento.APIRest.Models.Video", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("ServidorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("VideoContent")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.HasIndex("ServidorID");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("VideoMonitoramento.APIRest.Models.Video", b =>
                {
                    b.HasOne("VideoMonitoramento.APIRest.Models.Servidor", "Servidor")
                        .WithMany("Videos")
                        .HasForeignKey("ServidorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servidor");
                });

            modelBuilder.Entity("VideoMonitoramento.APIRest.Models.Servidor", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
