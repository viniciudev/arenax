using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Renametableclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Verificar e remover a FK existente (se existir)
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (
                        SELECT 1 FROM information_schema.table_constraints 
                        WHERE constraint_name = 'FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterCl'
                        AND table_name = 'SportsCourtAppointments'
                    ) THEN
                        ALTER TABLE ""SportsCourtAppointments"" 
                        DROP CONSTRAINT ""FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterCl"";
                    END IF;
                END $$;
            ");

            // 2. Renomear a tabela
            migrationBuilder.RenameTable(
                name: "SportsCenterClients",
                newName: "Client");

            // 3. Renomear a coluna de referência
            migrationBuilder.RenameColumn(
                name: "IdSportsCenterClient",
                table: "SportsCourtAppointments",
                newName: "IdClient");

            // 4. Renomear o índice se existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (
                        SELECT 1 FROM pg_indexes 
                        WHERE indexname = 'IX_SportsCourtAppointments_IdSportsCenterClient'
                    ) THEN
                        ALTER INDEX ""IX_SportsCourtAppointments_IdSportsCenterClient"" 
                        RENAME TO ""IX_SportsCourtAppointments_IdClient"";
                    END IF;
                END $$;
            ");

            // 5. Recriar a FK com o novo nome
            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtAppointments_Client_IdClient",
                table: "SportsCourtAppointments",
                column: "IdClient",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // 6. Renomear o índice da tabela Client se existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (
                        SELECT 1 FROM pg_indexes 
                        WHERE indexname = 'IX_SportsCenterClients_IdSportCenter'
                    ) THEN
                        ALTER INDEX ""IX_SportsCenterClients_IdSportCenter"" 
                        RENAME TO ""IX_Client_IdSportCenter"";
                    END IF;
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Remover a FK atual
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtAppointments_Client_IdClient",
                table: "SportsCourtAppointments");

            // 2. Renomear a tabela de volta
            migrationBuilder.RenameTable(
                name: "Client",
                newName: "SportsCenterClients");

            // 3. Renomear a coluna de volta
            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "SportsCourtAppointments",
                newName: "IdSportsCenterClient");

            // 4. Renomear o índice de volta se existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (
                        SELECT 1 FROM pg_indexes 
                        WHERE indexname = 'IX_SportsCourtAppointments_IdClient'
                    ) THEN
                        ALTER INDEX ""IX_SportsCourtAppointments_IdClient"" 
                        RENAME TO ""IX_SportsCourtAppointments_IdSportsCenterClient"";
                    END IF;
                END $$;
            ");

            // 5. Recriar a FK original
            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterClient",
                table: "SportsCourtAppointments",
                column: "IdSportsCenterClient",
                principalTable: "SportsCenterClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // 6. Renomear o índice de volta se existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (
                        SELECT 1 FROM pg_indexes 
                        WHERE indexname = 'IX_Client_IdSportCenter'
                    ) THEN
                        ALTER INDEX ""IX_Client_IdSportCenter"" 
                        RENAME TO ""IX_SportsCenterClients_IdSportCenter"";
                    END IF;
                END $$;
            ");
        }
    }
}